using Imagekit;

namespace CREL_BE.Services;

public interface IFileService
{
    Task<ImagekitResponse> UploadFileAsync(IFormFile file);

    Task<DeleteAPIResponse> DeleteFileAsync(string publicId);
}
