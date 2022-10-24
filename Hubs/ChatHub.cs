using PlayerDuo.DTOs.Messages;
using PlayerDuo.Repositories.Messages;
using Microsoft.AspNetCore.SignalR;

namespace PlayerDuo.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _messageRepository;

        public ChatHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        // userId can be user's id, or anonymous guid
        public async Task ConnectUserToChatHub(string userId)
        {
            var shakeHandMessage = new object();
            shakeHandMessage = $"User {userId} has joined {userId} group";

            var groupName = userId;
            //var new_chat_guid_obj = new { ChatGuid = "" };
            if (userId.Equals("ANONYMOUS"))
            {
                var shortGuid = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("==", String.Empty).Replace("/", String.Empty).Replace("+", String.Empty);
                shakeHandMessage = new { ChatGuid = shortGuid };
                // add to group with group name = guid
                groupName = shortGuid;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            // check connection message
            await Clients.Groups(groupName).SendAsync("ShakeHandMessage", shakeHandMessage);
        }

        public async Task SendMessage(MessageDTO messageDto)
        {
            // save new message to database 
            var newMessage = new MessageDTO()
            {
                SenderId = messageDto.SenderId,
                ReceiverId = messageDto.ReceiverId,
                Content = messageDto.Content,
                ImageUrl = messageDto.ImageUrl
            };

            // create new message in database
            await _messageRepository.CreateMessage(newMessage.SenderId, newMessage);

            // send new message via signal R to sender and receiver clients

            var groupNames = new List<string>() { messageDto.SenderId.ToString(), messageDto.ReceiverId.ToString() };
            await Clients.Groups(groupNames)
                            .SendAsync("ReceiveMessage", newMessage);
        }

        public async Task ChangeTypingState(bool isTyping, string senderId, string receiverId)
        {
            await Clients.Groups(receiverId).SendAsync("ChangeTypingState", new { senderId = senderId, isTyping = isTyping });
        }
    }
}
