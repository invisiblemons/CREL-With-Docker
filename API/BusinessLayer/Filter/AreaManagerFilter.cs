using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class AreaManagerFilter : PaginationParams
{
    public IQueryable<AreaManager> ApplyFilter(IQueryable<AreaManager> query)
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

		if (UserName != null) {
			query = query.Where(e => (e.UserName != null) && (e.UserName.Contains(UserName)));
		}

		if (PasswordHash != null) {
			query = query.Where(e => e.PasswordHash == PasswordHash);
		}

		if (PasswordSalt != null) {
			query = query.Where(e => e.PasswordSalt == PasswordSalt);
		}

		if (Birthday != null) {
			query = query.Where(e => e.Birthday == Birthday);
		}
		if (MaxBirthday != null) {
			query = query.Where(e => e.Birthday <= MaxBirthday);
		}
		if (MinBirthday != null) {
			query = query.Where(e => e.Birthday >= MinBirthday);
		}
		if (ListBirthday != null) {
			query = query.Where(e => ListBirthday.Contains(e.Birthday));
		}

		if (Gender != null) {
			query = query.Where(e => e.Gender == Gender);
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

		if (AvatarFileId != null) {
			query = query.Where(e => (e.AvatarFileId != null) && (e.AvatarFileId.Contains(AvatarFileId)));
		}

		if (AvatarLink != null) {
			query = query.Where(e => (e.AvatarLink != null) && (e.AvatarLink.Contains(AvatarLink)));
		}

		if (PhoneNumber != null) {
			query = query.Where(e => (e.PhoneNumber != null) && (e.PhoneNumber.Contains(PhoneNumber)));
		}

		if (Email != null) {
			query = query.Where(e => (e.Email != null) && (e.Email.Contains(Email)));
		}

		
		switch (OrderBy)
        {
            
			case IdAscending:
                query = query.OrderBy(c => c.Id);
                break;
            case IdDescending:
                query = query.OrderByDescending(c => c.Id);
                break;

			case UserNameAscending:
                query = query.OrderBy(c => c.UserName);
                break;
            case UserNameDescending:
                query = query.OrderByDescending(c => c.UserName);
                break;

			case PasswordHashAscending:
                query = query.OrderBy(c => c.PasswordHash);
                break;
            case PasswordHashDescending:
                query = query.OrderByDescending(c => c.PasswordHash);
                break;

			case PasswordSaltAscending:
                query = query.OrderBy(c => c.PasswordSalt);
                break;
            case PasswordSaltDescending:
                query = query.OrderByDescending(c => c.PasswordSalt);
                break;

			case BirthdayAscending:
                query = query.OrderBy(c => c.Birthday);
                break;
            case BirthdayDescending:
                query = query.OrderByDescending(c => c.Birthday);
                break;

			case GenderAscending:
                query = query.OrderBy(c => c.Gender);
                break;
            case GenderDescending:
                query = query.OrderByDescending(c => c.Gender);
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

			case AvatarFileIdAscending:
                query = query.OrderBy(c => c.AvatarFileId);
                break;
            case AvatarFileIdDescending:
                query = query.OrderByDescending(c => c.AvatarFileId);
                break;

			case AvatarLinkAscending:
                query = query.OrderBy(c => c.AvatarLink);
                break;
            case AvatarLinkDescending:
                query = query.OrderByDescending(c => c.AvatarLink);
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

        }
		return query;
    }
	
	public int? OrderBy { get; set; }
	
	public const int IdAscending = 1;
	public const int IdDescending = 1 + 1;

	public const int UserNameAscending = 3;
	public const int UserNameDescending = 3 + 1;

	public const int PasswordHashAscending = 5;
	public const int PasswordHashDescending = 5 + 1;

	public const int PasswordSaltAscending = 7;
	public const int PasswordSaltDescending = 7 + 1;

	public const int BirthdayAscending = 9;
	public const int BirthdayDescending = 9 + 1;

	public const int GenderAscending = 11;
	public const int GenderDescending = 11 + 1;

	public const int NameAscending = 13;
	public const int NameDescending = 13 + 1;

	public const int StatusAscending = 15;
	public const int StatusDescending = 15 + 1;

	public const int AvatarFileIdAscending = 17;
	public const int AvatarFileIdDescending = 17 + 1;

	public const int AvatarLinkAscending = 19;
	public const int AvatarLinkDescending = 19 + 1;

	public const int PhoneNumberAscending = 21;
	public const int PhoneNumberDescending = 21 + 1;

	public const int EmailAscending = 23;
	public const int EmailDescending = 23 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public string? UserName { get; set; }

	public byte[]? PasswordHash { get; set; }

	public byte[]? PasswordSalt { get; set; }

	public DateTime? Birthday { get; set; }
	public DateTime? MaxBirthday { get; set; }
	public DateTime? MinBirthday { get; set; }
	public IEnumerable<DateTime?>? ListBirthday { get; set; }

	public bool? Gender { get; set; }

	public string? Name { get; set; }

	public int? Status { get; set; }
	public int? MaxStatus { get; set; }
	public int? MinStatus { get; set; }
	public IEnumerable<int?>? ListStatus { get; set; }

	public string? AvatarFileId { get; set; }

	public string? AvatarLink { get; set; }

	public string? PhoneNumber { get; set; }

	public string? Email { get; set; }

}

