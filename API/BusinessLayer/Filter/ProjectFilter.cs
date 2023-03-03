using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class ProjectFilter : PaginationParams
{
    public IQueryable<Project> ApplyFilter(IQueryable<Project> query)
    {
        
		if (Id != null) {
			query = query.Where(e => e.Id == Id);
		}
		if (MaxId != null) {
			query = query.Where(e => e.Id <= MaxId);
		}
		if (MinId != null) {
			query = query.Where(e => e.Id >= MinId);
		}
		if (ListId != null) {
			query = query.Where(e => ListId.Contains(e.Id));
		}

		if (Status != null) {
			query = query.Where(e => e.Status == Status);
		}
		if (MaxStatus != null) {
			query = query.Where(e => e.Status <= MaxStatus);
		}
		if (MinStatus != null) {
			query = query.Where(e => e.Status >= MinStatus);
		}
		if (ListStatus != null) {
			query = query.Where(e => ListStatus.Contains(e.Status));
		}

		if (Description != null) {
			query = query.Where(e => (e.Description != null) && (e.Description.Contains(Description)));
		}

		if (Name != null) {
			query = query.Where(e => (e.Name != null) && (e.Name.Contains(Name)));
		}

		if (DistrictId != null) {
			query = query.Where(e => e.DistrictId == DistrictId);
		}
		if (MaxDistrictId != null) {
			query = query.Where(e => e.DistrictId <= MaxDistrictId);
		}
		if (MinDistrictId != null) {
			query = query.Where(e => e.DistrictId >= MinDistrictId);
		}
		if (ListDistrictId != null) {
			query = query.Where(e => ListDistrictId.Contains(e.DistrictId));
		}

		if (HandoverYear != null) {
			query = query.Where(e => e.HandoverYear == HandoverYear);
		}
		if (MaxHandoverYear != null) {
			query = query.Where(e => e.HandoverYear <= MaxHandoverYear);
		}
		if (MinHandoverYear != null) {
			query = query.Where(e => e.HandoverYear >= MinHandoverYear);
		}
		if (ListHandoverYear != null) {
			query = query.Where(e => ListHandoverYear.Contains(e.HandoverYear));
		}

		if (Company != null) {
			query = query.Where(e => (e.Company != null) && (e.Company.Contains(Company)));
		}

		
		switch (OrderBy)
        {
            
			case IdAscending:
                query = query.OrderBy(c => c.Id);
                break;
            case IdDescending:
                query = query.OrderByDescending(c => c.Id);
                break;

			case StatusAscending:
                query = query.OrderBy(c => c.Status);
                break;
            case StatusDescending:
                query = query.OrderByDescending(c => c.Status);
                break;

			case DescriptionAscending:
                query = query.OrderBy(c => c.Description);
                break;
            case DescriptionDescending:
                query = query.OrderByDescending(c => c.Description);
                break;

			case NameAscending:
                query = query.OrderBy(c => c.Name);
                break;
            case NameDescending:
                query = query.OrderByDescending(c => c.Name);
                break;

			case DistrictIdAscending:
                query = query.OrderBy(c => c.DistrictId);
                break;
            case DistrictIdDescending:
                query = query.OrderByDescending(c => c.DistrictId);
                break;

			case HandoverYearAscending:
                query = query.OrderBy(c => c.HandoverYear);
                break;
            case HandoverYearDescending:
                query = query.OrderByDescending(c => c.HandoverYear);
                break;

			case CompanyAscending:
                query = query.OrderBy(c => c.Company);
                break;
            case CompanyDescending:
                query = query.OrderByDescending(c => c.Company);
                break;

        }
		return query;
    }
	
	public int? OrderBy { get; set; }
	
	public const int IdAscending = 1;
	public const int IdDescending = 1 + 1;

	public const int StatusAscending = 3;
	public const int StatusDescending = 3 + 1;

	public const int DescriptionAscending = 5;
	public const int DescriptionDescending = 5 + 1;

	public const int NameAscending = 7;
	public const int NameDescending = 7 + 1;

	public const int DistrictIdAscending = 9;
	public const int DistrictIdDescending = 9 + 1;

	public const int HandoverYearAscending = 11;
	public const int HandoverYearDescending = 11 + 1;

	public const int CompanyAscending = 13;
	public const int CompanyDescending = 13 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public int? Status { get; set; }
	public int? MaxStatus { get; set; }
	public int? MinStatus { get; set; }
	public IEnumerable<int?>? ListStatus { get; set; }

	public string? Description { get; set; }

	public string? Name { get; set; }

	public decimal? DistrictId { get; set; }
	public decimal? MaxDistrictId { get; set; }
	public decimal? MinDistrictId { get; set; }
	public IEnumerable<decimal?>? ListDistrictId { get; set; }

	public DateTime? HandoverYear { get; set; }
	public DateTime? MaxHandoverYear { get; set; }
	public DateTime? MinHandoverYear { get; set; }
	public IEnumerable<DateTime?>? ListHandoverYear { get; set; }

	public string? Company { get; set; }

}

