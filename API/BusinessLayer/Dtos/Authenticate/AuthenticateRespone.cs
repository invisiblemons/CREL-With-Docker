namespace CREL_BE.Dtos;
public class AuthenticateRespone<T>
{
    public string? Token{ get; set; }
    public T? User{ get; set; }
}
