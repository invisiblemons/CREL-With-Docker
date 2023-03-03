using CREL_BE.Entities;
using CREL_BE.Filter;
using CREL_BE.Dtos;
using CREL_BE.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
	private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
	
	public AppointmentRepository(CRELDBContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<AppointmentDto?> GetAppointmentDtoById(decimal id)
	{
		return (await dbContext.Appointments
				.Where(entity => entity.Id == id)
				.ProjectTo<AppointmentDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
	}
	
    public Task<PagedList<AppointmentDto>> GetAppointmentDtos(AppointmentFilter appointmentFilter)
	{
		return PagedList<AppointmentDto>.CreateAsync(
			appointmentFilter.ApplyFilter(dbContext.Appointments)
			.ProjectTo<AppointmentDto>(mapper.ConfigurationProvider)
			.AsNoTracking()
			, appointmentFilter);
	}
	
	public async Task<Appointment> Add(Appointment appointment)
	{
		return (await dbContext.Appointments.AddAsync(appointment)).Entity;
	}
	
	public async Task<Appointment?> GetAppointmentById(decimal id)
	{
		return (await dbContext.Appointments.Include(entity => entity.Property).FirstOrDefaultAsync(entity => entity.Id == id));
	}
	
	public Appointment Delete(Appointment appointment)
	{
		return dbContext.Appointments.Remove(appointment).Entity;
	}

    public async Task<List<AppointmentDto>> GetDtosWithoutPaging(AppointmentFilter filter)
    {
        return await filter.ApplyFilter(dbContext.Appointments).ProjectTo<AppointmentDto>(mapper.ConfigurationProvider).AsNoTracking().ToListAsync();
    }

    public async Task<List<Appointment>> GetAppointmentsWithoutPaging(AppointmentFilter filter)
    {
        return await filter.ApplyFilter(dbContext.Appointments).ToListAsync();
    }
}
