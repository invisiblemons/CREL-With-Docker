namespace CREL_BE.Helpers;

public class PasswordGenerater
{
    private const string lowererCases = "abcdefghijklmnopqrstuvwxyz";
    private const string upperCases = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string digit = "0123456789";
    private const string specials = "!@#$%^&*()";
    private const string all = lowererCases + upperCases + digit + specials;
    private string passwordLowerCases = "";
    private string passwordUpperCases = "";
    private string passwordDigit = "";
    private string passwordSpecials = "";
    public int MinUpperCases { get; }
    public int MinLowerCases { get; }
    public int MinDigit { get; set; }
    public int MinSpecials { get; set; }
    public int Length { get; set; }

    public PasswordGenerater(int minLowerCases, int minUpperCases, int minDigit, int minSpecials, int Length)
    {
        this.Length = Length;
        this.MinSpecials = minSpecials;
        this.MinDigit = minDigit;
        this.MinLowerCases = minLowerCases;
        this.MinUpperCases = minUpperCases;
    }

    private void Reset()
    {
        passwordLowerCases = "";
        passwordUpperCases = "";
        passwordDigit = "";
        passwordSpecials = "";
    }

    public string Generate()
    {
        if (Length < MinLowerCases + MinUpperCases + MinDigit + MinSpecials)
        {
            throw new ArgumentException("Minimum requirements exceed password length.");
        }
        Reset();
        string password = "";
        var random = new Random();
        for (int i = 0; i < MinLowerCases; i++)
        {
            passwordLowerCases += lowererCases[random.Next(lowererCases.Length)];
        }
        for (int i = 0; i < MinUpperCases; i++)
        {
            passwordUpperCases += upperCases[random.Next(upperCases.Length)];
        }
        for (int i = 0; i < MinDigit; i++)
        {
            passwordDigit += digit[random.Next(digit.Length)];
        }
        for (int i = 0; i < MinSpecials; i++)
        {
            passwordSpecials += specials[random.Next(specials.Length)];
        }
        for (int i = 0; i < Length - (MinLowerCases + MinUpperCases + MinDigit + MinSpecials); i++)
        {
            password += all[random.Next(all.Length)];
        }
        return password + passwordLowerCases + passwordUpperCases + passwordDigit + passwordSpecials;
    }
}
