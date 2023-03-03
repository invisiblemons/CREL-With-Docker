using System.Net;
using System.Net.Mail;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using CREL_BE.Dtos;

namespace CREL_BE.Helpers;

public class EmailHelper
{
    public static bool SendDebugEmail(string content)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress("crelfall2022@gmail.com", "DataD");
        message.To.Add(new MailAddress("crelfall2022@gmail.com", "Commercial Real Estate Leasing"));
        message.Subject = "Log";
        message.IsBodyHtml = false;
        message.Body = content;
        smtp.Port = 587;
        smtp.Host = "email-smtp.ap-southeast-1.amazonaws.com"; //for gmail host  
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("AKIAQNLNCLF6X7LYUREV", "BPYkcEfrhuXXFeOUoCRFMoEstK/73xYcREqSxJuZTAlY");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Send(message);
        return true;
    }

    public static bool SendEmailResetPassword(string userName, string name, string newPassword, string email)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress("crelfall2022@gmail.com", "Commercial Real Estate Leasing");
        message.To.Add(new MailAddress(email, name));
        message.Subject = "Tạo mới mật khẩu";
        message.IsBodyHtml = true; //to make message body as html  
        message.Body = EmailTemplate.htmlResetPasswordTemplate
                            //.Replace("<CrelUserName>", userName)
                            .Replace("<CrelName>", name)
                            .Replace("<CrelNewPassword>", newPassword);
        smtp.Port = 587;
        smtp.Host = "email-smtp.ap-southeast-1.amazonaws.com"; //for gmail host  
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("AKIAQNLNCLF6X7LYUREV", "BPYkcEfrhuXXFeOUoCRFMoEstK/73xYcREqSxJuZTAlY");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Send(message);
        return true;
    }

    private static string getHtmlFromPropertyDtos(ICollection<PropertyDto> properties)
    {
        string result = "";
        foreach (PropertyDto property in properties)
        {
            result += EmailTemplate.htmlPropertyListTemplate
            .Replace("<CrelLink>", $@"https://crel.site/mat-bang-cho-thue/chi-tiet?id={property.Id}")
            .Replace("<CrelAddress>", property.getAddress())
            .Replace("<CrelImageLink>", ((property.Media ?? new List<ShortMediaDto>()).Where((m) => m.Type == 1).FirstOrDefault() ?? new ShortMediaDto()).Link)
            .Replace("<CrelPrice>", (property.Price ?? 0).ToString("### ### ### ###").Trim().Replace(" ", "."));
        }
        return result;
    }

    public static bool SendEmailBrandRequest(BrandRequestDto brandRequest)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress("crelfall2022@gmail.com", "Commercial Real Estate Leasing");
        message.To.Add(new MailAddress((brandRequest.Brand ?? new BrandDto()).Email ?? "", (brandRequest.Brand ?? new BrandDto()).Name));
        message.Subject = "Cập nhật thông tin yêu cầu tìm kiếm mặt bằng";
        message.IsBodyHtml = true; //to make message body as html  
        message.Body = EmailTemplate.htmlBrandRequestTemplate
                            //.Replace("<CrelUserName>", userName)
                            .Replace("<CrelUserName>", (brandRequest.Brand ?? new BrandDto()).Name)
                            .Replace("<CrelArea>", brandRequest.Area)
                            .Replace("<CrelMinPrice>", ((brandRequest.MinPrice ?? 0) / 1000000).ToString("## ###.#").Trim().Replace(".", ",").Replace(" ", "."))
                            .Replace("<CrelMaxPrice>", ((brandRequest.MaxPrice ?? 0) / 1000000).ToString("## ###.#").Trim().Replace(".", ",").Replace(" ", "."))
                            .Replace("<CrelMinFloorArea>", brandRequest.MinFloorArea.ToString())
                            .Replace("<CrelMaxFloorArea>", brandRequest.MaxFloorArea.ToString())
                            .Replace("<CrelDescription>", brandRequest.Description)
                            .Replace("<CrelPropertyList>", getHtmlFromPropertyDtos(brandRequest.Properties ?? new List<PropertyDto>()));
        smtp.Port = 587;
        smtp.Host = "email-smtp.ap-southeast-1.amazonaws.com"; //for gmail host  
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("AKIAQNLNCLF6X7LYUREV", "BPYkcEfrhuXXFeOUoCRFMoEstK/73xYcREqSxJuZTAlY");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Send(message);
        return true;
    }

    public static bool SendEmailSuggestProperty(string name, string email, List<PropertyDto> properties)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress("crelfall2022@gmail.com", "Commercial Real Estate Leasing");
        message.To.Add(new MailAddress(email, name));
        message.Subject = "Đề xuất mặt bằng";
        message.IsBodyHtml = true; //to make message body as html  
        message.Body = EmailTemplate.htmlAreaManagerSuggestTemplate
                            //.Replace("<CrelUserName>", userName)
                            .Replace("<CrelUserName>", name)
                            .Replace("<CrelPropertyList>", getHtmlFromPropertyDtos(properties));
        smtp.Port = 587;
        smtp.Host = "email-smtp.ap-southeast-1.amazonaws.com"; //for gmail host  
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("AKIAQNLNCLF6X7LYUREV", "BPYkcEfrhuXXFeOUoCRFMoEstK/73xYcREqSxJuZTAlY");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Send(message);
        return true;
    }

    public static bool SendEmailCreateAccount(string userName, string name, string password, string email)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress("crelfall2022@gmail.com", "Commercial Real Estate Leasing");
        message.To.Add(new MailAddress(email, name));
        message.Subject = "Tài khoản mới";
        message.IsBodyHtml = true; //to make message body as html  
        message.Body = EmailTemplate.htmlAccountCreateTemplate
                            .Replace("<CrelUserName>", userName)
                            .Replace("<CrelName>", name)
                            .Replace("<CrelPassword>", password);
        smtp.Port = 587;
        smtp.Host = "email-smtp.ap-southeast-1.amazonaws.com"; //for gmail host  
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("AKIAQNLNCLF6X7LYUREV", "BPYkcEfrhuXXFeOUoCRFMoEstK/73xYcREqSxJuZTAlY");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Send(message);
        return true;
    }

    public static bool SendEmailVerifiedAccount(string userName, string name, string email)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress("crelfall2022@gmail.com", "Commercial Real Estate Leasing");
        message.To.Add(new MailAddress(email, name));
        message.Subject = "Tài khoản của bạn đã được duyệt";
        message.IsBodyHtml = true; //to make message body as html  
        message.Body = EmailTemplate.htmlBrandVerifiedTemplate
                            .Replace("<CrelUserName>", userName)
                            .Replace("<CrelName>", name);
        smtp.Port = 587;
        smtp.Host = "email-smtp.ap-southeast-1.amazonaws.com"; //for gmail host  
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("AKIAQNLNCLF6X7LYUREV", "BPYkcEfrhuXXFeOUoCRFMoEstK/73xYcREqSxJuZTAlY");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Send(message);
        return true;
    }

    public static bool SendEmailNotVerifiedAccount(string userName, string name, string email, string rejectMessage)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress("crelfall2022@gmail.com", "Commercial Real Estate Leasing");
        message.To.Add(new MailAddress(email, name));
        message.Subject = "Tài khoản của bạn đã không được duyệt";
        message.IsBodyHtml = true; //to make message body as html  
        message.Body = EmailTemplate.htmlBrandNotVerifiedTemplate
                            .Replace("<CrelReason>", rejectMessage)
                            .Replace("<CrelUserName>", userName)
                            .Replace("<CrelName>", name);
        smtp.Port = 587;
        smtp.Host = "email-smtp.ap-southeast-1.amazonaws.com"; //for gmail host  
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("AKIAQNLNCLF6X7LYUREV", "BPYkcEfrhuXXFeOUoCRFMoEstK/73xYcREqSxJuZTAlY");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Send(message);
        return true;
    }

    public static bool SendEmailApproveAppoinment(string name, string email, string onDateTime, string link, string description, string brokerName, string phoneNumber)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress("crelfall2022@gmail.com", "Commercial Real Estate Leasing");
        message.To.Add(new MailAddress(email, name));
        message.Subject = "Bạn có cuộc hẹn";
        message.IsBodyHtml = true; //to make message body as html  
        message.Body = EmailTemplate.htmlBrokerApproveAppointmentTemplate
                            .Replace("CrelLink", link)
                            .Replace("CrelOnDateTime", onDateTime)
                            .Replace("CrelDescription", description)
                            .Replace("CrelBrokerName", brokerName)
                            .Replace("CrelPhoneNumber", phoneNumber);
        smtp.Port = 587;
        smtp.Host = "email-smtp.ap-southeast-1.amazonaws.com"; //for gmail host  
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("AKIAQNLNCLF6X7LYUREV", "BPYkcEfrhuXXFeOUoCRFMoEstK/73xYcREqSxJuZTAlY");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Send(message);
        return true;
    }

    public static bool SendEmailNotApproveAppoinment(string name, string email, string link,  string reason)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress("crelfall2022@gmail.com", "Commercial Real Estate Leasing");
        message.To.Add(new MailAddress(email, name));
        message.Subject = "Bạn có cuộc hẹn bị từ chối";
        message.IsBodyHtml = true; //to make message body as html  
        message.Body = EmailTemplate.htmlBrokerNotApproveAppointmentTemplate
                            .Replace("<CrelLink>", link)
                            .Replace("<CrelReason>", reason);
        smtp.Port = 587;
        smtp.Host = "email-smtp.ap-southeast-1.amazonaws.com"; //for gmail host  
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("AKIAQNLNCLF6X7LYUREV", "BPYkcEfrhuXXFeOUoCRFMoEstK/73xYcREqSxJuZTAlY");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Send(message);
        return true;
    }

    public static bool SendEmailContractSigned(string name, string email, string link)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress("crelfall2022@gmail.com", "Commercial Real Estate Leasing");
        message.To.Add(new MailAddress(email, name));
        message.Subject = "Bạn có hợp đồng mới";
        message.IsBodyHtml = true; //to make message body as html  
        message.Body = EmailTemplate.htmlContractSignedTemplate
                            .Replace("CrelLink", link);
        smtp.Port = 587;
        smtp.Host = "email-smtp.ap-southeast-1.amazonaws.com"; //for gmail host  
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("AKIAQNLNCLF6X7LYUREV", "BPYkcEfrhuXXFeOUoCRFMoEstK/73xYcREqSxJuZTAlY");
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.Send(message);
        return true;
    }
}
