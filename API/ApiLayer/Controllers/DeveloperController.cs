using AutoMapper;
using CREL_BE.Helpers;
using CREL_BE.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CREL_BE.Services;
using Newtonsoft.Json;
using CREL_BE.ApiLayer.Helpers;

namespace CREL_BE.Controllers;
[ApiController]
[Route("api/" + MyConstant.apiVersion + "/developer")]
public class DeveloperController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IHostEnvironment env;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    public DeveloperController(IUnitOfWorkService unitOfWorkService, IHostEnvironment env, IMapper mapper, IConfiguration configuration)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.env = env;
        this.mapper = mapper;
        this.configuration = configuration;
    }

    [HttpGet("admintoken")]
    public ActionResult<string> AdminToken()
    {
        return Ok($"Bearer {TokenHelpers.CreateToken("testadmin", "Admin", configuration)}");
    }

    [HttpGet("amtoken")]
    public ActionResult<string> AmToken()
    {
        return Ok($"Bearer {TokenHelpers.CreateToken("testam", "AreaManager", configuration)}");
    }

    [HttpGet("brandtoken")]
    public ActionResult<string> BrandToken()
    {
        return Ok($"Bearer {TokenHelpers.CreateToken("testbrand", "Brand", configuration)}");
    }

    [HttpGet("brokertoken")]
    public ActionResult<string> BrokerToken()
    {
        return Ok($"Bearer {TokenHelpers.CreateToken("testbroker", "Broker", configuration)}");
    }

    [HttpGet("log")]
    public ActionResult<string> GetLog()
    {
        return Ok(DevelopHelper.ReadLog());
    }

    [HttpPost("log")]
    public ActionResult WriteLog(string st)
    {
        DevelopHelper.WriteLog(st);
        return Ok();
    }

    [HttpDelete("log")]
    public ActionResult<string> ClearLog()
    {
        DevelopHelper.ClearLog();
        return Ok();
    }

    [HttpGet("environment")]
    public ActionResult Environment()
    {
        return Ok(env.EnvironmentName);
    }


    ///<response code="403">Unauthorized</response>
    [Authorize(Policy = "RequireStaffRole")]
    [HttpGet("staff-only")]
    public ActionResult StaffOnly()
    {
        return Ok("Staff can see this");
    }

    ///<response code="403">Unauthorized</response>
    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("admin-only")]
    public ActionResult AdminOnly()
    {
        return Ok("Admins can see this");
    }

    /// <summary>
    /// Initial district ward street segment
    /// </summary>
    [HttpPost("district-ward-street-segment")]
    public ActionResult Initial([FromBody] Welcome welcome)
    {
        foreach (var d in welcome.District)
        {
            unitOfWorkService.DistrictService.Add(new Entities.District()
            {
                Name = d.Pre + " " + d.Name,
                Wards = d.Ward.Select(w => new Entities.Ward()
                {
                    Name = w.Pre + " " + w.Name,
                    Status = 1
                }).ToList(),
                StreetSegments = d.Street.Select(s => new Entities.StreetSegment()
                {
                    Name = s,
                    Status = 1
                }).ToList(),
                Status = 1
            }
            );
        }
        unitOfWorkService.CommitAsync();
        return Ok();
    }

    public partial class Welcome
    {
        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("district")]
        public List<MetaDataDistrict> District { get; set; } = new List<MetaDataDistrict>();
    }

    public partial class MetaDataDistrict
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("pre")]
        public string Pre { get; set; } = string.Empty;

        [JsonProperty("ward")]
        public List<MetaDataWard> Ward { get; set; } = new List<MetaDataWard>();

        [JsonProperty("street")]
        public List<string> Street { get; set; } = new List<string>();
    }

    public partial class MetaDataWard
    {
        [JsonProperty("name")]
        public string Name { get; set; } = String.Empty;

        [JsonProperty("pre")]
        public string Pre { get; set; } = String.Empty;
    }

    // //speed appointment
    // [HttpPost("speed-appointment")]
    // public async Task<ActionResult> SpeedAppointment()
    // {
    //     await unitOfWorkService.AppointmentService.Add(new Appointment
    //     {
    //         BrandId = 2,
    //         BrokerId = 1,
    //         OnDateTime = DateTime.Now.AddMinutes(2)
    //     });

    //     await unitOfWorkService.CommitAsync();
    //     AppointmentReminderService.LastUpdate = DateTime.Now.AddDays(-1);
    //     return Ok();
    // }

    // //get list appointment
    // [HttpGet("get-list-appointment")]
    // public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetListAppointmentAsync()
    // {
    //     var listAppointment = await unitOfWorkService.AppointmentService.GetDtosWithoutPaging(new AppointmentFilter()
    //     {
    //         OnDateTime = DateTime.Now.Date,
    //         OrderBy = AppointmentFilter.OnDateTimeAscending
    //     });
    //     return Ok(listAppointment);
    // }

    [HttpPost("notification")]
    public async Task<ActionResult<string>> CreateNotificationAsync(NotificationDto notificationDto)
    {
        return Ok(await NotificationHelper.SendNotifications(notificationDto));
    }

    // [HttpPost("export-contract")]
    // public async Task<IActionResult> ExportContract(ContractInformationForExport ContractInformationForExport)
    // {
    //     var wordPath = await WordHelper.ExportContractToWordAsync(ContractInformationForExport);
    //     return File( System.IO.File.OpenRead(wordPath),"application/octet-stream", "contract.doc");
    // }
    [HttpPost("export-contract")]
    public async Task<IActionResult> ExportContract(ContractInformationForExport ContractInformationForExport)
    {
        var wordPath = await ExportHelper.ExportContractToWordAsync(ContractInformationForExport);
        return File(System.IO.File.OpenRead(wordPath), "application/octet-stream", "contract.doc");
    }

    [HttpGet("test")]
    public async Task<ActionResult> TestAsync()
    {
        await FireStoreHelper.RemoveEmailAsync("DCM");
        return Ok();
    }
}


