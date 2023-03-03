using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class AreaManagerTeamFilter : PaginationParams
{
    public IQueryable<AreaManagerTeam> ApplyFilter(IQueryable<AreaManagerTeam> query)
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

		if (AreaManagerId != null) {
			query = query.Where(e => e.AreaManagerId == AreaManagerId);
		}
		if (MaxAreaManagerId != null) {
			query = query.Where(e => e.AreaManagerId <= MaxAreaManagerId);
		}
		if (MinAreaManagerId != null) {
			query = query.Where(e => e.AreaManagerId >= MinAreaManagerId);
		}
		if (ListAreaManagerId != null) {
			query = query.Where(e => ListAreaManagerId.Contains(e.AreaManagerId));
		}

		if (StartDate != null) {
			query = query.Where(e => e.StartDate == StartDate);
		}
		if (MaxStartDate != null) {
			query = query.Where(e => e.StartDate <= MaxStartDate);
		}
		if (MinStartDate != null) {
			query = query.Where(e => e.StartDate >= MinStartDate);
		}
		if (ListStartDate != null) {
			query = query.Where(e => ListStartDate.Contains(e.StartDate));
		}

		if (EndDate != null) {
			query = query.Where(e => e.EndDate == EndDate);
		}
		if (MaxEndDate != null) {
			query = query.Where(e => e.EndDate <= MaxEndDate);
		}
		if (MinEndDate != null) {
			query = query.Where(e => e.EndDate >= MinEndDate);
		}
		if (ListEndDate != null) {
			query = query.Where(e => ListEndDate.Contains(e.EndDate));
		}

		
		switch (OrderBy)
        {
            
			case IdAscending:
                query = query.OrderBy(c => c.Id);
                break;
            case IdDescending:
                query = query.OrderByDescending(c => c.Id);
                break;

			case TeamIdAscending:
                query = query.OrderBy(c => c.TeamId);
                break;
            case TeamIdDescending:
                query = query.OrderByDescending(c => c.TeamId);
                break;

			case AreaManagerIdAscending:
                query = query.OrderBy(c => c.AreaManagerId);
                break;
            case AreaManagerIdDescending:
                query = query.OrderByDescending(c => c.AreaManagerId);
                break;

			case StartDateAscending:
                query = query.OrderBy(c => c.StartDate);
                break;
            case StartDateDescending:
                query = query.OrderByDescending(c => c.StartDate);
                break;

			case EndDateAscending:
                query = query.OrderBy(c => c.EndDate);
                break;
            case EndDateDescending:
                query = query.OrderByDescending(c => c.EndDate);
                break;

        }
		return query;
    }
	
	public int? OrderBy { get; set; }
	
	public const int IdAscending = 1;
	public const int IdDescending = 1 + 1;

	public const int TeamIdAscending = 3;
	public const int TeamIdDescending = 3 + 1;

	public const int AreaManagerIdAscending = 5;
	public const int AreaManagerIdDescending = 5 + 1;

	public const int StartDateAscending = 7;
	public const int StartDateDescending = 7 + 1;

	public const int EndDateAscending = 9;
	public const int EndDateDescending = 9 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public decimal? TeamId { get; set; }
	public decimal? MaxTeamId { get; set; }
	public decimal? MinTeamId { get; set; }
	public IEnumerable<decimal?>? ListTeamId { get; set; }

	public decimal? AreaManagerId { get; set; }
	public decimal? MaxAreaManagerId { get; set; }
	public decimal? MinAreaManagerId { get; set; }
	public IEnumerable<decimal?>? ListAreaManagerId { get; set; }

	public DateTime? StartDate { get; set; }
	public DateTime? MaxStartDate { get; set; }
	public DateTime? MinStartDate { get; set; }
	public IEnumerable<DateTime?>? ListStartDate { get; set; }

	public DateTime? EndDate { get; set; }
	public DateTime? MaxEndDate { get; set; }
	public DateTime? MinEndDate { get; set; }
	public IEnumerable<DateTime?>? ListEndDate { get; set; }

}

