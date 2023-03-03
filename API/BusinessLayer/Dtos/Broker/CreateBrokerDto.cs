using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class CreateBrokerDto
{
	[Required]
	public string? UserName { get; set; }
	[Required]
	public DateTime? Birthday { get; set; }
	[Required]
	public bool? Gender { get; set; }
	[Required]
	public string? Name { get; set; }
	[Required]
	public string? Email { get; set; }
	[Required]
	public string? PhoneNumber { get; set; }
	public int? Status { get; set; }
	public decimal? TeamId { get; set; }
	public string? AvatarFileId { get; set; }
	public string? AvatarLink { get; set; }

}
