using PlayerDuo.DTOs.Skills;
using PlayerDuo.Database;
using Microsoft.EntityFrameworkCore;
using PlayerDuo.Database.Entities;
using System.Linq;
using PlayerDuo.Utilities;

namespace PlayerDuo.Repositories.Skills
{
    public class SkillRepository : ISkillRepository
    {
        private readonly MyDbContext _context;
        private readonly string _storageFolder;
        private const string StorageFolderName = "audio";

        public SkillRepository(MyDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _storageFolder = Path.Combine(webHostEnvironment.WebRootPath, StorageFolderName);
            // create the folder if it does not exist
            Directory.CreateDirectory(_storageFolder);
            _context = context;
        }

        public async Task<ApiResult<string>> CreateSkill(int userId, CreateSkillRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            // check user exist
            if (user == null)
            {
                return new ApiResult<string> (false, Message: "User not exist!");
            }

            // create new object skill
            var newSkill = new Skill()
            {
                UserId = userId,
                CategoryId = request.CategoryId,
                AudioUrl = request.AudioUrl != null ? await UploadAudio(request.AudioUrl) : null,
                Description = request.Description,
                Price = request.Price
            };

            _context.Add(newSkill);
            var result = await _context.SaveChangesAsync();

            return new ApiResult<string>(true, Message: "Create new skill successfully!");
        }
        public async Task<string> UploadAudio(IFormFile audio)
        {
            return await SaveAudio(audio);
        }

        public async Task<string> SaveAudio(IFormFile audio)
        {
            // create a new random file name, security issues, reference from Microsoft doc
            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(audio.FileName)}";
            // create new path to save file into storage
            var newFilePath = Path.Combine(_storageFolder, newFileName);

            // save image
            using (var fileStream = new FileStream(newFilePath, FileMode.Create))
            {
                await audio.CopyToAsync(fileStream);
            }

            return $"/{StorageFolderName}/{newFileName}";
        }

        public async Task<ApiResult<SkillVm>> GetSkillById(int skillId)
        {
            if (!_context.Skills.Any(x => (x.Id == skillId)))
            {
                return new ApiResult<SkillVm>(false, Message: $"No skill exist with ID: {skillId}");
            }

            var skill = await
                        (from u in _context.Users
                         join s in _context.Skills on u.Id equals s.UserId
                         join c in _context.Categories on s.CategoryId equals c.Id
                         where s.Id == skillId
                         select new SkillVm
                         {
                            PlayerName = u.NickName,
                            CategoryName = c.CategoryName,
                            AudioUrl = s.AudioUrl,
                            Description = s.Description,
                            Total = GetTotal(skillId),
                            Rating = GetRating(skillId),
                            Price = s.Price,
                         }).FirstOrDefaultAsync();
            if(skill == null) return new ApiResult<SkillVm>(false, Message: "An error occurred!");
            return new ApiResult<SkillVm>(true, ResultObj: skill);


        }

        public int GetTotal(int skillId)
        {
            var total =
                        (from s in _context.Skills
                         join o in _context.Orders on s.Id equals o.SkillId
                         where o.Status == 3 && s.Id == skillId
                         group s by s.Id into t
                         select new { count = t.Count()  }
                         ).FirstOrDefault();
            if (total == null) return 0;
            return Int32.Parse(total.count.ToString());
        }

        public double GetRating(int skillId)
        {
            var average =
                        (from s in _context.Skills
                         join o in _context.Orders on s.Id equals o.SkillId
                         where o.Status == 3 && s.Id == skillId
                         group o by s.Id into t
                         select new { avg = t.Average(o => o.Rating) }
                         ).FirstOrDefault();
            if (average == null) return 0;
            return Int32.Parse(average.avg.ToString());
        }

