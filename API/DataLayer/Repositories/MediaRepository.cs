using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class MediaRepository : IMediaRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public MediaRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<MediaDto?> GetMediaDtoById(decimal id)
	{
		return (await dbContext.Media
				.Where(entity => entity.Id == id)
				.ProjectTo<MediaDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<MediaDto>> GetMediaDtos(MediaFilter mediaFilter)
	{
		return PagedList<MediaDto>.CreateAsync(
			mediaFilter.ApplyFilter(dbContext.Media)
			.ProjectTo<MediaDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, mediaFilter);
	}
	
	public async Task<Media> Add(Media media)
	{
		return (await dbContext.Media.AddAsync(media)).Entity;
	}
	
	public async Task<Media?> GetMediaById(decimal id)
	{
		return (await dbContext.Media.FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public Media Delete(Media media)
	{
		return dbContext.Media.Remove(media).Entity;
	}
	
}
