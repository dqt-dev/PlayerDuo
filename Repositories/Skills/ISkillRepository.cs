using PlayerDuo.DTOs.Skills;
using PlayerDuo.Utilities;

namespace PlayerDuo.Repositories.Skills
{
    public interface ISkillRepository
    {
        Task<ApiResult<string>> CreateSkill(int userId, CreateSkillRequest request);
        Task<ApiResult<SkillVm>> GetSkillById(int skillId);
        Task<ApiResult<IList<SkillVm>>> GetSkills(int? userId, bool? IsEnabled); 
        Task<ApiResult<string>> DisableSkill(int skillId); // for admin
        Task<ApiResult<string>> EnableSkill(int skillId); // for admin
    }
}