using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class StoreFilter : PaginationParams
{
    public IQueryable<Store> ApplyFilter(IQueryable<Store> query)
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

		if (BrandId != null) {
			query = query.Where(e => e.BrandId == BrandId);
		}
		if (MaxBrandId != null) {
			query = query.Where(e => e.BrandId <= MaxBrandId);
		}
		if (MinBrandId != null) {
			query = query.Where(e => e.BrandId >= MinBrandId);
		}
		if (ListBrandId != null) {
			query = query.Where(e => ListBrandId.Contains(e.BrandId));
		}

		if (Address != null) {
			query = query.Where(e => (e.Address != null) && (e.Address.Contains(Address)));
		}

		if (Name != null) {
			query = query.Where(e => (e.Name != null) && (e.Name.Contains(Name)));
		}

		
		switch (OrderBy)
        {
            
			case IdAscending:
                query = query.OrderBy(c => c.Id);
                break;
            case IdDescending:
                query = query.OrderByDescending(c => c.Id);
                break;

			case BrandIdAscending:
                query = query.OrderBy(c => c.BrandId);
                break;
            case BrandIdDescending:
                query = query.OrderByDescending(c => c.BrandId);
                break;

			case AddressAscending:
                query = query.OrderBy(c => c.Address);
                break;
            case AddressDescending:
                query = query.OrderByDescending(c => c.Address);
                break;

			case NameAscending:
                query = query.OrderBy(c => c.Name);
                break;
            case NameDescending:
                query = query.OrderByDescending(c => c.Name);
                break;

        }
		return query;
    }
	
	public int? OrderBy { get; set; }
	
	public const int IdAscending = 1;
	public const int IdDescending = 1 + 1;

	public const int BrandIdAscending = 3;
	public const int BrandIdDescending = 3 + 1;

	public const int AddressAscending = 5;
	public const int AddressDescending = 5 + 1;

	public const int NameAscending = 7;
	public const int NameDescending = 7 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public decimal? BrandId { get; set; }
	public decimal? MaxBrandId { get; set; }
	public decimal? MinBrandId { get; set; }
	public IEnumerable<decimal?>? ListBrandId { get; set; }

	public string? Address { get; set; }

	public string? Name { get; set; }

}

