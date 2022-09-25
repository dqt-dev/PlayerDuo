namespace PlayerDuo.Services.Storage
{
    public interface IStorageService
    {
        Task<string> SaveImage(IFormFile image);
    }
}
