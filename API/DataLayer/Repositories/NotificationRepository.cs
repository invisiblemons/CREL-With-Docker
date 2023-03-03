// using CREL_BE.Entities;
// using CREL_BE.Filter;
// using CREL_BE.Dtos;
// using CREL_BE.Helpers;
// using AutoMapper;
// using AutoMapper.QueryableExtensions;
// using Microsoft.EntityFrameworkCore;

// namespace CREL_BE.Repositories;

// public class NotificationRepository : INotificationRepository
// {
// 	private readonly CRELDBContext dbContext;
//     private readonly IMapper mapper;
	
// 	public NotificationRepository(CRELDBContext dbContext, IMapper mapper)
// 	{
// 		this.dbContext = dbContext;
// 		this.mapper = mapper;
// 	}

// 	public async Task<NotificationDto?> GetNotificationDtoById(decimal id)
// 	{
// 		return (await dbContext.Notifications
// 				.Where(entity => entity.Id == id)
// 				.ProjectTo<NotificationDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync());
// 	}
	
//     public Task<PagedList<NotificationDto>> GetNotificationDtos(NotificationFilter notificationFilter)
// 	{
// 		return PagedList<NotificationDto>.CreateAsync(
// 			notificationFilter.ApplyFilter(dbContext.Notifications)
// 			.ProjectTo<NotificationDto>(mapper.ConfigurationProvider)
// 			.AsNoTracking()
// 			, notificationFilter);
// 	}
	
// 	public async Task<Notification> Add(Notification notification)
// 	{
// 		return (await dbContext.Notifications.AddAsync(notification)).Entity;
// 	}
	
// 	public async Task<Notification?> GetNotificationById(decimal id)
// 	{
// 		return (await dbContext.Notifications.FirstOrDefaultAsync(entity => entity.Id == id));
// 	}
	
// 	public Notification Delete(Notification notification)
// 	{
// 		return dbContext.Notifications.Remove(notification).Entity;
// 	}
	
// 	public async Task<List<NotificationDto>> GetDtosWithoutPaging(NotificationFilter filter)
//     {
//         return await filter.ApplyFilter(dbContext.Notifications).ProjectTo<NotificationDto>(mapper.ConfigurationProvider).AsNoTracking().ToListAsync();
//     }
// }
