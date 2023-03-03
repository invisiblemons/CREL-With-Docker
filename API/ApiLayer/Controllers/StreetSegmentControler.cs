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
[Route("api/" + MyConstant.apiVersion + "/street-segments")]
public class StreetSegmentController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;

    public StreetSegmentController(IUnitOfWorkService unitOfWorkService, IMapper mapper)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get StreetSegment by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<StreetSegmentDto>> GetStreetSegment(decimal id)
    {
        StreetSegmentDto? streetSegmentDto = await unitOfWorkService.StreetSegmentService.GetStreetSegmentDtoById(id);
        if (streetSegmentDto == null)
        {
            return NotFound();
        }
        return Ok(streetSegmentDto);
    }

    /// <summary>
    /// Search StreetSegment
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<StreetSegmentDto>>> SearchStreetSegment([FromQuery] StreetSegmentFilter streetSegmentFilter)
    {
        PagedList<StreetSegmentDto> pagedListStreetSegment = await unitOfWorkService.StreetSegmentService.GetStreetSegmentDtos(streetSegmentFilter);
        Response.AddPaginationHeader(pagedListStreetSegment);
        return Ok(pagedListStreetSegment);
    }
}
