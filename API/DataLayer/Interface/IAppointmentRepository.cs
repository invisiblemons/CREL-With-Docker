using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;

namespace CREL_BE.Repositories;

public interface IAppointmentRepository
{
    Task<AppointmentDto?> GetAppointmentDtoById(decimal id);
    Task<PagedList<AppointmentDto>> GetAppointmentDtos(AppointmentFilter appointmentFilter);
	Task<Appointment> Add(Appointment appointment);
	Task<Appointment?> GetAppointmentById(decimal id);
	Appointment Delete(Appointment appointment);
    Task<List<AppointmentDto>> GetDtosWithoutPaging(AppointmentFilter appointmentFilter);
    Task<List<Appointment>> GetAppointmentsWithoutPaging(AppointmentFilter filter);
}
