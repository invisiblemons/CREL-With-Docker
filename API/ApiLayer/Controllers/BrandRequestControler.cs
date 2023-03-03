using AutoMapper;
using CREL_BE.Extensions;
using CREL_BE.Helpers;
using CREL_BE.Entities;
using CREL_BE.Filter;
using Microsoft.AspNetCore.Mvc;
using CREL_BE.Services;
using CREL_BE.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace CREL_BE.Controllers;
[ApiController]
[Route("api/" + MyConstant.apiVersion + "/brand-requests")]
public class BrandRequestController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;

    public BrandRequestController(IUnitOfWorkService unitOfWorkService, IMapper mapper)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get BrandRequest by ID
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "Brand,AreaManager,Admin")]
    public async Task<ActionResult<BrandRequestDto>> GetBrandRequest(decimal id)
    {
        BrandRequestDto? brandRequestDto = await unitOfWorkService.BrandRequestService.GetBrandRequestDtoById(id);
        if (brandRequestDto == null)
        {
            return NotFound();
        }
        return Ok(brandRequestDto);
    }

    /// <summary>
    /// Search BrandRequest
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Brand,AreaManager,Admin")]
    public async Task<ActionResult<PagedList<BrandRequestDto>>> SearchBrandRequest([FromQuery] BrandRequestFilter brandRequestFilter)
    {
        PagedList<BrandRequestDto> pagedListBrandRequest = await unitOfWorkService.BrandRequestService.GetBrandRequestDtos(brandRequestFilter);
        Response.AddPaginationHeader(pagedListBrandRequest);
        return Ok(pagedListBrandRequest);
    }

    /// <summary>
    /// Create BrandRequest
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Brand,AreaManager,Admin")]
    public async Task<ActionResult<BrandRequestDto>> CreateBrandRequest([FromBody] CreateBrandRequestDto createBrandRequestDto)
    {
        BrandRequest brandRequest = mapper.Map<BrandRequest>(createBrandRequestDto);
        await unitOfWorkService.BrandRequestService.Add(brandRequest);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<BrandRequestDto>(brandRequest));
    }

    /// <summary>
    /// Update BrandRequest
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Brand,AreaManager,Admin")]
    public async Task<ActionResult<BrandRequestDto>> UpdateBrandRequest([FromBody] UpdateBrandRequestDto updateBrandRequestDto)
    {
        BrandRequest? brandRequest = await unitOfWorkService.BrandRequestService.GetBrandRequestById(updateBrandRequestDto.Id!.Value);
        if (brandRequest == null)
        {
            return NotFound();
        }
        mapper.Map(updateBrandRequestDto, brandRequest);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<BrandRequestDto>(brandRequest));
    }

    /// <summary>
    /// Delete BrandRequest
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Brand,AreaManager,Admin")]
    public async Task<ActionResult<BrandRequestDto>> DeleteBrandRequest(decimal id)
    {
        BrandRequest? brandRequest = await unitOfWorkService.BrandRequestService.GetBrandRequestById(id);
        if (brandRequest == null)
        {
            return NotFound();
        }
        brandRequest = unitOfWorkService.BrandRequestService.Delete(brandRequest);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<BrandRequestDto>(brandRequest));
    }

    /// <summary>
    /// Add Property for BrandRequest
    /// </summary>
    [HttpPost("{id}/properties")]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<BrandRequestDto>> AddPropertyToBrandRequest(decimal id, [FromBody] ICollection<decimal> ids)
    {
        var brandRequest = await unitOfWorkService.BrandRequestService.GetBrandRequestById(id);

        if (brandRequest == null)
        {
            return NotFound();
        }

        if (ids.Count == 0)
        {
            return BadRequest(new { message = "No ID" });
        }

        foreach (var propertyId in ids)
        {
            var property = await unitOfWorkService.PropertyService.GetPropertyById(propertyId);
            if (property == null)
            {
                return NotFound();
            }
            if (!brandRequest.Properties.Contains(property))
            {
                brandRequest.Properties.Add(property);
            }
        }

        await unitOfWorkService.CommitAsync();
        if (brandRequest.Amount == 0)
        {
            EmailHelper.SendEmailBrandRequest(await unitOfWorkService.BrandRequestService.GetBrandRequestDtoById(id) ?? new BrandRequestDto());
        }
        return Ok(await unitOfWorkService.BrandRequestService.GetBrandRequestDtoById(id));
    }

    /// <summary>
    /// Remove Property from BrandRequest
    /// </summary>
    [HttpDelete("{id}/properties")]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<BrandRequestDto>> RemovePropertyFromBrandRequest(decimal id, [FromBody] ICollection<decimal> ids)
    {
        var brandRequest = await unitOfWorkService.BrandRequestService.GetBrandRequestById(id);

        if (brandRequest == null)
        {
            return NotFound();
        }

        if (ids.Count == 0)
        {
            return BadRequest(new { message = "No ID" });
        }

        foreach (var propertyId in ids)
        {
            var property = await unitOfWorkService.PropertyService.GetPropertyById(propertyId);
            if (property == null)
            {
                return NotFound();
            }
            if (brandRequest.Properties.Contains(property))
            {
                brandRequest.Properties.Remove(property);
            }
        }

        await unitOfWorkService.CommitAsync();
        return Ok(await unitOfWorkService.BrandRequestService.GetBrandRequestDtoById(id));
    }
}
