using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class BrandDto
{
	public decimal? Id { get; set; }
	public string? UserName { get; set; }
	public string? Name { get; set; }
	public string? FirebaseUid { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public int? Status { get; set; }
	public string? Description { get; set; }
	public string? RejectMessage { get; set; }
	public string? AvatarFileId { get; set; }
	public string? AvatarLink { get; set; }
	public decimal? IndustryId { get; set; }
	public string? RegistrationNumber { get; set; }
	
	public bool IsNeedUpdatePassword { get; set; } = false;

	public virtual ICollection<StoreDto>? Stores { get; set; }
}
