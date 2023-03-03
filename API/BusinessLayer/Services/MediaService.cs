using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;

namespace CREL_BE.Services;

public class MediaService : IMediaService
{
    private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;

    public MediaService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<MediaDto?> GetMediaDtoById(decimal id)
    {
        return await unitOfWork.MediaRepository.GetMediaDtoById(id);
    }

    public Task<PagedList<MediaDto>> GetMediaDtos(MediaFilter mediaFilter)
    {
        return unitOfWork.MediaRepository.GetMediaDtos(mediaFilter);
    }

    public async Task<Media> Add(Media media)
    {
        return await unitOfWork.MediaRepository.Add(media);
    }

    public async Task<Media?> GetMediaById(decimal id)
    {
        return await unitOfWork.MediaRepository.GetMediaById(id);
    }

    public Media Delete(Media media)
    {
        return unitOfWork.MediaRepository.Delete(media);
    }

}
