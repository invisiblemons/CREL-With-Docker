using AutoMapper;
using CREL_BE.Helpers;
using CREL_BE.Dtos;
using Microsoft.AspNetCore.Mvc;
using CREL_BE.Services;

namespace CREL_BE.Controllers;
[ApiController]
[Route("api/" + MyConstant.apiVersion + "/admins")]
public class AdminController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;
    private readonly IFileService fileService;
    private readonly IConfiguration configuration;

    public AdminController(IUnitOfWorkService unitOfWorkService, IMapper mapper, IFileService fileService, IConfiguration configuration)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
        this.fileService = fileService;
        this.configuration = configuration;
    }

    /// <summary>
    /// Login for admin
    /// </summary>
    [HttpPost("authenticate")]
    public ActionResult<AuthenticateRespone<string>> Authenticate([FromBody] AuthenticateRequest request)
    {
        if (request.UserName == configuration["Admin:UserName"] && request.Password == configuration["Admin:Password"])
        {

            return Ok(new AuthenticateRespone<AdminDto>
            {
                Token = TokenHelpers.CreateToken(request.UserName, "Admin", configuration),
                User = new AdminDto
                {
                    UserName = request.UserName
                }
            });
        }
        return BadRequest(new { message = "Invalid username or password" });
    }
}
