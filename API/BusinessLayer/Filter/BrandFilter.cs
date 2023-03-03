using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class BrandFilter : PaginationParams
{
    public IQueryable<Brand> ApplyFilter(IQueryable<Brand> query)
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
			query = query.Where(e => (e.UserName != null) && (e.UserName==UserName));
		}

		if (PasswordHash != null) {
			query = query.Where(e => e.PasswordHash == PasswordHash);
		}

		if (PasswordSalt != null) {
			query = query.Where(e => e.PasswordSalt == PasswordSalt);
		}

		if (Name != null) {
			query = query.Where(e => (e.Name != null) && (e.Name.Contains(Name)));
		}

		if (FirebaseUid != null) {
			query = query.Where(e => (e.FirebaseUid != null) && (e.FirebaseUid.Contains(FirebaseUid)));
		}

		if (Email != null) {
			query = query.Where(e => (e.Email != null) && (e.Email.Contains(Email)));
		}

		if (PhoneNumber != null) {
			query = query.Where(e => (e.PhoneNumber != null) && (e.PhoneNumber.Contains(PhoneNumber)));
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

		if (RejectMessage != null) {
			query = query.Where(e => (e.RejectMessage != null) && (e.RejectMessage.Contains(RejectMessage)));
		}

		if (AvatarFileId != null) {
			query = query.Where(e => (e.AvatarFileId != null) && (e.AvatarFileId.Contains(AvatarFileId)));
		}

		if (AvatarLink != null) {
			query = query.Where(e => (e.AvatarLink != null) && (e.AvatarLink.Contains(AvatarLink)));
		}

		if (IndustryId != null) {
			query = query.Where(e => e.IndustryId == IndustryId);
		}
		if (MaxIndustryId != null) {
			query = query.Where(e => e.IndustryId <= MaxIndustryId);
		}
		if (MinIndustryId != null) {
			query = query.Where(e => e.IndustryId >= MinIndustryId);
		}
		if (ListIndustryId != null) {
			query = query.Where(e => ListIndustryId.Contains(e.IndustryId));
		}

		if (RegistrationNumber != null) {
			query = query.Where(e => (e.RegistrationNumber != null) && (e.RegistrationNumber.Contains(RegistrationNumber)));
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

			case NameAscending:
                query = query.OrderBy(c => c.Name);
                break;
            case NameDescending:
                query = query.OrderByDescending(c => c.Name);
                break;

			case FirebaseUidAscending:
                query = query.OrderBy(c => c.FirebaseUid);
                break;
            case FirebaseUidDescending:
                query = query.OrderByDescending(c => c.FirebaseUid);
                break;

			case EmailAscending:
                query = query.OrderBy(c => c.Email);
                break;
            case EmailDescending:
                query = query.OrderByDescending(c => c.Email);
                break;

			case PhoneNumberAscending:
                query = query.OrderBy(c => c.PhoneNumber);
                break;
            case PhoneNumberDescending:
                query = query.OrderByDescending(c => c.PhoneNumber);
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

			case RejectMessageAscending:
                query = query.OrderBy(c => c.RejectMessage);
                break;
            case RejectMessageDescending:
                query = query.OrderByDescending(c => c.RejectMessage);
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

			case IndustryIdAscending:
                query = query.OrderBy(c => c.IndustryId);
                break;
            case IndustryIdDescending:
                query = query.OrderByDescending(c => c.IndustryId);
                break;

			case RegistrationNumberAscending:
                query = query.OrderBy(c => c.RegistrationNumber);
                break;
            case RegistrationNumberDescending:
                query = query.OrderByDescending(c => c.RegistrationNumber);
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

	public const int NameAscending = 9;
	public const int NameDescending = 9 + 1;

	public const int FirebaseUidAscending = 11;
	public const int FirebaseUidDescending = 11 + 1;

	public const int EmailAscending = 13;
	public const int EmailDescending = 13 + 1;

	public const int PhoneNumberAscending = 15;
	public const int PhoneNumberDescending = 15 + 1;

	public const int StatusAscending = 17;
	public const int StatusDescending = 17 + 1;

	public const int DescriptionAscending = 19;
	public const int DescriptionDescending = 19 + 1;

	public const int RejectMessageAscending = 21;
	public const int RejectMessageDescending = 21 + 1;

	public const int AvatarFileIdAscending = 23;
	public const int AvatarFileIdDescending = 23 + 1;

	public const int AvatarLinkAscending = 25;
	public const int AvatarLinkDescending = 25 + 1;

	public const int IndustryIdAscending = 27;
	public const int IndustryIdDescending = 27 + 1;

	public const int RegistrationNumberAscending = 29;
	public const int RegistrationNumberDescending = 29 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public string? UserName { get; set; }

	public byte[]? PasswordHash { get; set; }

	public byte[]? PasswordSalt { get; set; }

	public string? Name { get; set; }

	public string? FirebaseUid { get; set; }

	public string? Email { get; set; }

	public string? PhoneNumber { get; set; }

	public int? Status { get; set; }
	public int? MaxStatus { get; set; }
	public int? MinStatus { get; set; }
	public IEnumerable<int?>? ListStatus { get; set; }

	public string? Description { get; set; }

	public string? RejectMessage { get; set; }

	public string? AvatarFileId { get; set; }

	public string? AvatarLink { get; set; }

	public decimal? IndustryId { get; set; }
	public decimal? MaxIndustryId { get; set; }
	public decimal? MinIndustryId { get; set; }
	public IEnumerable<decimal?>? ListIndustryId { get; set; }

	public string? RegistrationNumber { get; set; }

}

