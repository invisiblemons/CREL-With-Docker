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
[Route("api/" + MyConstant.apiVersion + "/property_brands")]
public class PropertyBrandController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;

    public PropertyBrandController(IUnitOfWorkService unitOfWorkService, IMapper mapper)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
    }

    // /// <summary>
    // /// Get Property Brand by ID
    // /// </summary>
    // [HttpGet("{id}")]
    // public async Task<ActionResult<PropertyBrandDto>> GetPropertyBrand(decimal propertyId, decimal brandId)
    // {
    //     PropertyBrandDto? propertyBrandDto = await unitOfWorkService.PropertyBrandService.GetPropertyBrandDtoById(propertyId, brandId);
    //     if (propertyBrandDto == null)
    //     {
    //         return NotFound();
    //     }
    //     return Ok(propertyBrandDto);
    // }

    /// <summary>
    /// Search Property Brand
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PagedList<PropertyBrandDto>>> SearchPropertyBrand([FromQuery] PropertyBrandFilter propertyBrandFilter)
    {
        PagedList<PropertyBrandDto> pagedListPropertyBrand = await unitOfWorkService.PropertyBrandService.GetPropertyBrandDtos(propertyBrandFilter);
        Response.AddPaginationHeader(pagedListPropertyBrand);
        return Ok(pagedListPropertyBrand);
    }

    /// <summary>
    /// Create Property Brand
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<PropertyBrandDto>> CreatePropertyBrand([FromBody] CreatePropertyBrandDto createPropertyBrandDto)
    {
        PropertyBrand propertyBrand = mapper.Map<PropertyBrand>(createPropertyBrandDto);
        await unitOfWorkService.PropertyBrandService.Add(propertyBrand);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<PropertyBrandDto>(propertyBrand));
    }

    /// <summary>
    /// Update Property Brand
    /// </summary>
    [HttpPut]
    [Authorize]
    public async Task<ActionResult<PropertyBrandDto>> UpdatePropertyBrand([FromBody] UpdatePropertyBrandDto updatePropertyBrandDto)
    {
        PropertyBrand? propertyBrand = await unitOfWorkService.PropertyBrandService.GetPropertyBrandById(updatePropertyBrandDto.PropertyId!.Value, updatePropertyBrandDto.BrandId!.Value);
        if (propertyBrand == null)
        {
            return NotFound();
        }
        mapper.Map(updatePropertyBrandDto, propertyBrand);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<PropertyBrandDto>(propertyBrand));
    }

    /// <summary>
    /// Delete Property Brand
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<PropertyBrandDto>> DeletePropertyBrand(decimal propertyId, decimal brandId)
    {
        PropertyBrand? propertyBrand = await unitOfWorkService.PropertyBrandService.GetPropertyBrandById(propertyId, brandId);
        if (propertyBrand == null)
        {
            return NotFound();
        }
        propertyBrand = unitOfWorkService.PropertyBrandService.Delete(propertyBrand);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<PropertyBrandDto>(propertyBrand));
    }
}
