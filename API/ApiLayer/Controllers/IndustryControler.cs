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
[Route("api/" + MyConstant.apiVersion + "/industries")]
public class IndustryController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;

    public IndustryController(IUnitOfWorkService unitOfWorkService, IMapper mapper)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get Industry by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<IndustryDto>> GetIndustry(decimal id)
    {
        IndustryDto? industryDto = await unitOfWorkService.IndustryService.GetIndustryDtoById(id);
        if (industryDto == null)
        {
            return NotFound();
        }
        return Ok(industryDto);
    }

    /// <summary>
    /// Search Industry
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<IndustryDto>>> SearchIndustry([FromQuery] IndustryFilter industryFilter)
    {
        PagedList<IndustryDto> pagedListIndustry = await unitOfWorkService.IndustryService.GetIndustryDtos(industryFilter);
        Response.AddPaginationHeader(pagedListIndustry);
        return Ok(pagedListIndustry);
    }

    /// <summary>
    /// Create Industry
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IndustryDto>> CreateIndustry([FromBody] CreateIndustryDto createIndustryDto)
    {
        Industry industry = mapper.Map<Industry>(createIndustryDto);
        await unitOfWorkService.IndustryService.Add(industry);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<IndustryDto>(industry));
    }

    /// <summary>
    /// Update Industry
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IndustryDto>> UpdateIndustry([FromBody] UpdateIndustryDto updateIndustryDto)
    {
        Industry? industry = await unitOfWorkService.IndustryService.GetIndustryById(updateIndustryDto.Id!.Value);
        if (industry == null)
        {
            return NotFound();
        }
        mapper.Map(updateIndustryDto, industry);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<IndustryDto>(industry));
    }

    /// <summary>
    /// Delete Industry
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IndustryDto>> DeleteIndustry(decimal id)
    {
        Industry? industry = await unitOfWorkService.IndustryService.GetIndustryById(id);
        if (industry == null)
        {
            return NotFound();
        }
        industry = unitOfWorkService.IndustryService.Delete(industry);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<IndustryDto>(industry));
    }
}
