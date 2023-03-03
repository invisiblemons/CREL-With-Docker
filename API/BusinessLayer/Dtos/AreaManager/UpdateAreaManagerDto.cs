using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class UpdateAreaManagerDto
{
	[Required]
	public decimal? Id { get; set; }
	[Required]
	public string? UserName { get; set; }
	[Required]
	public DateTime? Birthday { get; set; }
	[Required]
	public bool? Gender { get; set; }
	[Required]
	public string? Name { get; set; }
	public int? Status { get; set; }
	public string? AvatarFileId { get; set; }
	public string? AvatarLink { get; set; }
	[Required]
	public string? PhoneNumber { get; set; }
	[Required]
	public string? Email { get; set; }

}
