using AutoMapper;
using CREL_BE.Extensions;
using CREL_BE.Helpers;
using CREL_BE.Entities;
using CREL_BE.Dtos;
using CREL_BE.Filter;
using Microsoft.AspNetCore.Mvc;
using CREL_BE.Services;
using Microsoft.AspNetCore.Authorization;

namespace CREL_BE.Controllers;
[ApiController]
[Route("api/" + MyConstant.apiVersion + "/media")]
public class MediaController : ControllerBase
{
    private readonly IFileService fileService;
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;

    public MediaController(IUnitOfWorkService unitOfWorkService, IMapper mapper, IFileService fileService)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
        this.fileService = fileService;
    }

    /// <summary>
    /// Get Media by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<MediaDto>> GetMedia(decimal id)
    {
        MediaDto? mediaDto = await unitOfWorkService.MediaService.GetMediaDtoById(id);
        if (mediaDto == null)
        {
            return NotFound();
        }
        return Ok(mediaDto);
    }

    /// <summary>
    /// Search Media
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<MediaDto>>> SearchMedia([FromQuery] MediaFilter mediaFilter)
    {
        PagedList<MediaDto> pagedListMedia = await unitOfWorkService.MediaService.GetMediaDtos(mediaFilter);
        Response.AddPaginationHeader(pagedListMedia);
        return Ok(pagedListMedia);
    }

    /// <summary>
    /// Delete Media
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Broker,AreaManager,Admin")] 
    public async Task<ActionResult<MediaDto>> DeleteMedia(decimal id)
    {
        Media? media = await unitOfWorkService.MediaService.GetMediaById(id);
        if (media == null)
        {
            return NotFound();
        }
        if (media.FileId != null)
        {
            await fileService.DeleteFileAsync(media.FileId!);
        }
        media = unitOfWorkService.MediaService.Delete(media);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<MediaDto>(media));
    }

    /// <summary>
    /// Delete Muti Media
    /// </summary>
    [HttpDelete]
    [Authorize(Roles = "Broker,AreaManager,Admin")] 
    public async Task<ActionResult<ICollection<MediaDto>>> DeleteMutiMedia([FromBody] ICollection<decimal> ids)
    {
        var listMedia = new List<Media>();
        foreach (var id in ids)
        {
            Media? media = await unitOfWorkService.MediaService.GetMediaById(id);
            if (media == null)
            {
                return NotFound();
            }
            if (media.FileId != null)
            {
                await fileService.DeleteFileAsync(media.FileId!);
            }
            listMedia.Add(unitOfWorkService.MediaService.Delete(media));
        }
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<ICollection<MediaDto>>(listMedia));
    }
}
