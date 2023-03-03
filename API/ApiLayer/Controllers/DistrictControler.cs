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
[Route("api/" + MyConstant.apiVersion + "/districts")]
public class DistrictController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;

    public DistrictController(IUnitOfWorkService unitOfWorkService, IMapper mapper)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get District by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<DistrictDto>> GetDistrict(decimal id)
    {
        DistrictDto? districtDto = await unitOfWorkService.DistrictService.GetDistrictDtoById(id);
        if (districtDto == null)
        {
            return NotFound();
        }
        return Ok(districtDto);
    }

    /// <summary>
    /// Search District
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<DistrictDto>>> SearchDistrict([FromQuery] DistrictFilter districtFilter)
    {
        PagedList<DistrictDto> pagedListDistrict = await unitOfWorkService.DistrictService.GetDistrictDtos(districtFilter);
        Response.AddPaginationHeader(pagedListDistrict);
        return Ok(pagedListDistrict);
    }
}
