using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class ContractFilter : PaginationParams
{
    public IQueryable<Contract> ApplyFilter(IQueryable<Contract> query)
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

		if (OwnerId != null) {
			query = query.Where(e => e.OwnerId == OwnerId);
		}
		if (MaxOwnerId != null) {
			query = query.Where(e => e.OwnerId <= MaxOwnerId);
		}
		if (MinOwnerId != null) {
			query = query.Where(e => e.OwnerId >= MinOwnerId);
		}
		if (ListOwnerId != null) {
			query = query.Where(e => ListOwnerId.Contains(e.OwnerId));
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

		if (ReasonCancel != null) {
			query = query.Where(e => (e.ReasonCancel != null) && (e.ReasonCancel.Contains(ReasonCancel)));
		}

		if (CreateDate != null) {
			query = query.Where(e => e.CreateDate == CreateDate);
		}
		if (MaxCreateDate != null) {
			query = query.Where(e => e.CreateDate <= MaxCreateDate);
		}
		if (MinCreateDate != null) {
			query = query.Where(e => e.CreateDate >= MinCreateDate);
		}
		if (ListCreateDate != null) {
			query = query.Where(e => ListCreateDate.Contains(e.CreateDate));
		}

		if (PaymentTerm != null) {
			query = query.Where(e => (e.PaymentTerm != null) && (e.PaymentTerm.Contains(PaymentTerm)));
		}

		if (Price != null) {
			query = query.Where(e => e.Price == Price);
		}
		if (MaxPrice != null) {
			query = query.Where(e => e.Price <= MaxPrice);
		}
		if (MinPrice != null) {
			query = query.Where(e => e.Price >= MinPrice);
		}
		if (ListPrice != null) {
			query = query.Where(e => ListPrice.Contains(e.Price));
		}

		if (SignDate != null) {
			query = query.Where(e => e.SignDate == SignDate);
		}
		if (MaxSignDate != null) {
			query = query.Where(e => e.SignDate <= MaxSignDate);
		}
		if (MinSignDate != null) {
			query = query.Where(e => e.SignDate >= MinSignDate);
		}
		if (ListSignDate != null) {
			query = query.Where(e => ListSignDate.Contains(e.SignDate));
		}

		if (LessorBirthDate != null) {
			query = query.Where(e => e.LessorBirthDate == LessorBirthDate);
		}
		if (MaxLessorBirthDate != null) {
			query = query.Where(e => e.LessorBirthDate <= MaxLessorBirthDate);
		}
		if (MinLessorBirthDate != null) {
			query = query.Where(e => e.LessorBirthDate >= MinLessorBirthDate);
		}
		if (ListLessorBirthDate != null) {
			query = query.Where(e => ListLessorBirthDate.Contains(e.LessorBirthDate));
		}

		if (LessorCccd != null) {
			query = query.Where(e => (e.LessorCccd != null) && (e.LessorCccd.Contains(LessorCccd)));
		}

		if (LessorCccdGrantDate != null) {
			query = query.Where(e => e.LessorCccdGrantDate == LessorCccdGrantDate);
		}
		if (MaxLessorCccdGrantDate != null) {
			query = query.Where(e => e.LessorCccdGrantDate <= MaxLessorCccdGrantDate);
		}
		if (MinLessorCccdGrantDate != null) {
			query = query.Where(e => e.LessorCccdGrantDate >= MinLessorCccdGrantDate);
		}
		if (ListLessorCccdGrantDate != null) {
			query = query.Where(e => ListLessorCccdGrantDate.Contains(e.LessorCccdGrantDate));
		}

		if (LessorCccdGrantAddress != null) {
			query = query.Where(e => (e.LessorCccdGrantAddress != null) && (e.LessorCccdGrantAddress.Contains(LessorCccdGrantAddress)));
		}

		if (LessorAddress != null) {
			query = query.Where(e => (e.LessorAddress != null) && (e.LessorAddress.Contains(LessorAddress)));
		}

		if (LessorBankAccountNumber != null) {
			query = query.Where(e => (e.LessorBankAccountNumber != null) && (e.LessorBankAccountNumber.Contains(LessorBankAccountNumber)));
		}

		if (LessorBank != null) {
			query = query.Where(e => (e.LessorBank != null) && (e.LessorBank.Contains(LessorBank)));
		}

		if (RenterOfficeAddress != null) {
			query = query.Where(e => (e.RenterOfficeAddress != null) && (e.RenterOfficeAddress.Contains(RenterOfficeAddress)));
		}

		if (RegistrationNumberGrantDate != null) {
			query = query.Where(e => e.RegistrationNumberGrantDate == RegistrationNumberGrantDate);
		}
		if (MaxRegistrationNumberGrantDate != null) {
			query = query.Where(e => e.RegistrationNumberGrantDate <= MaxRegistrationNumberGrantDate);
		}
		if (MinRegistrationNumberGrantDate != null) {
			query = query.Where(e => e.RegistrationNumberGrantDate >= MinRegistrationNumberGrantDate);
		}
		if (ListRegistrationNumberGrantDate != null) {
			query = query.Where(e => ListRegistrationNumberGrantDate.Contains(e.RegistrationNumberGrantDate));
		}

		if (RegistrationNumberGrantAddress != null) {
			query = query.Where(e => (e.RegistrationNumberGrantAddress != null) && (e.RegistrationNumberGrantAddress.Contains(RegistrationNumberGrantAddress)));
		}

		if (BrandRepresentativeName != null) {
			query = query.Where(e => (e.BrandRepresentativeName != null) && (e.BrandRepresentativeName.Contains(BrandRepresentativeName)));
		}

		if (BrandRepresentativeBirthday != null) {
			query = query.Where(e => e.BrandRepresentativeBirthday == BrandRepresentativeBirthday);
		}
		if (MaxBrandRepresentativeBirthday != null) {
			query = query.Where(e => e.BrandRepresentativeBirthday <= MaxBrandRepresentativeBirthday);
		}
		if (MinBrandRepresentativeBirthday != null) {
			query = query.Where(e => e.BrandRepresentativeBirthday >= MinBrandRepresentativeBirthday);
		}
		if (ListBrandRepresentativeBirthday != null) {
			query = query.Where(e => ListBrandRepresentativeBirthday.Contains(e.BrandRepresentativeBirthday));
		}

		if (BrandRepresentativeAddress != null) {
			query = query.Where(e => (e.BrandRepresentativeAddress != null) && (e.BrandRepresentativeAddress.Contains(BrandRepresentativeAddress)));
		}

		if (BrandRepresentativePhoneNumber != null) {
			query = query.Where(e => (e.BrandRepresentativePhoneNumber != null) && (e.BrandRepresentativePhoneNumber.Contains(BrandRepresentativePhoneNumber)));
		}

		if (BrandRepresentativeCccd != null) {
			query = query.Where(e => (e.BrandRepresentativeCccd != null) && (e.BrandRepresentativeCccd.Contains(BrandRepresentativeCccd)));
		}

		if (BrandRepresentativeCccdGrantDate != null) {
			query = query.Where(e => e.BrandRepresentativeCccdGrantDate == BrandRepresentativeCccdGrantDate);
		}
		if (MaxBrandRepresentativeCccdGrantDate != null) {
			query = query.Where(e => e.BrandRepresentativeCccdGrantDate <= MaxBrandRepresentativeCccdGrantDate);
		}
		if (MinBrandRepresentativeCccdGrantDate != null) {
			query = query.Where(e => e.BrandRepresentativeCccdGrantDate >= MinBrandRepresentativeCccdGrantDate);
		}
		if (ListBrandRepresentativeCccdGrantDate != null) {
			query = query.Where(e => ListBrandRepresentativeCccdGrantDate.Contains(e.BrandRepresentativeCccdGrantDate));
		}

		if (BrandRepresentativeCccdGrantAddress != null) {
			query = query.Where(e => (e.BrandRepresentativeCccdGrantAddress != null) && (e.BrandRepresentativeCccdGrantAddress.Contains(BrandRepresentativeCccdGrantAddress)));
		}

		if (PayDay != null) {
			query = query.Where(e => e.PayDay == PayDay);
		}
		if (MaxPayDay != null) {
			query = query.Where(e => e.PayDay <= MaxPayDay);
		}
		if (MinPayDay != null) {
			query = query.Where(e => e.PayDay >= MinPayDay);
		}
		if (ListPayDay != null) {
			query = query.Where(e => ListPayDay.Contains(e.PayDay));
		}

		if (LeaseLength != null) {
			query = query.Where(e => e.LeaseLength == LeaseLength);
		}
		if (MaxLeaseLength != null) {
			query = query.Where(e => e.LeaseLength <= MaxLeaseLength);
		}
		if (MinLeaseLength != null) {
			query = query.Where(e => e.LeaseLength >= MinLeaseLength);
		}
		if (ListLeaseLength != null) {
			query = query.Where(e => ListLeaseLength.Contains(e.LeaseLength));
		}

		if (HandoverDate != null) {
			query = query.Where(e => e.HandoverDate == HandoverDate);
		}
		if (MaxHandoverDate != null) {
			query = query.Where(e => e.HandoverDate <= MaxHandoverDate);
		}
		if (MinHandoverDate != null) {
			query = query.Where(e => e.HandoverDate >= MinHandoverDate);
		}
		if (ListHandoverDate != null) {
			query = query.Where(e => ListHandoverDate.Contains(e.HandoverDate));
		}

		
		switch (OrderBy)
        {
            
			case IdAscending:
                query = query.OrderBy(c => c.Id);
                break;
            case IdDescending:
                query = query.OrderByDescending(c => c.Id);
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

			case OwnerIdAscending:
                query = query.OrderBy(c => c.OwnerId);
                break;
            case OwnerIdDescending:
                query = query.OrderByDescending(c => c.OwnerId);
                break;

			case PropertyIdAscending:
                query = query.OrderBy(c => c.PropertyId);
                break;
            case PropertyIdDescending:
                query = query.OrderByDescending(c => c.PropertyId);
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

			case StatusAscending:
                query = query.OrderBy(c => c.Status);
                break;
            case StatusDescending:
                query = query.OrderByDescending(c => c.Status);
                break;

			case ReasonCancelAscending:
                query = query.OrderBy(c => c.ReasonCancel);
                break;
            case ReasonCancelDescending:
                query = query.OrderByDescending(c => c.ReasonCancel);
                break;

			case CreateDateAscending:
                query = query.OrderBy(c => c.CreateDate);
                break;
            case CreateDateDescending:
                query = query.OrderByDescending(c => c.CreateDate);
                break;

			case PaymentTermAscending:
                query = query.OrderBy(c => c.PaymentTerm);
                break;
            case PaymentTermDescending:
                query = query.OrderByDescending(c => c.PaymentTerm);
                break;

			case PriceAscending:
                query = query.OrderBy(c => c.Price);
                break;
            case PriceDescending:
                query = query.OrderByDescending(c => c.Price);
                break;

			case SignDateAscending:
                query = query.OrderBy(c => c.SignDate);
                break;
            case SignDateDescending:
                query = query.OrderByDescending(c => c.SignDate);
                break;

			case LessorBirthDateAscending:
                query = query.OrderBy(c => c.LessorBirthDate);
                break;
            case LessorBirthDateDescending:
                query = query.OrderByDescending(c => c.LessorBirthDate);
                break;

			case LessorCccdAscending:
                query = query.OrderBy(c => c.LessorCccd);
                break;
            case LessorCccdDescending:
                query = query.OrderByDescending(c => c.LessorCccd);
                break;

			case LessorCccdGrantDateAscending:
                query = query.OrderBy(c => c.LessorCccdGrantDate);
                break;
            case LessorCccdGrantDateDescending:
                query = query.OrderByDescending(c => c.LessorCccdGrantDate);
                break;

			case LessorCccdGrantAddressAscending:
                query = query.OrderBy(c => c.LessorCccdGrantAddress);
                break;
            case LessorCccdGrantAddressDescending:
                query = query.OrderByDescending(c => c.LessorCccdGrantAddress);
                break;

			case LessorAddressAscending:
                query = query.OrderBy(c => c.LessorAddress);
                break;
            case LessorAddressDescending:
                query = query.OrderByDescending(c => c.LessorAddress);
                break;

			case LessorBankAccountNumberAscending:
                query = query.OrderBy(c => c.LessorBankAccountNumber);
                break;
            case LessorBankAccountNumberDescending:
                query = query.OrderByDescending(c => c.LessorBankAccountNumber);
                break;

			case LessorBankAscending:
                query = query.OrderBy(c => c.LessorBank);
                break;
            case LessorBankDescending:
                query = query.OrderByDescending(c => c.LessorBank);
                break;

			case RenterOfficeAddressAscending:
                query = query.OrderBy(c => c.RenterOfficeAddress);
                break;
            case RenterOfficeAddressDescending:
                query = query.OrderByDescending(c => c.RenterOfficeAddress);
                break;

			case RegistrationNumberGrantDateAscending:
                query = query.OrderBy(c => c.RegistrationNumberGrantDate);
                break;
            case RegistrationNumberGrantDateDescending:
                query = query.OrderByDescending(c => c.RegistrationNumberGrantDate);
                break;

			case RegistrationNumberGrantAddressAscending:
                query = query.OrderBy(c => c.RegistrationNumberGrantAddress);
                break;
            case RegistrationNumberGrantAddressDescending:
                query = query.OrderByDescending(c => c.RegistrationNumberGrantAddress);
                break;

			case BrandRepresentativeNameAscending:
                query = query.OrderBy(c => c.BrandRepresentativeName);
                break;
            case BrandRepresentativeNameDescending:
                query = query.OrderByDescending(c => c.BrandRepresentativeName);
                break;

			case BrandRepresentativeBirthdayAscending:
                query = query.OrderBy(c => c.BrandRepresentativeBirthday);
                break;
            case BrandRepresentativeBirthdayDescending:
                query = query.OrderByDescending(c => c.BrandRepresentativeBirthday);
                break;

			case BrandRepresentativeAddressAscending:
                query = query.OrderBy(c => c.BrandRepresentativeAddress);
                break;
            case BrandRepresentativeAddressDescending:
                query = query.OrderByDescending(c => c.BrandRepresentativeAddress);
                break;

			case BrandRepresentativePhoneNumberAscending:
                query = query.OrderBy(c => c.BrandRepresentativePhoneNumber);
                break;
            case BrandRepresentativePhoneNumberDescending:
                query = query.OrderByDescending(c => c.BrandRepresentativePhoneNumber);
                break;

			case BrandRepresentativeCccdAscending:
                query = query.OrderBy(c => c.BrandRepresentativeCccd);
                break;
            case BrandRepresentativeCccdDescending:
                query = query.OrderByDescending(c => c.BrandRepresentativeCccd);
                break;

			case BrandRepresentativeCccdGrantDateAscending:
                query = query.OrderBy(c => c.BrandRepresentativeCccdGrantDate);
                break;
            case BrandRepresentativeCccdGrantDateDescending:
                query = query.OrderByDescending(c => c.BrandRepresentativeCccdGrantDate);
                break;

			case BrandRepresentativeCccdGrantAddressAscending:
                query = query.OrderBy(c => c.BrandRepresentativeCccdGrantAddress);
                break;
            case BrandRepresentativeCccdGrantAddressDescending:
                query = query.OrderByDescending(c => c.BrandRepresentativeCccdGrantAddress);
                break;

			case PayDayAscending:
                query = query.OrderBy(c => c.PayDay);
                break;
            case PayDayDescending:
                query = query.OrderByDescending(c => c.PayDay);
                break;

			case LeaseLengthAscending:
                query = query.OrderBy(c => c.LeaseLength);
                break;
            case LeaseLengthDescending:
                query = query.OrderByDescending(c => c.LeaseLength);
                break;

			case HandoverDateAscending:
                query = query.OrderBy(c => c.HandoverDate);
                break;
            case HandoverDateDescending:
                query = query.OrderByDescending(c => c.HandoverDate);
                break;

        }
		return query;
    }
	
	public int? OrderBy { get; set; }
	
	public const int IdAscending = 1;
	public const int IdDescending = 1 + 1;

	public const int StartDateAscending = 3;
	public const int StartDateDescending = 3 + 1;

	public const int EndDateAscending = 5;
	public const int EndDateDescending = 5 + 1;

	public const int OwnerIdAscending = 7;
	public const int OwnerIdDescending = 7 + 1;

	public const int PropertyIdAscending = 9;
	public const int PropertyIdDescending = 9 + 1;

	public const int BrandIdAscending = 11;
	public const int BrandIdDescending = 11 + 1;

	public const int BrokerIdAscending = 13;
	public const int BrokerIdDescending = 13 + 1;

	public const int StatusAscending = 15;
	public const int StatusDescending = 15 + 1;

	public const int ReasonCancelAscending = 17;
	public const int ReasonCancelDescending = 17 + 1;

	public const int CreateDateAscending = 19;
	public const int CreateDateDescending = 19 + 1;

	public const int PaymentTermAscending = 21;
	public const int PaymentTermDescending = 21 + 1;

	public const int PriceAscending = 23;
	public const int PriceDescending = 23 + 1;

	public const int SignDateAscending = 25;
	public const int SignDateDescending = 25 + 1;

	public const int LessorBirthDateAscending = 27;
	public const int LessorBirthDateDescending = 27 + 1;

	public const int LessorCccdAscending = 29;
	public const int LessorCccdDescending = 29 + 1;

	public const int LessorCccdGrantDateAscending = 31;
	public const int LessorCccdGrantDateDescending = 31 + 1;

	public const int LessorCccdGrantAddressAscending = 33;
	public const int LessorCccdGrantAddressDescending = 33 + 1;

	public const int LessorAddressAscending = 35;
	public const int LessorAddressDescending = 35 + 1;

	public const int LessorBankAccountNumberAscending = 37;
	public const int LessorBankAccountNumberDescending = 37 + 1;

	public const int LessorBankAscending = 39;
	public const int LessorBankDescending = 39 + 1;

	public const int RenterOfficeAddressAscending = 41;
	public const int RenterOfficeAddressDescending = 41 + 1;

	public const int RegistrationNumberGrantDateAscending = 43;
	public const int RegistrationNumberGrantDateDescending = 43 + 1;

	public const int RegistrationNumberGrantAddressAscending = 45;
	public const int RegistrationNumberGrantAddressDescending = 45 + 1;

	public const int BrandRepresentativeNameAscending = 47;
	public const int BrandRepresentativeNameDescending = 47 + 1;

	public const int BrandRepresentativeBirthdayAscending = 49;
	public const int BrandRepresentativeBirthdayDescending = 49 + 1;

	public const int BrandRepresentativeAddressAscending = 51;
	public const int BrandRepresentativeAddressDescending = 51 + 1;

	public const int BrandRepresentativePhoneNumberAscending = 53;
	public const int BrandRepresentativePhoneNumberDescending = 53 + 1;

	public const int BrandRepresentativeCccdAscending = 55;
	public const int BrandRepresentativeCccdDescending = 55 + 1;

	public const int BrandRepresentativeCccdGrantDateAscending = 57;
	public const int BrandRepresentativeCccdGrantDateDescending = 57 + 1;

	public const int BrandRepresentativeCccdGrantAddressAscending = 59;
	public const int BrandRepresentativeCccdGrantAddressDescending = 59 + 1;

	public const int PayDayAscending = 61;
	public const int PayDayDescending = 61 + 1;

	public const int LeaseLengthAscending = 63;
	public const int LeaseLengthDescending = 63 + 1;

	public const int HandoverDateAscending = 65;
	public const int HandoverDateDescending = 65 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public DateTime? StartDate { get; set; }
	public DateTime? MaxStartDate { get; set; }
	public DateTime? MinStartDate { get; set; }
	public IEnumerable<DateTime?>? ListStartDate { get; set; }

	public DateTime? EndDate { get; set; }
	public DateTime? MaxEndDate { get; set; }
	public DateTime? MinEndDate { get; set; }
	public IEnumerable<DateTime?>? ListEndDate { get; set; }

	public decimal? OwnerId { get; set; }
	public decimal? MaxOwnerId { get; set; }
	public decimal? MinOwnerId { get; set; }
	public IEnumerable<decimal?>? ListOwnerId { get; set; }

	public decimal? PropertyId { get; set; }
	public decimal? MaxPropertyId { get; set; }
	public decimal? MinPropertyId { get; set; }
	public IEnumerable<decimal?>? ListPropertyId { get; set; }

	public decimal? BrandId { get; set; }
	public decimal? MaxBrandId { get; set; }
	public decimal? MinBrandId { get; set; }
	public IEnumerable<decimal?>? ListBrandId { get; set; }

	public decimal? BrokerId { get; set; }
	public decimal? MaxBrokerId { get; set; }
	public decimal? MinBrokerId { get; set; }
	public IEnumerable<decimal?>? ListBrokerId { get; set; }

	public int? Status { get; set; }
	public int? MaxStatus { get; set; }
	public int? MinStatus { get; set; }
	public IEnumerable<int?>? ListStatus { get; set; }

	public string? ReasonCancel { get; set; }

	public DateTime? CreateDate { get; set; }
	public DateTime? MaxCreateDate { get; set; }
	public DateTime? MinCreateDate { get; set; }
	public IEnumerable<DateTime?>? ListCreateDate { get; set; }

	public string? PaymentTerm { get; set; }

	public decimal? Price { get; set; }
	public decimal? MaxPrice { get; set; }
	public decimal? MinPrice { get; set; }
	public IEnumerable<decimal?>? ListPrice { get; set; }

	public DateTime? SignDate { get; set; }
	public DateTime? MaxSignDate { get; set; }
	public DateTime? MinSignDate { get; set; }
	public IEnumerable<DateTime?>? ListSignDate { get; set; }

	public DateTime? LessorBirthDate { get; set; }
	public DateTime? MaxLessorBirthDate { get; set; }
	public DateTime? MinLessorBirthDate { get; set; }
	public IEnumerable<DateTime?>? ListLessorBirthDate { get; set; }

	public string? LessorCccd { get; set; }

	public DateTime? LessorCccdGrantDate { get; set; }
	public DateTime? MaxLessorCccdGrantDate { get; set; }
	public DateTime? MinLessorCccdGrantDate { get; set; }
	public IEnumerable<DateTime?>? ListLessorCccdGrantDate { get; set; }

	public string? LessorCccdGrantAddress { get; set; }

	public string? LessorAddress { get; set; }

	public string? LessorBankAccountNumber { get; set; }

	public string? LessorBank { get; set; }

	public string? RenterOfficeAddress { get; set; }

	public DateTime? RegistrationNumberGrantDate { get; set; }
	public DateTime? MaxRegistrationNumberGrantDate { get; set; }
	public DateTime? MinRegistrationNumberGrantDate { get; set; }
	public IEnumerable<DateTime?>? ListRegistrationNumberGrantDate { get; set; }

	public string? RegistrationNumberGrantAddress { get; set; }

	public string? BrandRepresentativeName { get; set; }

	public DateTime? BrandRepresentativeBirthday { get; set; }
	public DateTime? MaxBrandRepresentativeBirthday { get; set; }
	public DateTime? MinBrandRepresentativeBirthday { get; set; }
	public IEnumerable<DateTime?>? ListBrandRepresentativeBirthday { get; set; }

	public string? BrandRepresentativeAddress { get; set; }

	public string? BrandRepresentativePhoneNumber { get; set; }

	public string? BrandRepresentativeCccd { get; set; }

	public DateTime? BrandRepresentativeCccdGrantDate { get; set; }
	public DateTime? MaxBrandRepresentativeCccdGrantDate { get; set; }
	public DateTime? MinBrandRepresentativeCccdGrantDate { get; set; }
	public IEnumerable<DateTime?>? ListBrandRepresentativeCccdGrantDate { get; set; }

	public string? BrandRepresentativeCccdGrantAddress { get; set; }

	public int? PayDay { get; set; }
	public int? MaxPayDay { get; set; }
	public int? MinPayDay { get; set; }
	public IEnumerable<int?>? ListPayDay { get; set; }

	public double? LeaseLength { get; set; }
	public double? MaxLeaseLength { get; set; }
	public double? MinLeaseLength { get; set; }
	public IEnumerable<double?>? ListLeaseLength { get; set; }

	public DateTime? HandoverDate { get; set; }
	public DateTime? MaxHandoverDate { get; set; }
	public DateTime? MinHandoverDate { get; set; }
	public IEnumerable<DateTime?>? ListHandoverDate { get; set; }

}

