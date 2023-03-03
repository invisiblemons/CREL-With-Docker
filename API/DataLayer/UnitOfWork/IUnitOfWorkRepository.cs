namespace CREL_BE.Repositories;

public interface IUnitOfWorkRepository
{
	IAppointmentRepository AppointmentRepository { get; }
	IAreaManagerRepository AreaManagerRepository { get; }
	IAreaManagerTeamRepository AreaManagerTeamRepository { get; }
	IBrandRepository BrandRepository { get; }
	IBrandRequestRepository BrandRequestRepository { get; }
	IBrokerRepository BrokerRepository { get; }
	IContractRepository ContractRepository { get; }
	IContractTermRepository ContractTermRepository { get; }
	IDistrictRepository DistrictRepository { get; }
	IIndustryRepository IndustryRepository { get; }
	IIndustryLocationRepository IndustryLocationRepository { get; }
	ILocationRepository LocationRepository { get; }
	IMediaRepository MediaRepository { get; }
	IOwnerRepository OwnerRepository { get; }
	IProjectRepository ProjectRepository { get; }
	IPropertyRepository PropertyRepository { get; }
	IPropertyBrandRepository PropertyBrandRepository { get; }
	IStoreRepository StoreRepository { get; }
	IStreetSegmentRepository StreetSegmentRepository { get; }
	ITeamRepository TeamRepository { get; }
	IWardRepository WardRepository { get; }

	
    Task CommitAsync();
}