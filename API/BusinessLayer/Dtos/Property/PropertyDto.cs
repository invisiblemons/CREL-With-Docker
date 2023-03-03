using System.ComponentModel.DataAnnotations;

namespace CREL_BE.Dtos;
public class PropertyDto
{
    public decimal? Id { get; set; }
    public decimal? LocationId { get; set; }
    public decimal? BrokerId { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
    public int? Status { get; set; }
    public string? Description { get; set; }
    public string? RejectReason { get; set; }
    public int? Type { get; set; }
    public string? Name { get; set; }
    public decimal? ProjectId { get; set; }
    public int? Direction { get; set; }
    public string? Floor { get; set; }
    public double? FloorArea { get; set; }
    public double? Area { get; set; }
    public double? Frontage { get; set; }
    public int? Certificates { get; set; }
    public double? Vertical { get; set; }
    public double? Horizontal { get; set; }
    public double? RoadWidth { get; set; }
    public string? RentalCondition { get; set; }
    public string? RentalTerm { get; set; }
    public string? DepositTerm { get; set; }
    public string? PaymentTerm { get; set; }
    public decimal? Price { get; set; }
    public int? NumberOfFrontage { get; set; }

    public ICollection<ShortMediaDto>? Media { get; set; }
    public ICollection<OwnerDto>? Owners { get; set; }
    public LocationDto? Location { get; set; }
    public IdNameDto? Project { get; set; }
    public string getAddress()
    {
        string result = "";
        if (Location != null)
        {
            result += Location.Address + ", ";
            if (Location.StreetSegment != null)
            {
                result += Location.StreetSegment.Name + ", ";
            }
            if (Location.Ward != null)
            {
                result += Location.Ward.Name + ", ";
                if (Location.Ward.District != null)
                {
                    result += Location.Ward.District.Name + ", ";
                }
            }
        }
        return result + " Thành Phố Hồ Chí Minh";
    }
}
