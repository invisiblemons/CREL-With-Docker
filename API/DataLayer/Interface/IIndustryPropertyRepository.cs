using CREL_BE.Entities;

namespace CREL_BE.Repositories;

public interface IIndustryLocationRepository
{
	Task<IndustryLocation> Add(IndustryLocation industryProperty);
	Task<IndustryLocation?> GetIndustryLocationByLocationIdIndustryId(decimal propertyId,decimal industryId);
	IndustryLocation Delete(IndustryLocation industryProperty);
}
