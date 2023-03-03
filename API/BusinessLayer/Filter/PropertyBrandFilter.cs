using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class PropertyBrandFilter : PaginationParams
{
    public IQueryable<PropertyBrand> ApplyFilter(IQueryable<PropertyBrand> query)
    {
        
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

		if (PropertyId != null) {
			query = query.Where(e => e.PropertyId == PropertyId);
		}
		if (MaxPropertyId != null) {
			query = query.Where(e => e.PropertyId <= MaxPropertyId);
		}
		if (MinPropertyId != null) {
			query = query.Where(e => e.PropertyId >= MinPropertyId);
		}
		if (ListPropertyId != null) {
			query = query.Where(e => ListPropertyId.Contains(e.PropertyId));
		}

		if (Type != null) {
			query = query.Where(e => e.Type == Type);
		}
		if (MaxType != null) {
			query = query.Where(e => e.Type <= MaxType);
		}
		if (MinType != null) {
			query = query.Where(e => e.Type >= MinType);
		}
		if (ListType != null) {
			query = query.Where(e => ListType.Contains(e.Type));
		}

		
		switch (OrderBy)
        {
            
			case BrandIdAscending:
                query = query.OrderBy(c => c.BrandId);
                break;
            case BrandIdDescending:
                query = query.OrderByDescending(c => c.BrandId);
                break;

			case PropertyIdAscending:
                query = query.OrderBy(c => c.PropertyId);
                break;
            case PropertyIdDescending:
                query = query.OrderByDescending(c => c.PropertyId);
                break;

			case TypeAscending:
                query = query.OrderBy(c => c.Type);
                break;
            case TypeDescending:
                query = query.OrderByDescending(c => c.Type);
                break;

        }
		return query;
    }
	
	public int? OrderBy { get; set; }
	
	public const int BrandIdAscending = 1;
	public const int BrandIdDescending = 1 + 1;

	public const int PropertyIdAscending = 3;
	public const int PropertyIdDescending = 3 + 1;

	public const int TypeAscending = 5;
	public const int TypeDescending = 5 + 1;

	
	public decimal? BrandId { get; set; }
	public decimal? MaxBrandId { get; set; }
	public decimal? MinBrandId { get; set; }
	public IEnumerable<decimal?>? ListBrandId { get; set; }

	public decimal? PropertyId { get; set; }
	public decimal? MaxPropertyId { get; set; }
	public decimal? MinPropertyId { get; set; }
	public IEnumerable<decimal?>? ListPropertyId { get; set; }

	public int? Type { get; set; }
	public int? MaxType { get; set; }
	public int? MinType { get; set; }
	public IEnumerable<int?>? ListType { get; set; }

}

