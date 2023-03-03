using AutoMapper;
using CREL_BE.Entities;
using CREL_BE.Repositories;

namespace CREL_BE.Services;

public class UnitOfWorkService : IUnitOfWorkService
{
    private readonly IUnitOfWorkRepository unitOfWork;
    private readonly IMapper mapper;
    public UnitOfWorkService(CRELDBContext dbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.unitOfWork = new UnitOfWorkRepository(dbContext, mapper);
    }

    public IAppointmentService AppointmentService => new AppointmentService(unitOfWork, mapper);
    public IAreaManagerService AreaManagerService => new AreaManagerService(unitOfWork, mapper);
    public IAreaManagerTeamService AreaManagerTeamService => new AreaManagerTeamService(unitOfWork, mapper);
    public IBrandService BrandService => new BrandService(unitOfWork, mapper);
    public IBrandRequestService BrandRequestService => new BrandRequestService(unitOfWork, mapper);
    public IBrokerService BrokerService => new BrokerService(unitOfWork, mapper);
    public IContractService ContractService => new ContractService(unitOfWork, mapper);
    public IContractTermService ContractTermService => new ContractTermService(unitOfWork, mapper);
    public IDistrictService DistrictService => new DistrictService(unitOfWork, mapper);
    public IIndustryService IndustryService => new IndustryService(unitOfWork, mapper);
    public IIndustryLocationService IndustryLocationService => new IndustryLocationService(unitOfWork, mapper);
    public ILocationService LocationService => new LocationService(unitOfWork, mapper);
    public IMediaService MediaService => new MediaService(unitOfWork, mapper);
    public IOwnerService OwnerService => new OwnerService(unitOfWork, mapper);
    public IProjectService ProjectService => new ProjectService(unitOfWork, mapper);
    public IPropertyService PropertyService => new PropertyService(unitOfWork, mapper);
    public IPropertyBrandService PropertyBrandService => new PropertyBrandService(unitOfWork, mapper);
    public IStoreService StoreService => new StoreService(unitOfWork, mapper);
    public IStreetSegmentService StreetSegmentService => new StreetSegmentService(unitOfWork, mapper);
    public ITeamService TeamService => new TeamService(unitOfWork, mapper);
    public IWardService WardService => new WardService(unitOfWork, mapper);

    public async Task CommitAsync()
    {
        await this.unitOfWork.CommitAsync();
    }
}
