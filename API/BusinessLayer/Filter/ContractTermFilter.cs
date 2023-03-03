using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class ContractTermFilter : PaginationParams
{
    public IQueryable<ContractTerm> ApplyFilter(IQueryable<ContractTerm> query)
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

		if (ContractId != null) {
			query = query.Where(e => e.ContractId == ContractId);
		}
		if (MaxContractId != null) {
			query = query.Where(e => e.ContractId <= MaxContractId);
		}
		if (MinContractId != null) {
			query = query.Where(e => e.ContractId >= MinContractId);
		}
		if (ListContractId != null) {
			query = query.Where(e => ListContractId.Contains(e.ContractId));
		}

		if (ContractTermId != null) {
			query = query.Where(e => e.ContractTermId == ContractTermId);
		}
		if (MaxContractTermId != null) {
			query = query.Where(e => e.ContractTermId <= MaxContractTermId);
		}
		if (MinContractTermId != null) {
			query = query.Where(e => e.ContractTermId >= MinContractTermId);
		}
		if (ListContractTermId != null) {
			query = query.Where(e => ListContractTermId.Contains(e.ContractTermId));
		}

		if (Title != null) {
			query = query.Where(e => (e.Title != null) && (e.Title.Contains(Title)));
		}

		if (Content != null) {
			query = query.Where(e => (e.Content != null) && (e.Content.Contains(Content)));
		}

		if (Index != null) {
			query = query.Where(e => e.Index == Index);
		}
		if (MaxIndex != null) {
			query = query.Where(e => e.Index <= MaxIndex);
		}
		if (MinIndex != null) {
			query = query.Where(e => e.Index >= MinIndex);
		}
		if (ListIndex != null) {
			query = query.Where(e => ListIndex.Contains(e.Index));
		}

		
		switch (OrderBy)
        {
            
			case IdAscending:
                query = query.OrderBy(c => c.Id);
                break;
            case IdDescending:
                query = query.OrderByDescending(c => c.Id);
                break;

			case ContractIdAscending:
                query = query.OrderBy(c => c.ContractId);
                break;
            case ContractIdDescending:
                query = query.OrderByDescending(c => c.ContractId);
                break;

			case ContractTermIdAscending:
                query = query.OrderBy(c => c.ContractTermId);
                break;
            case ContractTermIdDescending:
                query = query.OrderByDescending(c => c.ContractTermId);
                break;

			case TitleAscending:
                query = query.OrderBy(c => c.Title);
                break;
            case TitleDescending:
                query = query.OrderByDescending(c => c.Title);
                break;

			case ContentAscending:
                query = query.OrderBy(c => c.Content);
                break;
            case ContentDescending:
                query = query.OrderByDescending(c => c.Content);
                break;

			case IndexAscending:
                query = query.OrderBy(c => c.Index);
                break;
            case IndexDescending:
                query = query.OrderByDescending(c => c.Index);
                break;

        }
		return query;
    }
	
	public int? OrderBy { get; set; }
	
	public const int IdAscending = 1;
	public const int IdDescending = 1 + 1;

	public const int ContractIdAscending = 3;
	public const int ContractIdDescending = 3 + 1;

	public const int ContractTermIdAscending = 5;
	public const int ContractTermIdDescending = 5 + 1;

	public const int TitleAscending = 7;
	public const int TitleDescending = 7 + 1;

	public const int ContentAscending = 9;
	public const int ContentDescending = 9 + 1;

	public const int IndexAscending = 11;
	public const int IndexDescending = 11 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public decimal? ContractId { get; set; }
	public decimal? MaxContractId { get; set; }
	public decimal? MinContractId { get; set; }
	public IEnumerable<decimal?>? ListContractId { get; set; }

	public decimal? ContractTermId { get; set; }
	public decimal? MaxContractTermId { get; set; }
	public decimal? MinContractTermId { get; set; }
	public IEnumerable<decimal?>? ListContractTermId { get; set; }

	public string? Title { get; set; }

	public string? Content { get; set; }

	public int? Index { get; set; }
	public int? MaxIndex { get; set; }
	public int? MinIndex { get; set; }
	public IEnumerable<int?>? ListIndex { get; set; }

}

