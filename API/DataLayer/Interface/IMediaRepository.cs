using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Repositories;

public interface IMediaRepository
{
    Task<MediaDto?> GetMediaDtoById(decimal id);
    Task<PagedList<MediaDto>> GetMediaDtos(MediaFilter mediaFilter);
	Task<Media> Add(Media media);
	Task<Media?> GetMediaById(decimal id);
	Media Delete(Media media);
}
