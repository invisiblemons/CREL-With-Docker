using AutoMapper;
using CREL_BE.Entities;
using CREL_BE.Dtos;

namespace CREL_BE.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
		//Appointment
		CreateMap<Appointment, AppointmentDto>();
		CreateMap<CreateAppointmentDto, Appointment>();
		CreateMap<UpdateAppointmentDto, Appointment>();
				
		//AreaManager
		CreateMap<AreaManager, AreaManagerDto>();
		CreateMap<CreateAreaManagerDto, AreaManager>();
		CreateMap<UpdateAreaManagerDto, AreaManager>();

		//AreaManagerTeam
		CreateMap<AreaManagerTeam, AreaManagerTeamDto>();
				
		//Brand
		CreateMap<Brand, BrandDto>();
		CreateMap<CreateBrandDto, Brand>();
		CreateMap<UpdateBrandDto, Brand>();
		CreateMap<BrandProfileDto, Brand>();
		CreateMap<Brand, IdNameDto>();
				
		//BrandRequest
		CreateMap<BrandRequest, BrandRequestDto>();
		CreateMap<CreateBrandRequestDto, BrandRequest>();
		CreateMap<UpdateBrandRequestDto, BrandRequest>();
				
		//Broker
		CreateMap<Broker, BrokerDto>();
		CreateMap<CreateBrokerDto, Broker>();
		CreateMap<UpdateBrokerDto, Broker>();
		CreateMap<Broker, IdNameDto>();
		CreateMap<Broker, BrokerAppoinmentCount>()
		.ForMember(bac => bac.AppoinmentCount, o => o.MapFrom(bac => bac.Appointments.Count(a => a.Status == MyConstant.Appointment.Status.Waiting)));
				
		//Owner
		CreateMap<Owner, OwnerDto>();
		CreateMap<Owner, IdNameDto>();
		CreateMap<CreateOwnerDto, Owner>();
		CreateMap<UpdateOwnerDto, Owner>();
				
		//Contract
		CreateMap<Contract, ContractDto>();
		CreateMap<CreateContractDto, Contract>();
		CreateMap<UpdateContractDto, Contract>();
				
		//ContractTerm
		CreateMap<ContractTerm, ContractTermDto>();
		CreateMap<CreateContractTermDto, ContractTerm>();
		CreateMap<CreateFirstLevelContractTermDto, ContractTerm>();
		CreateMap<CreateSecondLevelContractTermDto, ContractTerm>();
		CreateMap<UpdateContractTermDto, ContractTerm>();
		CreateMap<ContractTerm, CreateFirstLevelContractTermDto>();
		CreateMap<ContractTerm, CreateSecondLevelContractTermDto>();
				
		//District
		CreateMap<District, DistrictDto>();
		CreateMap<District, IdNameDto>();
		CreateMap<CreateDistrictDto, District>();
		CreateMap<UpdateDistrictDto, District>();
				
		//Industry
		CreateMap<Industry, IndustryDto>();
		CreateMap<CreateIndustryDto, Industry>();
		CreateMap<UpdateIndustryDto, Industry>();
				
		//IndustryProperty
		CreateMap<IndustryLocation, IndustryLocationDto>();
				
		//Location
		CreateMap<Location, LocationDto>();
		CreateMap<Location, LocationDtoForTeamDto>();
		CreateMap<CreateLocationDto, Location>();
		CreateMap<UpdateLocationDto, Location>();
				
		//Media
		CreateMap<Media, MediaDto>();
		CreateMap<Media, ShortMediaDto>();
				
		//Project
		CreateMap<Project, ProjectDto>();
		CreateMap<CreateProjectDto, Project>();
		CreateMap<UpdateProjectDto, Project>();
		CreateMap<Project, IdNameDto>();
				
		//Property
		CreateMap<Property, PropertyDto>();
		CreateMap<CreatePropertyDto, Property>();
		CreateMap<UpdatePropertyDto, Property>();
		CreateMap<Property, IdNameDto>();
				
		//PropertyBrand
		CreateMap<PropertyBrand, PropertyBrandDto>();
		CreateMap<CreatePropertyBrandDto, PropertyBrand>();
		CreateMap<UpdatePropertyBrandDto, PropertyBrand>();
				
		//Store
		CreateMap<Store, StoreDto>();
		CreateMap<CreateStoreDto, Store>();
		CreateMap<UpdateStoreDto, Store>();
				
		//StreetSegment
		CreateMap<StreetSegment, StreetSegmentDto>();
		CreateMap<StreetSegment, IdNameDto>();
		CreateMap<CreateStreetSegmentDto, StreetSegment>();
		CreateMap<UpdateStreetSegmentDto, StreetSegment>();
				
		//Team
		CreateMap<Team, TeamDto>();
		CreateMap<Team, IdNameDto>();
		CreateMap<CreateTeamDto, Team>();
		CreateMap<UpdateTeamDto, Team>();
				
		//Ward
		CreateMap<Ward, WardDtWardDtoForTeamDtoo>();
		CreateMap<Ward, WardDto>();
		CreateMap<Ward, IdNameDto>();
		CreateMap<CreateWardDto, Ward>();
		CreateMap<UpdateWardDto, Ward>();
		
		CreateMap<ContractDto, ContractInformationForExport>();
    }
}
