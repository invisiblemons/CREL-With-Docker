using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class WardFilter : PaginationParams
{
    public IQueryable<Ward> ApplyFilter(IQueryable<Ward> query)
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

		if (Name != null) {
			query = query.Where(e => (e.Name != null) && (e.Name.Contains(Name)));
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

		if (TeamId != null) {
			query = query.Where(e => e.TeamId == TeamId);
		}
		if (MaxTeamId != null) {
			query = query.Where(e => e.TeamId <= MaxTeamId);
		}
		if (MinTeamId != null) {
			query = query.Where(e => e.TeamId >= MinTeamId);
		}
		if (ListTeamId != null) {
			query = query.Where(e => ListTeamId.Contains(e.TeamId));
		}

		
		switch (OrderBy)
        {
            
			case IdAscending:
                query = query.OrderBy(c => c.Id);
                break;
            case IdDescending:
                query = query.OrderByDescending(c => c.Id);
                break;

			case DistrictIdAscending:
                query = query.OrderBy(c => c.DistrictId);
                break;
            case DistrictIdDescending:
                query = query.OrderByDescending(c => c.DistrictId);
                break;

			case NameAscending:
                query = query.OrderBy(c => c.Name);
                break;
            case NameDescending:
                query = query.OrderByDescending(c => c.Name);
                break;

			case StatusAscending:
                query = query.OrderBy(c => c.Status);
                break;
            case StatusDescending:
                query = query.OrderByDescending(c => c.Status);
                break;

			case TeamIdAscending:
                query = query.OrderBy(c => c.TeamId);
                break;
            case TeamIdDescending:
                query = query.OrderByDescending(c => c.TeamId);
                break;

        }
		return query;
    }
	
	public int? OrderBy { get; set; }
	
	public const int IdAscending = 1;
	public const int IdDescending = 1 + 1;

	public const int DistrictIdAscending = 3;
	public const int DistrictIdDescending = 3 + 1;

	public const int NameAscending = 5;
	public const int NameDescending = 5 + 1;

	public const int StatusAscending = 7;
	public const int StatusDescending = 7 + 1;

	public const int TeamIdAscending = 9;
	public const int TeamIdDescending = 9 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public decimal? DistrictId { get; set; }
	public decimal? MaxDistrictId { get; set; }
	public decimal? MinDistrictId { get; set; }
	public IEnumerable<decimal?>? ListDistrictId { get; set; }

	public string? Name { get; set; }

	public int? Status { get; set; }
	public int? MaxStatus { get; set; }
	public int? MinStatus { get; set; }
	public IEnumerable<int?>? ListStatus { get; set; }

	public decimal? TeamId { get; set; }
	public decimal? MaxTeamId { get; set; }
	public decimal? MinTeamId { get; set; }
	public IEnumerable<decimal?>? ListTeamId { get; set; }

}

