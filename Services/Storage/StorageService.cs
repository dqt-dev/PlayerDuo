namespace PlayerDuo.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly string _storageFolder;
        private const string StorageFolderName = "storage";

        public StorageService(IWebHostEnvironment webHostEnvironment)
        {
            _storageFolder = Path.Combine(webHostEnvironment.WebRootPath, StorageFolderName);
            // create the folder if it does not exist
            Directory.CreateDirectory(_storageFolder);
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            // create a new random file name, security issues, reference from Microsoft doc
            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            // create new path to save file into storage
            var newFilePath = Path.Combine(_storageFolder, newFileName);

            // save image
            using (var fileStream = new FileStream(newFilePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return $"/{StorageFolderName}/{newFileName}";
        }
    }
}
