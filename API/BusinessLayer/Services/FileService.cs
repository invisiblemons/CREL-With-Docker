using CREL_BE.Entities;
using CREL_BE.Helpers;
using CREL_BE.Repositories;
using Imagekit;
using Microsoft.Extensions.Options;

namespace CREL_BE.Services;

public class FileService : IFileService
{
    private readonly ServerImagekit imagekit;
    private readonly IUnitOfWorkService unitOfWorkService;
    public FileService(IOptions<ImageKitSettings> config, IUnitOfWorkService unitOfWorkService)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.imagekit = new ServerImagekit(config.Value.PublicKey, config.Value.PrivateKey, config.Value.URLEndpoint);
    }

    public async Task<ImagekitResponse> UploadFileAsync(IFormFile file)
    {
        var path = Path.GetTempFileName();
        path = Path.ChangeExtension(path, Path.GetExtension(file.FileName));
        using (var stream = System.IO.File.OpenWrite(path))
        {
            await file.CopyToAsync(stream);
        }

        var imagekitResponse = await imagekit
                   .FileName(file.FileName)
                   .UploadAsync(path);
        System.IO.File.Delete(path);
        return imagekitResponse;
    }

    public async Task CreateAnUpdatedMediaDTO(IFormFile file, Media media)
    {
        var updatedFile = await UploadFileAsync(file);
        media.FileId = updatedFile.FileId;
        media.Link = updatedFile.URL;
    }

    public async Task<DeleteAPIResponse> DeleteFileAsync(string fileId)
    {
        return await imagekit.DeleteFileAsync(fileId);
    }
}
