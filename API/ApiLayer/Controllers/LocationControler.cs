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
[Route("api/" + MyConstant.apiVersion + "/locations")]
public class LocationController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;

    public LocationController(IUnitOfWorkService unitOfWorkService, IMapper mapper)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get Location by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LocationDto>> GetLocation(decimal id)
    {
        LocationDto? locationDto = await unitOfWorkService.LocationService.GetLocationDtoById(id);
        if (locationDto == null)
        {
            return NotFound();
        }
        return Ok(locationDto);
    }

    /// <summary>
    /// Search Location
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<LocationDto>>> SearchLocation([FromQuery] LocationFilter locationFilter)
    {
        PagedList<LocationDto> pagedListLocation = await unitOfWorkService.LocationService.GetLocationDtos(locationFilter);
        Response.AddPaginationHeader(pagedListLocation);
        return Ok(pagedListLocation);
    }

    /// <summary>
    /// Create Location
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<LocationDto>> CreateLocation([FromBody] CreateLocationDto createLocationDto)
    {
        Location location = mapper.Map<Location>(createLocationDto);
        await unitOfWorkService.LocationService.Add(location);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<LocationDto>(location));
    }

    /// <summary>
    /// Update Location
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<LocationDto>> UpdateLocation([FromBody] UpdateLocationDto updateLocationDto)
    {
        Location? location = await unitOfWorkService.LocationService.GetLocationById(updateLocationDto.Id!.Value);
        if (location == null)
        {
            return NotFound();
        }
        mapper.Map(updateLocationDto, location);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<LocationDto>(location));
    }

    /// <summary>
    /// Delete Location
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<LocationDto>> DeleteLocation(decimal id)
    {
        Location? location = await unitOfWorkService.LocationService.GetLocationById(id);
        if (location == null)
        {
            return NotFound();
        }
        location = unitOfWorkService.LocationService.Delete(location);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<LocationDto>(location));
    }
}
