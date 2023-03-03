using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class DistrictFilter : PaginationParams
{
    public IQueryable<District> ApplyFilter(IQueryable<District> query)
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

		
		switch (OrderBy)
        {
            
			case IdAscending:
                query = query.OrderBy(c => c.Id);
                break;
            case IdDescending:
                query = query.OrderByDescending(c => c.Id);
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

        }
		return query;
    }
	
	public int? OrderBy { get; set; }
	
	public const int IdAscending = 1;
	public const int IdDescending = 1 + 1;

	public const int NameAscending = 3;
	public const int NameDescending = 3 + 1;

	public const int StatusAscending = 5;
	public const int StatusDescending = 5 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public string? Name { get; set; }

	public int? Status { get; set; }
	public int? MaxStatus { get; set; }
	public int? MinStatus { get; set; }
	public IEnumerable<int?>? ListStatus { get; set; }

}

