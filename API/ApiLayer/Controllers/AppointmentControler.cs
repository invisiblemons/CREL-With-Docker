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
[Route("api/" + MyConstant.apiVersion + "/appointments")]
public class AppointmentController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;

    public AppointmentController(IUnitOfWorkService unitOfWorkService, IMapper mapper)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get Appointment by ID
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<AppointmentDto>> GetAppointment(decimal id)
    {
        AppointmentDto? appointmentDto = await unitOfWorkService.AppointmentService.GetAppointmentDtoById(id);
        if (appointmentDto == null)
        {
            return NotFound();
        }
        return Ok(appointmentDto);
    }

    /// <summary>
    /// Search Appointment
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PagedList<AppointmentDto>>> SearchAppointment([FromQuery] AppointmentFilter appointmentFilter)
    {
        PagedList<AppointmentDto> pagedListAppointment = await unitOfWorkService.AppointmentService.GetAppointmentDtos(appointmentFilter);
        Response.AddPaginationHeader(pagedListAppointment);
        return Ok(pagedListAppointment);
    }

    /// <summary>
    /// Create Appointment
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<AppointmentDto>> CreateAppointment([FromBody] CreateAppointmentDto createAppointmentDto)
    {
        Appointment appointment = mapper.Map<Appointment>(createAppointmentDto);
        await unitOfWorkService.AppointmentService.Add(appointment);
        await unitOfWorkService.CommitAsync();
        if (appointment.Status == 1)
        {
            Brand? brand = await unitOfWorkService.BrandService.GetBrandById(appointment.BrandId);
            await NotificationHelper.SendNotifications(
                    new NotificationDto
                    {
                        BrandId = appointment.BrandId,
                        BrokerId = appointment.BrokerId,
                        AppointmentId = appointment.Id,
                        SendDateTime = DateTime.Now,
                        BrandName = brand!.Name,
                        Type = MyConstant.Notification.Type.AppointmentCreation,
                    }
                );
        }
        else
        {
            if (appointment.Status == 2)
            {
                var brandDto = await unitOfWorkService.BrandService.GetBrandDtoById(appointment.BrandId);
                var brokerDto = await unitOfWorkService.BrokerService.GetBrokerDtoById(appointment.BrokerId);
                if (brandDto != null && brokerDto != null)
                {
                    EmailHelper.SendEmailApproveAppoinment(
                        brandDto.Name ?? "",
                        brandDto.Email ?? "",
                        appointment.OnDateTime.ToString("HH:mm, dd-MM-yyyy"),
                        "https://crel.site/cuoc-hen/danh-sach",
                        appointment.Description ?? "",
                        brokerDto.Name ?? "",
                        brokerDto.PhoneNumber ?? ""
                    );
                }
            }
        }
        return Ok(mapper.Map<AppointmentDto>(appointment));
    }

    /// <summary>
    /// Update Appointment
    /// </summary>
    [HttpPut]
    [Authorize]
    public async Task<ActionResult<AppointmentDto>> UpdateAppointment([FromBody] UpdateAppointmentDto updateAppointmentDto)
    {
        Appointment? appointment = await unitOfWorkService.AppointmentService.GetAppointmentById(updateAppointmentDto.Id!.Value);
        if (appointment == null)
        {
            return NotFound();
        }
        var isApprove = updateAppointmentDto.Status == 2 && appointment.Status == 1;
        var isNotApprove = updateAppointmentDto.Status == 3 && appointment.Status == 1;
        mapper.Map(updateAppointmentDto, appointment);
        await unitOfWorkService.CommitAsync();
        if (isApprove)
        {
            var brandDto = await unitOfWorkService.BrandService.GetBrandDtoById(appointment.BrandId);
            var brokerDto = await unitOfWorkService.BrokerService.GetBrokerDtoById(appointment.BrokerId);
            if (brandDto != null && brokerDto != null)
            {
                EmailHelper.SendEmailApproveAppoinment(
                    brandDto.Name ?? "",
                    brandDto.Email ?? "",
                    appointment.OnDateTime.ToString("HH:mm, dd-MM-yyyy"),
                    "https://crel.site/cuoc-hen/danh-sach",
                    appointment.Description ?? "",
                    brokerDto.Name ?? "",
                    brokerDto.PhoneNumber ?? ""
                );
            }
        }
        if (isNotApprove)
        {
            var brandDto = await unitOfWorkService.BrandService.GetBrandDtoById(appointment.BrandId);
            if (brandDto != null)
            {
                EmailHelper.SendEmailNotApproveAppoinment(
                    brandDto.Name ?? "",
                    brandDto.Email ?? "",
                    "https://crel.site/cuoc-hen/danh-sach",
                    appointment.RejectMessage ?? ""
                );
            }
        }
        return Ok(mapper.Map<AppointmentDto>(appointment));
    }

    /// <summary>
    /// Delete Appointment
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<AppointmentDto>> DeleteAppointment(decimal id)
    {
        Appointment? appointment = await unitOfWorkService.AppointmentService.GetAppointmentById(id);
        if (appointment == null)
        {
            return NotFound();
        }
        appointment = unitOfWorkService.AppointmentService.Delete(appointment);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<AppointmentDto>(appointment));
    }
}
