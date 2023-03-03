using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class BrokerDto
{
	public decimal? Id { get; set; }
	public string? UserName { get; set; }
	public DateTime? Birthday { get; set; }
	public bool? Gender { get; set; }
	public string? Name { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public int? Status { get; set; }
	public decimal? TeamId { get; set; }
	public string? AvatarFileId { get; set; }
	public string? AvatarLink { get; set; }
	public IdNameDto? Team { get; set; }
}
