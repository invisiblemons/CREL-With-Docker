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
[Route("api/" + MyConstant.apiVersion + "/properties")]
public class PropertyController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;
    private readonly IFileService fileService;

    public PropertyController(IUnitOfWorkService unitOfWorkService, IMapper mapper, IFileService fileService)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
        this.fileService = fileService;
    }

    /// <summary>
    /// Get Property by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyDto>> GetProperty(decimal id)
    {
        PropertyDto? propertyDto = await unitOfWorkService.PropertyService.GetPropertyDtoById(id);
        if (propertyDto == null)
        {
            return NotFound();
        }
        return Ok(propertyDto);
    }

    /// <summary>
    /// Search Property
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<PropertyDto>>> SearchProperty([FromQuery] PropertyFilter propertyFilter)
    {
        PagedList<PropertyDto> pagedListProperty = await unitOfWorkService.PropertyService.GetPropertyDtos(propertyFilter);
        Response.AddPaginationHeader(pagedListProperty);
        return Ok(pagedListProperty);
    }

    /// <summary>
    /// Create Property
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<PropertyDto>> CreateProperty([FromBody] CreatePropertyDto createPropertyDto)
    {
        Property property = mapper.Map<Property>(createPropertyDto);
        property.LastUpdateDate = DateTime.Now;
        await unitOfWorkService.PropertyService.Add(property);
        await unitOfWorkService.CommitAsync();
        await NotificationHelper.SendNotifications(
                new NotificationDto
                {
                    BrokerId = property.BrokerId,
                    PropertyId = property.Id,
                    SendDateTime = DateTime.Now,
                    PropertyName = property.Name,
                    Type = MyConstant.Notification.Type.PropertyForRentAssign,
                }
            );
        return Ok(mapper.Map<PropertyDto>(property));
    }

    /// <summary>
    /// Update Property
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Broker,AreaManager,Admin")]
    public async Task<ActionResult<PropertyDto>> UpdateProperty([FromBody] UpdatePropertyDto updatePropertyDto)
    {
        Property? property = await unitOfWorkService.PropertyService.GetPropertyById(updatePropertyDto.Id!.Value);
        if (property == null)
        {
            return NotFound();
        }
        if (updatePropertyDto.BrokerId != property.BrokerId)
        {
            await NotificationHelper.SendNotifications(
                new NotificationDto
                {
                    BrokerId = updatePropertyDto.BrokerId,
                    PropertyId = updatePropertyDto.Id,
                    SendDateTime = DateTime.Now,
                    PropertyName = updatePropertyDto.Name,
                    Type = MyConstant.Notification.Type.PropertyForRentAssign,
                }
            );

            await NotificationHelper.SendNotifications(
                new NotificationDto
                {
                    BrokerId = property.BrokerId,
                    PropertyId = property.Id,
                    SendDateTime = DateTime.Now,
                    PropertyName = property.Name,
                    Type = MyConstant.Notification.Type.PropertyForRentUnassign,
                }
            );
        }
        mapper.Map(updatePropertyDto, property);
        property.LastUpdateDate = DateTime.Now;
        await unitOfWorkService.CommitAsync();
        return Ok(await unitOfWorkService.PropertyService.GetPropertyDtoById(updatePropertyDto.Id!.Value));
    }

    /// <summary>
    /// Delete Property
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<PropertyDto>> DeleteProperty(decimal id)
    {
        Property? property = await unitOfWorkService.PropertyService.GetPropertyById(id);
        if (property == null)
        {
            return NotFound();
        }
        property = unitOfWorkService.PropertyService.Delete(property);
        property.LastUpdateDate = DateTime.Now;
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<PropertyDto>(property));
    }

    /// <summary>
    /// Add Media for Property
    /// </summary>
    [HttpPost("{id}/media")]
    [Authorize(Roles = "Broker,AreaManager,Admin")]
    public async Task<ActionResult<PropertyDto>> AddMediaToProperty(decimal id, IFormFileCollection files)
    {
        var property = await unitOfWorkService.PropertyService.GetPropertyById(id);

        if (property == null)
        {
            return NotFound();
        }

        if (files.Count == 0)
        {
            return BadRequest(new { message = "No file" });
        }

        foreach (var file in files)
        {
            var uploadFile = await fileService.UploadFileAsync(file);
            property.Media.Add(new Media
            {
                FileId = uploadFile.FileId,
                Link = uploadFile.URL,
                Type = 1
            });
        }
        property.LastUpdateDate = DateTime.Now;
        await unitOfWorkService.CommitAsync();
        return Ok(await unitOfWorkService.PropertyService.GetPropertyDtoById(id));
    }

    /// <summary>
    /// Add Certification for Property
    /// </summary>
    [HttpPost("{id}/certification")]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<PropertyDto>> AddCertificationToProperty(decimal id, IFormFileCollection files)
    {
        var property = await unitOfWorkService.PropertyService.GetPropertyById(id);

        if (property == null)
        {
            return NotFound();
        }

        if (files.Count == 0)
        {
            return BadRequest(new { message = "No file" });
        }

        foreach (var file in files)
        {
            var uploadFile = await fileService.UploadFileAsync(file);
            property.Media.Add(new Media
            {
                FileId = uploadFile.FileId,
                Link = uploadFile.URL,
                Type = 2
            });
        }

        property.LastUpdateDate = DateTime.Now;
        await unitOfWorkService.CommitAsync();
        return Ok(await unitOfWorkService.PropertyService.GetPropertyDtoById(id));
    }

    /// <summary>
    /// Add Owners for Property
    /// </summary>
    [HttpPost("{id}/owners")]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<PropertyDto>> AddOwnersToProperty(decimal id, [FromBody] ICollection<decimal> ids)
    {
        var property = await unitOfWorkService.PropertyService.GetPropertyById(id);

        if (property == null)
        {
            return NotFound();
        }

        if (ids.Count == 0)
        {
            return BadRequest(new { message = "No ID" });
        }

        foreach (var contactId in ids)
        {
            var contact = await unitOfWorkService.OwnerService.GetOwnerById(contactId);
            if (contact == null)
            {
                return NotFound();
            }
            property.Owners.Add(contact);
        }

        property.LastUpdateDate = DateTime.Now;
        await unitOfWorkService.CommitAsync();
        return Ok(await unitOfWorkService.PropertyService.GetPropertyDtoById(id));
    }

    /// <summary>
    /// Remove Owners from Property
    /// </summary>
    [HttpDelete("{id}/owners")]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<PropertyDto>> RemoveOwnersFromProperty(decimal id, [FromBody] ICollection<decimal> ids)
    {
        var property = await unitOfWorkService.PropertyService.GetPropertyById(id);

        if (property == null)
        {
            return NotFound();
        }

        if (ids.Count == 0)
        {
            return BadRequest(new { message = "No ID" });
        }

        foreach (var contactId in ids)
        {
            var contact = await unitOfWorkService.OwnerService.GetOwnerById(contactId);
            if (contact == null)
            {
                return NotFound();
            }
            property.Owners.Remove(contact);
        }

        property.LastUpdateDate = DateTime.Now;
        await unitOfWorkService.CommitAsync();
        return Ok(await unitOfWorkService.PropertyService.GetPropertyDtoById(id));
    }
}
