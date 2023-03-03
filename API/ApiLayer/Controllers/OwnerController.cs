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
[Route("api/" + MyConstant.apiVersion + "/owners")]
public class OwnerController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;

    public OwnerController(IUnitOfWorkService unitOfWorkService, IMapper mapper)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get Owner by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<OwnerDto>> GetOwner(decimal id)
    {
        OwnerDto? contactDto = await unitOfWorkService.OwnerService.GetOwnerDtoById(id);
        if (contactDto == null)
        {
            return NotFound();
        }
        return Ok(contactDto);
    }

    /// <summary>
    /// Search Owner
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<OwnerDto>>> SearchOwner([FromQuery] OwnerFilter contactFilter)
    {
        PagedList<OwnerDto> pagedListOwner = await unitOfWorkService.OwnerService.GetOwnerDtos(contactFilter);
        Response.AddPaginationHeader(pagedListOwner);
        return Ok(pagedListOwner);
    }

    /// <summary>
    /// Create Owner
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<OwnerDto>> CreateOwner([FromBody] CreateOwnerDto createOwnerDto)
    {
        Owner contact = mapper.Map<Owner>(createOwnerDto);
        await unitOfWorkService.OwnerService.Add(contact);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<OwnerDto>(contact));
    }

    /// <summary>
    /// Update Owner
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<OwnerDto>> UpdateOwner([FromBody] UpdateOwnerDto updateOwnerDto)
    {
        Owner? contact = await unitOfWorkService.OwnerService.GetOwnerById(updateOwnerDto.Id!.Value);
        if (contact == null)
        {
            return NotFound();
        }
        mapper.Map(updateOwnerDto, contact);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<OwnerDto>(contact));
    }

    /// <summary>
    /// Delete Owner
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<OwnerDto>> DeleteOwner(decimal id)
    {
        Owner? contact = await unitOfWorkService.OwnerService.GetOwnerById(id);
        if (contact == null)
        {
            return NotFound();
        }
        contact = unitOfWorkService.OwnerService.Delete(contact);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<OwnerDto>(contact));
    }
}
