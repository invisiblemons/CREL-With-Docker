using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class PropertyFilter : PaginationParams
{
    public IQueryable<Property> ApplyFilter(IQueryable<Property> query)
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

		if (LocationId != null) {
			query = query.Where(e => e.LocationId == LocationId);
		}
		if (MaxLocationId != null) {
			query = query.Where(e => e.LocationId <= MaxLocationId);
		}
		if (MinLocationId != null) {
			query = query.Where(e => e.LocationId >= MinLocationId);
		}
		if (ListLocationId != null) {
			query = query.Where(e => ListLocationId.Contains(e.LocationId));
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

		if (LastUpdateDate != null) {
			query = query.Where(e => e.LastUpdateDate == LastUpdateDate);
		}
		if (MaxLastUpdateDate != null) {
			query = query.Where(e => e.LastUpdateDate <= MaxLastUpdateDate);
		}
		if (MinLastUpdateDate != null) {
			query = query.Where(e => e.LastUpdateDate >= MinLastUpdateDate);
		}
		if (ListLastUpdateDate != null) {
			query = query.Where(e => ListLastUpdateDate.Contains(e.LastUpdateDate));
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

		if (RejectReason != null) {
			query = query.Where(e => (e.RejectReason != null) && (e.RejectReason.Contains(RejectReason)));
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

		if (Name != null) {
			query = query.Where(e => (e.Name != null) && (e.Name.Contains(Name)));
		}

		if (ProjectId != null) {
			query = query.Where(e => e.ProjectId == ProjectId);
		}
		if (MaxProjectId != null) {
			query = query.Where(e => e.ProjectId <= MaxProjectId);
		}
		if (MinProjectId != null) {
			query = query.Where(e => e.ProjectId >= MinProjectId);
		}
		if (ListProjectId != null) {
			query = query.Where(e => ListProjectId.Contains(e.ProjectId));
		}

		if (Direction != null) {
			query = query.Where(e => e.Direction == Direction);
		}
		if (MaxDirection != null) {
			query = query.Where(e => e.Direction <= MaxDirection);
		}
		if (MinDirection != null) {
			query = query.Where(e => e.Direction >= MinDirection);
		}
		if (ListDirection != null) {
			query = query.Where(e => ListDirection.Contains(e.Direction));
		}

		if (Floor != null) {
			query = query.Where(e => (e.Floor != null) && (e.Floor.Contains(Floor)));
		}

		if (FloorArea != null) {
			query = query.Where(e => e.FloorArea == FloorArea);
		}
		if (MaxFloorArea != null) {
			query = query.Where(e => e.FloorArea <= MaxFloorArea);
		}
		if (MinFloorArea != null) {
			query = query.Where(e => e.FloorArea >= MinFloorArea);
		}
		if (ListFloorArea != null) {
			query = query.Where(e => ListFloorArea.Contains(e.FloorArea));
		}

		if (Area != null) {
			query = query.Where(e => e.Area == Area);
		}
		if (MaxArea != null) {
			query = query.Where(e => e.Area <= MaxArea);
		}
		if (MinArea != null) {
			query = query.Where(e => e.Area >= MinArea);
		}
		if (ListArea != null) {
			query = query.Where(e => ListArea.Contains(e.Area));
		}

		if (Frontage != null) {
			query = query.Where(e => e.Frontage == Frontage);
		}
		if (MaxFrontage != null) {
			query = query.Where(e => e.Frontage <= MaxFrontage);
		}
		if (MinFrontage != null) {
			query = query.Where(e => e.Frontage >= MinFrontage);
		}
		if (ListFrontage != null) {
			query = query.Where(e => ListFrontage.Contains(e.Frontage));
		}

		if (Certificates != null) {
			query = query.Where(e => e.Certificates == Certificates);
		}
		if (MaxCertificates != null) {
			query = query.Where(e => e.Certificates <= MaxCertificates);
		}
		if (MinCertificates != null) {
			query = query.Where(e => e.Certificates >= MinCertificates);
		}
		if (ListCertificates != null) {
			query = query.Where(e => ListCertificates.Contains(e.Certificates));
		}

		if (Vertical != null) {
			query = query.Where(e => e.Vertical == Vertical);
		}
		if (MaxVertical != null) {
			query = query.Where(e => e.Vertical <= MaxVertical);
		}
		if (MinVertical != null) {
			query = query.Where(e => e.Vertical >= MinVertical);
		}
		if (ListVertical != null) {
			query = query.Where(e => ListVertical.Contains(e.Vertical));
		}

		if (Horizontal != null) {
			query = query.Where(e => e.Horizontal == Horizontal);
		}
		if (MaxHorizontal != null) {
			query = query.Where(e => e.Horizontal <= MaxHorizontal);
		}
		if (MinHorizontal != null) {
			query = query.Where(e => e.Horizontal >= MinHorizontal);
		}
		if (ListHorizontal != null) {
			query = query.Where(e => ListHorizontal.Contains(e.Horizontal));
		}

		if (RoadWidth != null) {
			query = query.Where(e => e.RoadWidth == RoadWidth);
		}
		if (MaxRoadWidth != null) {
			query = query.Where(e => e.RoadWidth <= MaxRoadWidth);
		}
		if (MinRoadWidth != null) {
			query = query.Where(e => e.RoadWidth >= MinRoadWidth);
		}
		if (ListRoadWidth != null) {
			query = query.Where(e => ListRoadWidth.Contains(e.RoadWidth));
		}

		if (RentalCondition != null) {
			query = query.Where(e => (e.RentalCondition != null) && (e.RentalCondition.Contains(RentalCondition)));
		}

		if (RentalTerm != null) {
			query = query.Where(e => (e.RentalTerm != null) && (e.RentalTerm.Contains(RentalTerm)));
		}

		if (DepositTerm != null) {
			query = query.Where(e => (e.DepositTerm != null) && (e.DepositTerm.Contains(DepositTerm)));
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

		if (NumberOfFrontage != null) {
			query = query.Where(e => e.NumberOfFrontage == NumberOfFrontage);
		}
		if (MaxNumberOfFrontage != null) {
			query = query.Where(e => e.NumberOfFrontage <= MaxNumberOfFrontage);
		}
		if (MinNumberOfFrontage != null) {
			query = query.Where(e => e.NumberOfFrontage >= MinNumberOfFrontage);
		}
		if (ListNumberOfFrontage != null) {
			query = query.Where(e => ListNumberOfFrontage.Contains(e.NumberOfFrontage));
		}

		
		switch (OrderBy)
        {
            
			case IdAscending:
                query = query.OrderBy(c => c.Id);
                break;
            case IdDescending:
                query = query.OrderByDescending(c => c.Id);
                break;

			case LocationIdAscending:
                query = query.OrderBy(c => c.LocationId);
                break;
            case LocationIdDescending:
                query = query.OrderByDescending(c => c.LocationId);
                break;

			case BrokerIdAscending:
                query = query.OrderBy(c => c.BrokerId);
                break;
            case BrokerIdDescending:
                query = query.OrderByDescending(c => c.BrokerId);
                break;

			case CreateDateAscending:
                query = query.OrderBy(c => c.CreateDate);
                break;
            case CreateDateDescending:
                query = query.OrderByDescending(c => c.CreateDate);
                break;

			case LastUpdateDateAscending:
                query = query.OrderBy(c => c.LastUpdateDate);
                break;
            case LastUpdateDateDescending:
                query = query.OrderByDescending(c => c.LastUpdateDate);
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

			case RejectReasonAscending:
                query = query.OrderBy(c => c.RejectReason);
                break;
            case RejectReasonDescending:
                query = query.OrderByDescending(c => c.RejectReason);
                break;

			case TypeAscending:
                query = query.OrderBy(c => c.Type);
                break;
            case TypeDescending:
                query = query.OrderByDescending(c => c.Type);
                break;

			case NameAscending:
                query = query.OrderBy(c => c.Name);
                break;
            case NameDescending:
                query = query.OrderByDescending(c => c.Name);
                break;

			case ProjectIdAscending:
                query = query.OrderBy(c => c.ProjectId);
                break;
            case ProjectIdDescending:
                query = query.OrderByDescending(c => c.ProjectId);
                break;

			case DirectionAscending:
                query = query.OrderBy(c => c.Direction);
                break;
            case DirectionDescending:
                query = query.OrderByDescending(c => c.Direction);
                break;

			case FloorAscending:
                query = query.OrderBy(c => c.Floor);
                break;
            case FloorDescending:
                query = query.OrderByDescending(c => c.Floor);
                break;

			case FloorAreaAscending:
                query = query.OrderBy(c => c.FloorArea);
                break;
            case FloorAreaDescending:
                query = query.OrderByDescending(c => c.FloorArea);
                break;

			case AreaAscending:
                query = query.OrderBy(c => c.Area);
                break;
            case AreaDescending:
                query = query.OrderByDescending(c => c.Area);
                break;

			case FrontageAscending:
                query = query.OrderBy(c => c.Frontage);
                break;
            case FrontageDescending:
                query = query.OrderByDescending(c => c.Frontage);
                break;

			case CertificatesAscending:
                query = query.OrderBy(c => c.Certificates);
                break;
            case CertificatesDescending:
                query = query.OrderByDescending(c => c.Certificates);
                break;

			case VerticalAscending:
                query = query.OrderBy(c => c.Vertical);
                break;
            case VerticalDescending:
                query = query.OrderByDescending(c => c.Vertical);
                break;

			case HorizontalAscending:
                query = query.OrderBy(c => c.Horizontal);
                break;
            case HorizontalDescending:
                query = query.OrderByDescending(c => c.Horizontal);
                break;

			case RoadWidthAscending:
                query = query.OrderBy(c => c.RoadWidth);
                break;
            case RoadWidthDescending:
                query = query.OrderByDescending(c => c.RoadWidth);
                break;

			case RentalConditionAscending:
                query = query.OrderBy(c => c.RentalCondition);
                break;
            case RentalConditionDescending:
                query = query.OrderByDescending(c => c.RentalCondition);
                break;

			case RentalTermAscending:
                query = query.OrderBy(c => c.RentalTerm);
                break;
            case RentalTermDescending:
                query = query.OrderByDescending(c => c.RentalTerm);
                break;

			case DepositTermAscending:
                query = query.OrderBy(c => c.DepositTerm);
                break;
            case DepositTermDescending:
                query = query.OrderByDescending(c => c.DepositTerm);
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

			case NumberOfFrontageAscending:
                query = query.OrderBy(c => c.NumberOfFrontage);
                break;
            case NumberOfFrontageDescending:
                query = query.OrderByDescending(c => c.NumberOfFrontage);
                break;

        }
		return query;
    }
	
	public int? OrderBy { get; set; }
	
	public const int IdAscending = 1;
	public const int IdDescending = 1 + 1;

	public const int LocationIdAscending = 3;
	public const int LocationIdDescending = 3 + 1;

	public const int BrokerIdAscending = 5;
	public const int BrokerIdDescending = 5 + 1;

	public const int CreateDateAscending = 7;
	public const int CreateDateDescending = 7 + 1;

	public const int LastUpdateDateAscending = 9;
	public const int LastUpdateDateDescending = 9 + 1;

	public const int StatusAscending = 11;
	public const int StatusDescending = 11 + 1;

	public const int DescriptionAscending = 13;
	public const int DescriptionDescending = 13 + 1;

	public const int RejectReasonAscending = 15;
	public const int RejectReasonDescending = 15 + 1;

	public const int TypeAscending = 17;
	public const int TypeDescending = 17 + 1;

	public const int NameAscending = 19;
	public const int NameDescending = 19 + 1;

	public const int ProjectIdAscending = 21;
	public const int ProjectIdDescending = 21 + 1;

	public const int DirectionAscending = 23;
	public const int DirectionDescending = 23 + 1;

	public const int FloorAscending = 25;
	public const int FloorDescending = 25 + 1;

	public const int FloorAreaAscending = 27;
	public const int FloorAreaDescending = 27 + 1;

	public const int AreaAscending = 29;
	public const int AreaDescending = 29 + 1;

	public const int FrontageAscending = 31;
	public const int FrontageDescending = 31 + 1;

	public const int CertificatesAscending = 33;
	public const int CertificatesDescending = 33 + 1;

	public const int VerticalAscending = 35;
	public const int VerticalDescending = 35 + 1;

	public const int HorizontalAscending = 37;
	public const int HorizontalDescending = 37 + 1;

	public const int RoadWidthAscending = 39;
	public const int RoadWidthDescending = 39 + 1;

	public const int RentalConditionAscending = 41;
	public const int RentalConditionDescending = 41 + 1;

	public const int RentalTermAscending = 43;
	public const int RentalTermDescending = 43 + 1;

	public const int DepositTermAscending = 45;
	public const int DepositTermDescending = 45 + 1;

	public const int PaymentTermAscending = 47;
	public const int PaymentTermDescending = 47 + 1;

	public const int PriceAscending = 49;
	public const int PriceDescending = 49 + 1;

	public const int NumberOfFrontageAscending = 51;
	public const int NumberOfFrontageDescending = 51 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public decimal? LocationId { get; set; }
	public decimal? MaxLocationId { get; set; }
	public decimal? MinLocationId { get; set; }
	public IEnumerable<decimal?>? ListLocationId { get; set; }

	public decimal? BrokerId { get; set; }
	public decimal? MaxBrokerId { get; set; }
	public decimal? MinBrokerId { get; set; }
	public IEnumerable<decimal?>? ListBrokerId { get; set; }

	public DateTime? CreateDate { get; set; }
	public DateTime? MaxCreateDate { get; set; }
	public DateTime? MinCreateDate { get; set; }
	public IEnumerable<DateTime?>? ListCreateDate { get; set; }

	public DateTime? LastUpdateDate { get; set; }
	public DateTime? MaxLastUpdateDate { get; set; }
	public DateTime? MinLastUpdateDate { get; set; }
	public IEnumerable<DateTime?>? ListLastUpdateDate { get; set; }

	public int? Status { get; set; }
	public int? MaxStatus { get; set; }
	public int? MinStatus { get; set; }
	public IEnumerable<int?>? ListStatus { get; set; }

	public string? Description { get; set; }

	public string? RejectReason { get; set; }

	public int? Type { get; set; }
	public int? MaxType { get; set; }
	public int? MinType { get; set; }
	public IEnumerable<int?>? ListType { get; set; }

	public string? Name { get; set; }

	public decimal? ProjectId { get; set; }
	public decimal? MaxProjectId { get; set; }
	public decimal? MinProjectId { get; set; }
	public IEnumerable<decimal?>? ListProjectId { get; set; }

	public int? Direction { get; set; }
	public int? MaxDirection { get; set; }
	public int? MinDirection { get; set; }
	public IEnumerable<int?>? ListDirection { get; set; }

	public string? Floor { get; set; }

	public double? FloorArea { get; set; }
	public double? MaxFloorArea { get; set; }
	public double? MinFloorArea { get; set; }
	public IEnumerable<double?>? ListFloorArea { get; set; }

	public double? Area { get; set; }
	public double? MaxArea { get; set; }
	public double? MinArea { get; set; }
	public IEnumerable<double?>? ListArea { get; set; }

	public double? Frontage { get; set; }
	public double? MaxFrontage { get; set; }
	public double? MinFrontage { get; set; }
	public IEnumerable<double?>? ListFrontage { get; set; }

	public int? Certificates { get; set; }
	public int? MaxCertificates { get; set; }
	public int? MinCertificates { get; set; }
	public IEnumerable<int?>? ListCertificates { get; set; }

	public double? Vertical { get; set; }
	public double? MaxVertical { get; set; }
	public double? MinVertical { get; set; }
	public IEnumerable<double?>? ListVertical { get; set; }

	public double? Horizontal { get; set; }
	public double? MaxHorizontal { get; set; }
	public double? MinHorizontal { get; set; }
	public IEnumerable<double?>? ListHorizontal { get; set; }

	public double? RoadWidth { get; set; }
	public double? MaxRoadWidth { get; set; }
	public double? MinRoadWidth { get; set; }
	public IEnumerable<double?>? ListRoadWidth { get; set; }

	public string? RentalCondition { get; set; }

	public string? RentalTerm { get; set; }

	public string? DepositTerm { get; set; }

	public string? PaymentTerm { get; set; }

	public decimal? Price { get; set; }
	public decimal? MaxPrice { get; set; }
	public decimal? MinPrice { get; set; }
	public IEnumerable<decimal?>? ListPrice { get; set; }

	public decimal? NumberOfFrontage { get; set; }
	public decimal? MaxNumberOfFrontage { get; set; }
	public decimal? MinNumberOfFrontage { get; set; }
	public IEnumerable<decimal?>? ListNumberOfFrontage { get; set; }
}

