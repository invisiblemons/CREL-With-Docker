using CREL_BE.Entities;
using CREL_BE.Helpers;

namespace CREL_BE.Filter;
public class MediaFilter : PaginationParams
{
    public IQueryable<Media> ApplyFilter(IQueryable<Media> query)
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

		if (Link != null) {
			query = query.Where(e => (e.Link != null) && (e.Link.Contains(Link)));
		}

		if (FileId != null) {
			query = query.Where(e => (e.FileId != null) && (e.FileId.Contains(FileId)));
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
            
			case IdAscending:
                query = query.OrderBy(c => c.Id);
                break;
            case IdDescending:
                query = query.OrderByDescending(c => c.Id);
                break;

			case LinkAscending:
                query = query.OrderBy(c => c.Link);
                break;
            case LinkDescending:
                query = query.OrderByDescending(c => c.Link);
                break;

			case FileIdAscending:
                query = query.OrderBy(c => c.FileId);
                break;
            case FileIdDescending:
                query = query.OrderByDescending(c => c.FileId);
                break;

			case PropertyIdAscending:
                query = query.OrderBy(c => c.PropertyId);
                break;
            case PropertyIdDescending:
                query = query.OrderByDescending(c => c.PropertyId);
                break;

			case ProjectIdAscending:
                query = query.OrderBy(c => c.ProjectId);
                break;
            case ProjectIdDescending:
                query = query.OrderByDescending(c => c.ProjectId);
                break;

			case ContractIdAscending:
                query = query.OrderBy(c => c.ContractId);
                break;
            case ContractIdDescending:
                query = query.OrderByDescending(c => c.ContractId);
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
	
	public const int IdAscending = 1;
	public const int IdDescending = 1 + 1;

	public const int LinkAscending = 3;
	public const int LinkDescending = 3 + 1;

	public const int FileIdAscending = 5;
	public const int FileIdDescending = 5 + 1;

	public const int PropertyIdAscending = 7;
	public const int PropertyIdDescending = 7 + 1;

	public const int ProjectIdAscending = 9;
	public const int ProjectIdDescending = 9 + 1;

	public const int ContractIdAscending = 11;
	public const int ContractIdDescending = 11 + 1;

	public const int TypeAscending = 13;
	public const int TypeDescending = 13 + 1;

	
	public decimal? Id { get; set; }
	public decimal? MaxId { get; set; }
	public decimal? MinId { get; set; }
	public IEnumerable<decimal?>? ListId { get; set; }

	public string? Link { get; set; }

	public string? FileId { get; set; }

	public decimal? PropertyId { get; set; }
	public decimal? MaxPropertyId { get; set; }
	public decimal? MinPropertyId { get; set; }
	public IEnumerable<decimal?>? ListPropertyId { get; set; }

	public decimal? ProjectId { get; set; }
	public decimal? MaxProjectId { get; set; }
	public decimal? MinProjectId { get; set; }
	public IEnumerable<decimal?>? ListProjectId { get; set; }

	public decimal? ContractId { get; set; }
	public decimal? MaxContractId { get; set; }
	public decimal? MinContractId { get; set; }
	public IEnumerable<decimal?>? ListContractId { get; set; }

	public int? Type { get; set; }
	public int? MaxType { get; set; }
	public int? MinType { get; set; }
	public IEnumerable<int?>? ListType { get; set; }

}

