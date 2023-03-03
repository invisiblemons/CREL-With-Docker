using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class LocationFilter : PaginationParams
{
    public IQueryable<Location> ApplyFilter(IQueryable<Location> query)
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

		if (PlaceId != null) {
			query = query.Where(e => (e.PlaceId != null) && (e.PlaceId.Contains(PlaceId)));
		}

		if (Address != null) {
			query = query.Where(e => (e.Address != null) && (e.Address.Contains(Address)));
		}

		if (WardId != null) {
			query = query.Where(e => e.WardId == WardId);
		}
		if (MaxWardId != null) {
			query = query.Where(e => e.WardId <= MaxWardId);
		}
		if (MinWardId != null) {
			query = query.Where(e => e.WardId >= MinWardId);
		}
		if (ListWardId != null) {
			query = query.Where(e => ListWardId.Contains(e.WardId));
		}

		if (StreetSegmentId != null) {
			query = query.Where(e => e.StreetSegmentId == StreetSegmentId);
		}
		if (MaxStreetSegmentId != null) {
			query = query.Where(e => e.StreetSegmentId <= MaxStreetSegmentId);
		}
		if (MinStreetSegmentId != null) {
			query = query.Where(e => e.StreetSegmentId >= MinStreetSegmentId);
		}
		if (ListStreetSegmentId != null) {
			query = query.Where(e => ListStreetSegmentId.Contains(e.StreetSegmentId));
		}

		if (Latitude != null) {
			query = query.Where(e => e.Latitude == Latitude);
		}
		if (MaxLatitude != null) {
			query = query.Where(e => e.Latitude <= MaxLatitude);
		}
		if (MinLatitude != null) {
			query = query.Where(e => e.Latitude >= MinLatitude);
		}
		if (ListLatitude != null) {
			query = query.Where(e => ListLatitude.Contains(e.Latitude));
		}

		if (Longitude != null) {
			query = query.Where(e => e.Longitude == Longitude);
		}
		if (MaxLongitude != null) {
			query = query.Where(e => e.Longitude <= MaxLongitude);
		}
		if (MinLongitude != null) {
			query = query.Where(e => e.Longitude >= MinLongitude);
		}
		if (ListLongitude != null) {
			query = query.Where(e => ListLongitude.Contains(e.Longitude));
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

			case PlaceIdAscending:
                query = query.OrderBy(c => c.PlaceId);
                break;
            case PlaceIdDescending:
                query = query.OrderByDescending(c => c.PlaceId);
                break;

			case AddressAscending:
                query = query.OrderBy(c => c.Address);
                break;
            case AddressDescending:
                query = query.OrderByDescending(c => c.Address);
                break;

			case WardIdAscending:
                query = query.OrderBy(c => c.WardId);
                break;
            case WardIdDescending:
                query = query.OrderByDescending(c => c.WardId);
                break;

			case StreetSegmentIdAscending:
                query = query.OrderBy(c => c.StreetSegmentId);
                break;
            case StreetSegmentIdDescending:
                query = query.OrderByDescending(c => c.StreetSegmentId);
                break;

			case LatitudeAscending:
                query = query.OrderBy(c => c.Latitude);
                break;
            case LatitudeDescending:
                query = query.OrderByDescending(c => c.Latitude);
                break;

			case LongitudeAscending:
                query = query.OrderBy(c => c.Longitude);
                break;
            case LongitudeDescending:
                query = query.OrderByDescending(c => c.Longitude);
                break;

        }
		return query;
    }
	
	public int? OrderBy { get; set; }
	
	public const int IdAscending = 1;
	public const int IdDescending = 1 + 1;

	public const int StatusAscending = 3;
	public const int StatusDescending = 3 + 1;

	public const int PlaceIdAscending = 5;
	public const int PlaceIdDescending = 5 + 1;

	public const int AddressAscending = 7;
	public const int AddressDescending = 7 + 1;

	public const int WardIdAscending = 9;
	public const int WardIdDescending = 9 + 1;

	public const int StreetSegmentIdAscending = 11;
	public const int StreetSegmentIdDescending = 11 + 1;

	public const int LatitudeAscending = 13;
	public const int LatitudeDescending = 13 + 1;

	public const int LongitudeAscending = 15;
	public const int LongitudeDescending = 15 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public int? Status { get; set; }
	public int? MaxStatus { get; set; }
	public int? MinStatus { get; set; }
	public IEnumerable<int?>? ListStatus { get; set; }

	public string? PlaceId { get; set; }

	public string? Address { get; set; }

	public decimal? WardId { get; set; }
	public decimal? MaxWardId { get; set; }
	public decimal? MinWardId { get; set; }
	public IEnumerable<decimal?>? ListWardId { get; set; }

	public decimal? StreetSegmentId { get; set; }
	public decimal? MaxStreetSegmentId { get; set; }
	public decimal? MinStreetSegmentId { get; set; }
	public IEnumerable<decimal?>? ListStreetSegmentId { get; set; }

	public double? Latitude { get; set; }
	public double? MaxLatitude { get; set; }
	public double? MinLatitude { get; set; }
	public IEnumerable<double?>? ListLatitude { get; set; }

	public double? Longitude { get; set; }
	public double? MaxLongitude { get; set; }
	public double? MinLongitude { get; set; }
	public IEnumerable<double?>? ListLongitude { get; set; }

}

