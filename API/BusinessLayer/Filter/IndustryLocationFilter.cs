using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class IndustryLocationFilter : PaginationParams
{
    public IQueryable<IndustryLocation> ApplyFilter(IQueryable<IndustryLocation> query)
    {
        
		if (IndustryId != null) {
			query = query.Where(e => e.IndustryId == IndustryId);
		}
		if (MaxIndustryId != null) {
			query = query.Where(e => e.IndustryId <= MaxIndustryId);
		}
		if (MinIndustryId != null) {
			query = query.Where(e => e.IndustryId >= MinIndustryId);
		}
		if (ListIndustryId != null) {
			query = query.Where(e => ListIndustryId.Contains(e.IndustryId));
		}

		if (LocationId != null) {
			query = query.Where(e => e.LocationId == LocationId);
		}
		if (MaxLocationId != null) {
			query = query.Where(e => e.LocationId <= MaxLocationId);
		}
		if (MinLocationId != null) {
			query = query.Where(e => e.LocationId >= MinLocationId);
		}
		if (ListLocationId != null) {
			query = query.Where(e => ListLocationId.Contains(e.LocationId));
		}

		if (Rate != null) {
			query = query.Where(e => e.Rate == Rate);
		}
		if (MaxRate != null) {
			query = query.Where(e => e.Rate <= MaxRate);
		}
		if (MinRate != null) {
			query = query.Where(e => e.Rate >= MinRate);
		}
		if (ListRate != null) {
			query = query.Where(e => ListRate.Contains(e.Rate));
		}

		
		switch (OrderBy)
        {
            
			case IndustryIdAscending:
                query = query.OrderBy(c => c.IndustryId);
                break;
            case IndustryIdDescending:
                query = query.OrderByDescending(c => c.IndustryId);
                break;

			case LocationIdAscending:
                query = query.OrderBy(c => c.LocationId);
                break;
            case LocationIdDescending:
                query = query.OrderByDescending(c => c.LocationId);
                break;

			case RateAscending:
                query = query.OrderBy(c => c.Rate);
                break;
            case RateDescending:
                query = query.OrderByDescending(c => c.Rate);
                break;

        }
		return query;
    }
	
	public int? OrderBy { get; set; }
	
	public const int IndustryIdAscending = 1;
	public const int IndustryIdDescending = 1 + 1;

	public const int LocationIdAscending = 3;
	public const int LocationIdDescending = 3 + 1;

	public const int RateAscending = 5;
	public const int RateDescending = 5 + 1;

	
	public decimal? IndustryId { get; set; }
	public decimal? MaxIndustryId { get; set; }
	public decimal? MinIndustryId { get; set; }
	public IEnumerable<decimal?>? ListIndustryId { get; set; }

	public decimal? LocationId { get; set; }
	public decimal? MaxLocationId { get; set; }
	public decimal? MinLocationId { get; set; }
	public IEnumerable<decimal?>? ListLocationId { get; set; }

	public int? Rate { get; set; }
	public int? MaxRate { get; set; }
	public int? MinRate { get; set; }
	public IEnumerable<int?>? ListRate { get; set; }

}

