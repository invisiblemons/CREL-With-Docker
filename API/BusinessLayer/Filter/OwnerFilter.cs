using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class OwnerFilter : PaginationParams
{
    public IQueryable<Owner> ApplyFilter(IQueryable<Owner> query)
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

		if (PhoneNumber != null) {
			query = query.Where(e => (e.PhoneNumber != null) && (e.PhoneNumber.Contains(PhoneNumber)));
		}

		if (Email != null) {
			query = query.Where(e => (e.Email != null) && (e.Email.Contains(Email)));
		}

		if (Gender != null) {
			query = query.Where(e => e.Gender == Gender);
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

			case PhoneNumberAscending:
                query = query.OrderBy(c => c.PhoneNumber);
                break;
            case PhoneNumberDescending:
                query = query.OrderByDescending(c => c.PhoneNumber);
                break;

			case EmailAscending:
                query = query.OrderBy(c => c.Email);
                break;
            case EmailDescending:
                query = query.OrderByDescending(c => c.Email);
                break;

			case GenderAscending:
                query = query.OrderBy(c => c.Gender);
                break;
            case GenderDescending:
                query = query.OrderByDescending(c => c.Gender);
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

	public const int PhoneNumberAscending = 5;
	public const int PhoneNumberDescending = 5 + 1;

	public const int EmailAscending = 7;
	public const int EmailDescending = 7 + 1;

	public const int GenderAscending = 9;
	public const int GenderDescending = 9 + 1;

	public const int StatusAscending = 11;
	public const int StatusDescending = 11 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public string? Name { get; set; }

	public string? PhoneNumber { get; set; }

	public string? Email { get; set; }

	public bool? Gender { get; set; }

	public int? Status { get; set; }
	public int? MaxStatus { get; set; }
	public int? MinStatus { get; set; }
	public IEnumerable<int?>? ListStatus { get; set; }

}

