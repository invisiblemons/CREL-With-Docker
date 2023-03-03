using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class AppointmentFilter : PaginationParams
{
    public IQueryable<Appointment> ApplyFilter(IQueryable<Appointment> query)
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

		if (BrokerId != null) {
			query = query.Where(e => e.BrokerId == BrokerId);
		}
		if (MaxBrokerId != null) {
			query = query.Where(e => e.BrokerId <= MaxBrokerId);
		}
		if (MinBrokerId != null) {
			query = query.Where(e => e.BrokerId >= MinBrokerId);
		}
		if (ListBrokerId != null) {
			query = query.Where(e => ListBrokerId.Contains(e.BrokerId));
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

		if (OnDateTime != null) {
			query = query.Where(e => e.OnDateTime == OnDateTime);
		}
		if (MaxOnDateTime != null) {
			query = query.Where(e => e.OnDateTime <= MaxOnDateTime);
		}
		if (MinOnDateTime != null) {
			query = query.Where(e => e.OnDateTime >= MinOnDateTime);
		}
		if (ListOnDateTime != null) {
			query = query.Where(e => ListOnDateTime.Contains(e.OnDateTime));
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

		if (CreateDateTime != null) {
			query = query.Where(e => e.CreateDateTime == CreateDateTime);
		}
		if (MaxCreateDateTime != null) {
			query = query.Where(e => e.CreateDateTime <= MaxCreateDateTime);
		}
		if (MinCreateDateTime != null) {
			query = query.Where(e => e.CreateDateTime >= MinCreateDateTime);
		}
		if (ListCreateDateTime != null) {
			query = query.Where(e => ListCreateDateTime.Contains(e.CreateDateTime));
		}

		if (RejectMessage != null) {
			query = query.Where(e => (e.RejectMessage != null) && (e.RejectMessage.Contains(RejectMessage)));
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

			case BrokerIdAscending:
                query = query.OrderBy(c => c.BrokerId);
                break;
            case BrokerIdDescending:
                query = query.OrderByDescending(c => c.BrokerId);
                break;

			case PropertyIdAscending:
                query = query.OrderBy(c => c.PropertyId);
                break;
            case PropertyIdDescending:
                query = query.OrderByDescending(c => c.PropertyId);
                break;

			case OnDateTimeAscending:
                query = query.OrderBy(c => c.OnDateTime);
                break;
            case OnDateTimeDescending:
                query = query.OrderByDescending(c => c.OnDateTime);
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

			case CreateDateTimeAscending:
                query = query.OrderBy(c => c.CreateDateTime);
                break;
            case CreateDateTimeDescending:
                query = query.OrderByDescending(c => c.CreateDateTime);
                break;

			case RejectMessageAscending:
                query = query.OrderBy(c => c.RejectMessage);
                break;
            case RejectMessageDescending:
                query = query.OrderByDescending(c => c.RejectMessage);
                break;

        }
		return query;
    }
	
	public int? OrderBy { get; set; }
	
	public const int IdAscending = 1;
	public const int IdDescending = 1 + 1;

	public const int BrandIdAscending = 3;
	public const int BrandIdDescending = 3 + 1;

	public const int BrokerIdAscending = 5;
	public const int BrokerIdDescending = 5 + 1;

	public const int PropertyIdAscending = 7;
	public const int PropertyIdDescending = 7 + 1;

	public const int OnDateTimeAscending = 9;
	public const int OnDateTimeDescending = 9 + 1;

	public const int StatusAscending = 11;
	public const int StatusDescending = 11 + 1;

	public const int DescriptionAscending = 13;
	public const int DescriptionDescending = 13 + 1;

	public const int CreateDateTimeAscending = 15;
	public const int CreateDateTimeDescending = 15 + 1;

	public const int RejectMessageAscending = 17;
	public const int RejectMessageDescending = 17 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public decimal? BrandId { get; set; }
	public decimal? MaxBrandId { get; set; }
	public decimal? MinBrandId { get; set; }
	public IEnumerable<decimal?>? ListBrandId { get; set; }

	public decimal? BrokerId { get; set; }
	public decimal? MaxBrokerId { get; set; }
	public decimal? MinBrokerId { get; set; }
	public IEnumerable<decimal?>? ListBrokerId { get; set; }

	public decimal? PropertyId { get; set; }
	public decimal? MaxPropertyId { get; set; }
	public decimal? MinPropertyId { get; set; }
	public IEnumerable<decimal?>? ListPropertyId { get; set; }

	public DateTime? OnDateTime { get; set; }
	public DateTime? MaxOnDateTime { get; set; }
	public DateTime? MinOnDateTime { get; set; }
	public IEnumerable<DateTime?>? ListOnDateTime { get; set; }

	public int? Status { get; set; }
	public int? MaxStatus { get; set; }
	public int? MinStatus { get; set; }
	public IEnumerable<int?>? ListStatus { get; set; }

	public string? Description { get; set; }

	public DateTime? CreateDateTime { get; set; }
	public DateTime? MaxCreateDateTime { get; set; }
	public DateTime? MinCreateDateTime { get; set; }
	public IEnumerable<DateTime?>? ListCreateDateTime { get; set; }

	public string? RejectMessage { get; set; }

}

