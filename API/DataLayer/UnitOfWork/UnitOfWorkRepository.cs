using AutoMapper;
using CREL_BE.Entities;

namespace CREL_BE.Repositories;

public class UnitOfWorkRepository : IUnitOfWorkRepository
{
    private readonly CRELDBContext dbContext;
    private readonly IMapper mapper;
    public UnitOfWorkRepository(CRELDBContext dbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }
	
	public IAppointmentRepository AppointmentRepository => new AppointmentRepository(dbContext, mapper);
	public IAreaManagerRepository AreaManagerRepository => new AreaManagerRepository(dbContext, mapper);
	public IAreaManagerTeamRepository AreaManagerTeamRepository => new AreaManagerTeamRepository(dbContext, mapper);
	public IBrandRepository BrandRepository => new BrandRepository(dbContext, mapper);
	public IBrandRequestRepository BrandRequestRepository => new BrandRequestRepository(dbContext, mapper);
	public IBrokerRepository BrokerRepository => new BrokerRepository(dbContext, mapper);
	public IContractRepository ContractRepository => new ContractRepository(dbContext, mapper);
	public IContractTermRepository ContractTermRepository => new ContractTermRepository(dbContext, mapper);
	public IDistrictRepository DistrictRepository => new DistrictRepository(dbContext, mapper);
	public IIndustryRepository IndustryRepository => new IndustryRepository(dbContext, mapper);
	public IIndustryLocationRepository IndustryLocationRepository => new IndustryLocationRepository(dbContext, mapper);
	public ILocationRepository LocationRepository => new LocationRepository(dbContext, mapper);
	public IMediaRepository MediaRepository => new MediaRepository(dbContext, mapper);
	public IOwnerRepository OwnerRepository => new OwnerRepository(dbContext, mapper);
	public IProjectRepository ProjectRepository => new ProjectRepository(dbContext, mapper);
	public IPropertyRepository PropertyRepository => new PropertyRepository(dbContext, mapper);
	public IPropertyBrandRepository PropertyBrandRepository => new PropertyBrandRepository(dbContext, mapper);
	public IStoreRepository StoreRepository => new StoreRepository(dbContext, mapper);
	public IStreetSegmentRepository StreetSegmentRepository => new StreetSegmentRepository(dbContext, mapper);
	public ITeamRepository TeamRepository => new TeamRepository(dbContext, mapper);
	public IWardRepository WardRepository => new WardRepository(dbContext, mapper);

    public async Task CommitAsync()
    {
        await this.dbContext.SaveChangesAsync();
    }
}
