using PlayerDuo.Database.Entities;

namespace PlayerDuo.Services.Token
{
    public interface ITokenService
    {
        string CreateToken(User user, List<string> roles);
    }

}
