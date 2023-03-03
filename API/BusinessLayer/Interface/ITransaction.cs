namespace CREL_BE.Services;

public interface IUnitOfWorkService
{
	IAppointmentService AppointmentService { get; }
	IAreaManagerService AreaManagerService { get; }
	IAreaManagerTeamService AreaManagerTeamService { get; }
	IBrandService BrandService { get; }
	IBrandRequestService BrandRequestService { get; }
	IBrokerService BrokerService { get; }
	IContractService ContractService { get; }
	IContractTermService ContractTermService { get; }
	IDistrictService DistrictService { get; }
	IIndustryService IndustryService { get; }
	IIndustryLocationService IndustryLocationService { get; }
	ILocationService LocationService { get; }
	IMediaService MediaService { get; }
	IOwnerService OwnerService { get; }
	IProjectService ProjectService { get; }
	IPropertyService PropertyService { get; }
	IPropertyBrandService PropertyBrandService { get; }
	IStoreService StoreService { get; }
	IStreetSegmentService StreetSegmentService { get; }
	ITeamService TeamService { get; }
	IWardService WardService { get; }

	
    Task CommitAsync();
}