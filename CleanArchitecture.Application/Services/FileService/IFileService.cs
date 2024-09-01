using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services.FileService
{
    public interface IFileService
    {
        Task<string> UploadFile(string location, IFormFile file);

    }
}
