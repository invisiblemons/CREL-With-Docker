using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Repositories;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;


namespace CREL_BE.Services;

public class AppointmentService : IAppointmentService
{
	private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;
	
	public AppointmentService(IUnitOfWorkRepository unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	public async Task<AppointmentDto?> GetAppointmentDtoById(decimal id)
	{
		return await unitOfWork.AppointmentRepository.GetAppointmentDtoById(id);
	}
	
    public async Task<PagedList<AppointmentDto>> GetAppointmentDtos(AppointmentFilter appointmentFilter)
	{
		return await unitOfWork.AppointmentRepository.GetAppointmentDtos(appointmentFilter);
	}
	
	public async Task<Appointment> Add(Appointment appointment)
	{
		return await unitOfWork.AppointmentRepository.Add(appointment);
	}
	
	public async Task<Appointment?> GetAppointmentById(decimal id)
	{
		return await unitOfWork.AppointmentRepository.GetAppointmentById(id);
	}
	
	public Appointment Delete(Appointment appointment)
	{
		return unitOfWork.AppointmentRepository.Delete(appointment);
	}

    public async Task<List<AppointmentDto>> GetDtosWithoutPaging(AppointmentFilter filter)
    {
        return await unitOfWork.AppointmentRepository.GetDtosWithoutPaging(filter);
    }

    public async Task<List<Appointment>> GetAppointmentsWithoutPaging(AppointmentFilter filter)
    {
        return await unitOfWork.AppointmentRepository.GetAppointmentsWithoutPaging(filter);
    }
}
