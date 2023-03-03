using AutoMapper;
using CREL_BE.Extensions;
using CREL_BE.Helpers;
using CREL_BE.Entities;
using CREL_BE.Dtos;
using CREL_BE.Filter;
using Microsoft.AspNetCore.Mvc;
using CREL_BE.Services;

namespace CREL_BE.Controllers;
[ApiController]
[Route("api/" + MyConstant.apiVersion + "/wards")]
public class WardController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;

    public WardController(IUnitOfWorkService unitOfWorkService, IMapper mapper)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get Ward by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<WardDto>> GetWard(decimal id)
    {
        WardDto? wardDto = await unitOfWorkService.WardService.GetWardDtoById(id);
        if (wardDto == null)
        {
            return NotFound();
        }
        return Ok(wardDto);
    }

    /// <summary>
    /// Search Ward
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<WardDto>>> SearchWard([FromQuery] WardFilter wardFilter)
    {
        PagedList<WardDto> pagedListWard = await unitOfWorkService.WardService.GetWardDtos(wardFilter);
        Response.AddPaginationHeader(pagedListWard);
        return Ok(pagedListWard);
    }
    
    /// <summary>
    /// Update Ward
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<WardDto>> UpdateWard([FromBody] UpdateWardDto updateWardDto)
    {
        Ward? ward = await unitOfWorkService.WardService.GetWardById(updateWardDto.Id!.Value);
        if (ward == null)
        {
            return NotFound();
        }
        mapper.Map(updateWardDto, ward);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<WardDto>(ward));
    }
}
