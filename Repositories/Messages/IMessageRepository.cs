using PlayerDuo.DTOs.Messages;
using PlayerDuo.DTOs.Users;

namespace PlayerDuo.Repositories.Messages
{
    public interface IMessageRepository
    {
        Task<List<MessageVm>> GetMessages(string userId, string withUserId);
        Task<MessageVm> GetMessageById(int messageId);
        Task<int> CreateMessage(string userId, MessageDTO message);
        Task<List<UserChatVm>> GetUserChatList(int userId); 
        Task<string> UploadImage(IFormFile image);
    }
}