        public async Task<ApiResult<string>> DisableSkill(int skillId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var skill = await _context.Skills.Where(x => x.Id == skillId).FirstOrDefaultAsync();
                if (skill == null)
                {
                    return new ApiResult<string>(false, Message: $"No skill exist with ID: {skillId}");
                }
                // disable skill
                skill.IsEnabled = false;
                await _context.SaveChangesAsync();

                // kiểm tra nếu không còn skill nào enable thì update lại role là user
                var skills = await _context.Skills.Where(x => x.UserId == skill.UserId && x.IsEnabled == true).ToListAsync();
                if (skills.Count() == 0)
                {
                    var user = await _context.Users.Where(x => x.Id == skill.UserId).FirstOrDefaultAsync();
                    if (user == null) return new ApiResult<string>(false, Message: $"No user exist with ID: {skill.UserId}");
                    user.isPlayer = false;
                    await _context.SaveChangesAsync();
                }
                // Commit transaction if all commands succeed, transaction will auto-rollback
                transaction.Commit();
                return new ApiResult<string>(true, Message: $"Disable skill successfully!");
            }
            catch (Exception)
            {
                // TODO: Handle failure
            }
            return new ApiResult<string>(false, Message: $"An error occurred!");

        }

        public async Task<ApiResult<string>> EnableSkill(int skillId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {

                var skill = await _context.Skills.Where(x => x.Id == skillId).FirstOrDefaultAsync();

                if (skill == null)
                {
                    return new ApiResult<string>(false, Message: $"No skill exist with ID: {skillId}");
                }

                // enable skill
                skill.IsEnabled = true;
                await _context.SaveChangesAsync();

                // update role cho user là player
                var isPlayer = await _context.UserRoles.Where(x => x.UserId == skill.UserId && x.RoleId.Equals("2")).FirstOrDefaultAsync();

                if (isPlayer == null)
                {
                    var userRole = new UserRole()
                    {
                        UserId = skill.UserId,
                        RoleId = 2
                    };
                    _context.UserRoles.Add(userRole);
                    await _context.SaveChangesAsync();
                }

                // Commit transaction if all commands succeed, transaction will auto-rollback
                transaction.Commit();
                return new ApiResult<string>(true, Message: $"Skill is alreadly!");
            }
            catch (Exception)
            {
                // TODO: Handle failure
            }
            return new ApiResult<string>(false, Message: "An error occurred!");
        }

        public async Task<ApiResult<IList<SkillVm>>> GetSkills(int? userId, bool? IsEnabled)
        {
            // get all skills
            var query = await
                        (from u in _context.Users
                         join s in _context.Skills on u.Id equals s.UserId
                         join c in _context.Categories on s.CategoryId equals c.Id
                         select new
                         {
                             UserId = u.Id,
                             Status = u.Status,
                             Avatar = u.AvatarUrl,
                             NickName = u.NickName,
                             CategoryName = c.CategoryName,
                             AudioUrl = s.AudioUrl,
                             Description = s.Description,
                             SkillId = s.Id,
                             Price = s.Price,
                             IsEnabled = s.IsEnabled,
                         }).Distinct().ToListAsync();
            // Trường hợp get all không filter theo userId và IsEnable
            if (userId == null && IsEnabled == null)
            {
                var result = query.Select(x => new SkillVm()
                {
                    UserId = x.UserId,
                    SkillId = x.SkillId,
                    PlayerName = x.NickName,
                    AvatarUrl = x.Avatar,
                    CategoryName = x.CategoryName,
                    AudioUrl = x.AudioUrl,
                    Description = x.Description,
                    Status = x.Status,
                    Price = x.Price,
                    Total = GetTotal(x.SkillId),
                    Rating = GetRating(x.SkillId),
                    IsEnabled = x.IsEnabled,
                }).ToList();
                return new ApiResult<IList<SkillVm>>(true, ResultObj: result);
            }
            if (userId != null && IsEnabled != null)
            {
                var result = query.Where(x => x.UserId == userId && x.IsEnabled == IsEnabled).Select(x => new SkillVm()
                {
                    UserId = x.UserId,
                    SkillId = x.SkillId,
                    PlayerName = x.NickName,
                    CategoryName = x.CategoryName,
                    AvatarUrl = x.Avatar,
                    AudioUrl = x.AudioUrl,
                    Description = x.Description,
                    Price = x.Price,
                    Status = x.Status,
                    Total = GetTotal(x.SkillId),
                    Rating = GetRating(x.SkillId),
                    IsEnabled = x.IsEnabled,
                }
                ).ToList();
                return new ApiResult<IList<SkillVm>>(true, ResultObj: result);
            }
            if (userId != null && IsEnabled == null)
            {
                var result = query.Where(x => x.UserId == userId).Select(x => new SkillVm()
                {
                    UserId = x.UserId,
                    SkillId = x.SkillId,
                    PlayerName = x.NickName,
                    CategoryName = x.CategoryName,
                    AvatarUrl = x.Avatar,
                    AudioUrl = x.AudioUrl,
                    Status = x.Status,
                    Description = x.Description,
                    Price = x.Price,
                    Total = GetTotal(x.SkillId),
                    Rating = GetRating(x.SkillId),
                    IsEnabled = x.IsEnabled,
                }
                ).ToList();
                return new ApiResult<IList<SkillVm>>(true, ResultObj: result);
            }
            if (userId == null && IsEnabled != null)
            {
                var result = query.Where(x => x.IsEnabled == IsEnabled).Select(x => new SkillVm()
                {
                    UserId = x.UserId,
                    SkillId = x.SkillId,
                    PlayerName = x.NickName,
                    CategoryName = x.CategoryName,
                    AvatarUrl = x.Avatar,
                    AudioUrl = x.AudioUrl,
                    Status = x.Status,
                    Description = x.Description,
                    Price = x.Price,
                    Total = GetTotal(x.SkillId),
                    Rating = GetRating(x.SkillId),
                    IsEnabled = x.IsEnabled,
                }
                ).ToList();
                return new ApiResult<IList<SkillVm>>(true, ResultObj: result);
            }
            return new ApiResult<IList<SkillVm>>(false, Message: "An error occurred!");
        }
    }
}
