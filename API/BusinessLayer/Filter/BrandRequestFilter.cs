using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class BrandRequestFilter : PaginationParams
{
    public IQueryable<BrandRequest> ApplyFilter(IQueryable<BrandRequest> query)
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

		if (Area != null) {
			query = query.Where(e => (e.Area != null) && (e.Area.Contains(Area)));
		}

		if (Amount != null) {
			query = query.Where(e => e.Amount == Amount);
		}
		if (MaxAmount != null) {
			query = query.Where(e => e.Amount <= MaxAmount);
		}
		if (MinAmount != null) {
			query = query.Where(e => e.Amount >= MinAmount);
		}
		if (ListAmount != null) {
			query = query.Where(e => ListAmount.Contains(e.Amount));
		}

		if (AmountFrontage != null) {
			query = query.Where(e => e.AmountFrontage == AmountFrontage);
		}
		if (MaxAmountFrontage != null) {
			query = query.Where(e => e.AmountFrontage <= MaxAmountFrontage);
		}
		if (MinAmountFrontage != null) {
			query = query.Where(e => e.AmountFrontage >= MinAmountFrontage);
		}
		if (ListAmountFrontage != null) {
			query = query.Where(e => ListAmountFrontage.Contains(e.AmountFrontage));
		}

		if (MinPrice != null) {
			query = query.Where(e => e.MinPrice == MinPrice);
		}
		if (MaxMinPrice != null) {
			query = query.Where(e => e.MinPrice <= MaxMinPrice);
		}
		if (MinMinPrice != null) {
			query = query.Where(e => e.MinPrice >= MinMinPrice);
		}
		if (ListMinPrice != null) {
			query = query.Where(e => ListMinPrice.Contains(e.MinPrice));
		}

		if (MaxPrice != null) {
			query = query.Where(e => e.MaxPrice == MaxPrice);
		}
		if (MaxMaxPrice != null) {
			query = query.Where(e => e.MaxPrice <= MaxMaxPrice);
		}
		if (MinMaxPrice != null) {
			query = query.Where(e => e.MaxPrice >= MinMaxPrice);
		}
		if (ListMaxPrice != null) {
			query = query.Where(e => ListMaxPrice.Contains(e.MaxPrice));
		}

		if (MinRentalTime != null) {
			query = query.Where(e => e.MinRentalTime == MinRentalTime);
		}
		if (MaxMinRentalTime != null) {
			query = query.Where(e => e.MinRentalTime <= MaxMinRentalTime);
		}
		if (MinMinRentalTime != null) {
			query = query.Where(e => e.MinRentalTime >= MinMinRentalTime);
		}
		if (ListMinRentalTime != null) {
			query = query.Where(e => ListMinRentalTime.Contains(e.MinRentalTime));
		}

		if (MaxRentalTime != null) {
			query = query.Where(e => e.MaxRentalTime == MaxRentalTime);
		}
		if (MaxMaxRentalTime != null) {
			query = query.Where(e => e.MaxRentalTime <= MaxMaxRentalTime);
		}
		if (MinMaxRentalTime != null) {
			query = query.Where(e => e.MaxRentalTime >= MinMaxRentalTime);
		}
		if (ListMaxRentalTime != null) {
			query = query.Where(e => ListMaxRentalTime.Contains(e.MaxRentalTime));
		}

		if (MinFloorArea != null) {
			query = query.Where(e => e.MinFloorArea == MinFloorArea);
		}
		if (MaxMinFloorArea != null) {
			query = query.Where(e => e.MinFloorArea <= MaxMinFloorArea);
		}
		if (MinMinFloorArea != null) {
			query = query.Where(e => e.MinFloorArea >= MinMinFloorArea);
		}
		if (ListMinFloorArea != null) {
			query = query.Where(e => ListMinFloorArea.Contains(e.MinFloorArea));
		}

		if (MaxFloorArea != null) {
			query = query.Where(e => e.MaxFloorArea == MaxFloorArea);
		}
		if (MaxMaxFloorArea != null) {
			query = query.Where(e => e.MaxFloorArea <= MaxMaxFloorArea);
		}
		if (MinMaxFloorArea != null) {
			query = query.Where(e => e.MaxFloorArea >= MinMaxFloorArea);
		}
		if (ListMaxFloorArea != null) {
			query = query.Where(e => ListMaxFloorArea.Contains(e.MaxFloorArea));
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

			case AreaAscending:
                query = query.OrderBy(c => c.Area);
                break;
            case AreaDescending:
                query = query.OrderByDescending(c => c.Area);
                break;

			case AmountAscending:
                query = query.OrderBy(c => c.Amount);
                break;
            case AmountDescending:
                query = query.OrderByDescending(c => c.Amount);
                break;

			case AmountFrontageAscending:
                query = query.OrderBy(c => c.AmountFrontage);
                break;
            case AmountFrontageDescending:
                query = query.OrderByDescending(c => c.AmountFrontage);
                break;

			case MinPriceAscending:
                query = query.OrderBy(c => c.MinPrice);
                break;
            case MinPriceDescending:
                query = query.OrderByDescending(c => c.MinPrice);
                break;

			case MaxPriceAscending:
                query = query.OrderBy(c => c.MaxPrice);
                break;
            case MaxPriceDescending:
                query = query.OrderByDescending(c => c.MaxPrice);
                break;

			case MinRentalTimeAscending:
                query = query.OrderBy(c => c.MinRentalTime);
                break;
            case MinRentalTimeDescending:
                query = query.OrderByDescending(c => c.MinRentalTime);
                break;

			case MaxRentalTimeAscending:
                query = query.OrderBy(c => c.MaxRentalTime);
                break;
            case MaxRentalTimeDescending:
                query = query.OrderByDescending(c => c.MaxRentalTime);
                break;

			case MinFloorAreaAscending:
                query = query.OrderBy(c => c.MinFloorArea);
                break;
            case MinFloorAreaDescending:
                query = query.OrderByDescending(c => c.MinFloorArea);
                break;

			case MaxFloorAreaAscending:
                query = query.OrderBy(c => c.MaxFloorArea);
                break;
            case MaxFloorAreaDescending:
                query = query.OrderByDescending(c => c.MaxFloorArea);
                break;

			case DescriptionAscending:
                query = query.OrderBy(c => c.Description);
                break;
            case DescriptionDescending:
                query = query.OrderByDescending(c => c.Description);
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

	public const int BrandIdAscending = 3;
	public const int BrandIdDescending = 3 + 1;

	public const int AreaAscending = 5;
	public const int AreaDescending = 5 + 1;

	public const int AmountAscending = 7;
	public const int AmountDescending = 7 + 1;

	public const int AmountFrontageAscending = 9;
	public const int AmountFrontageDescending = 9 + 1;

	public const int MinPriceAscending = 11;
	public const int MinPriceDescending = 11 + 1;

	public const int MaxPriceAscending = 13;
	public const int MaxPriceDescending = 13 + 1;

	public const int MinRentalTimeAscending = 15;
	public const int MinRentalTimeDescending = 15 + 1;

	public const int MaxRentalTimeAscending = 17;
	public const int MaxRentalTimeDescending = 17 + 1;

	public const int MinFloorAreaAscending = 19;
	public const int MinFloorAreaDescending = 19 + 1;

	public const int MaxFloorAreaAscending = 21;
	public const int MaxFloorAreaDescending = 21 + 1;

	public const int DescriptionAscending = 23;
	public const int DescriptionDescending = 23 + 1;

	public const int StatusAscending = 25;
	public const int StatusDescending = 25 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public decimal? BrandId { get; set; }
	public decimal? MaxBrandId { get; set; }
	public decimal? MinBrandId { get; set; }
	public IEnumerable<decimal?>? ListBrandId { get; set; }

	public string? Area { get; set; }

	public int? Amount { get; set; }
	public int? MaxAmount { get; set; }
	public int? MinAmount { get; set; }
	public IEnumerable<int?>? ListAmount { get; set; }

	public int? AmountFrontage { get; set; }
	public int? MaxAmountFrontage { get; set; }
	public int? MinAmountFrontage { get; set; }
	public IEnumerable<int?>? ListAmountFrontage { get; set; }

	public decimal? MinPrice { get; set; }
	public decimal? MaxMinPrice { get; set; }
	public decimal? MinMinPrice { get; set; }
	public IEnumerable<decimal?>? ListMinPrice { get; set; }

	public decimal? MaxPrice { get; set; }
	public decimal? MaxMaxPrice { get; set; }
	public decimal? MinMaxPrice { get; set; }
	public IEnumerable<decimal?>? ListMaxPrice { get; set; }

	public DateTime? MinRentalTime { get; set; }
	public DateTime? MaxMinRentalTime { get; set; }
	public DateTime? MinMinRentalTime { get; set; }
	public IEnumerable<DateTime?>? ListMinRentalTime { get; set; }

	public DateTime? MaxRentalTime { get; set; }
	public DateTime? MaxMaxRentalTime { get; set; }
	public DateTime? MinMaxRentalTime { get; set; }
	public IEnumerable<DateTime?>? ListMaxRentalTime { get; set; }

	public double? MinFloorArea { get; set; }
	public double? MaxMinFloorArea { get; set; }
	public double? MinMinFloorArea { get; set; }
	public IEnumerable<double?>? ListMinFloorArea { get; set; }

	public double? MaxFloorArea { get; set; }
	public double? MaxMaxFloorArea { get; set; }
	public double? MinMaxFloorArea { get; set; }
	public IEnumerable<double?>? ListMaxFloorArea { get; set; }

	public double? Status { get; set; }
	public double? MaxStatus { get; set; }
	public double? MinStatus { get; set; }
	public IEnumerable<double?>? ListStatus { get; set; }

	public string? Description { get; set; }

}

