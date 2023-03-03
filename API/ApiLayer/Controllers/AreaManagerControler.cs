using AutoMapper;
using CREL_BE.Extensions;
using CREL_BE.Helpers;
using CREL_BE.Entities;
using CREL_BE.Dtos;
using CREL_BE.Filter;
using Microsoft.AspNetCore.Mvc;
using CREL_BE.Services;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using CREL_BE.ApiLayer.Helpers;

namespace CREL_BE.Controllers;
[ApiController]
[Route("api/" + MyConstant.apiVersion + "/area-managers")]
public class AreaManagerController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;
    private readonly IFileService fileService;
    private readonly IConfiguration configuration;

    public AreaManagerController(IUnitOfWorkService unitOfWorkService, IMapper mapper, IFileService fileService, IConfiguration configuration)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
        this.fileService = fileService;
        this.configuration = configuration;
    }

    /// <summary>
    /// Get AreaManager by ID
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<AreaManagerDto>> GetAreaManager(decimal id)
    {
        AreaManagerDto? areaManagerDto = await unitOfWorkService.AreaManagerService.GetAreaManagerDtoById(id);
        if (areaManagerDto == null)
        {
            return NotFound();
        }
        return Ok(areaManagerDto);
    }

    /// <summary>
    /// Search AreaManager
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<PagedList<AreaManagerDto>>> SearchAreaManager([FromQuery] AreaManagerFilter areaManagerFilter)
    {
        PagedList<AreaManagerDto> pagedListAreaManager = await unitOfWorkService.AreaManagerService.GetAreaManagerDtos(areaManagerFilter);
        Response.AddPaginationHeader(pagedListAreaManager);
        return Ok(pagedListAreaManager);
    }

    /// <summary>
    /// Create AreaManager
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<AreaManagerDto>> CreateAreaManager([FromBody] CreateAreaManagerDto createAreaManagerDto)
    {
        createAreaManagerDto.Status = 1;
        AreaManager areaManager = mapper.Map<AreaManager>(createAreaManagerDto);
        var password = new PasswordGenerater(
            1, 1, 1, 1, 8
        ).Generate();
        if (await UserNameEmailPhoneNumberChecker.CheckUserName(createAreaManagerDto.UserName, unitOfWorkService))
        {
            return BadRequest(new { message = "UserName is already exist" });
        }

        if (await UserNameEmailPhoneNumberChecker.CheckEmail(createAreaManagerDto.Email, unitOfWorkService))
        {
           return BadRequest(new { message = "Email is already exist" });
        }

        if (await UserNameEmailPhoneNumberChecker.CheckPhoneNumber(createAreaManagerDto.PhoneNumber, unitOfWorkService))
        {
            return BadRequest(new { message = "PhoneNumber is already exist" });
        }
        var hmac = new HMACSHA512();
        areaManager.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        areaManager.PasswordSalt = hmac.Key;
        await unitOfWorkService.AreaManagerService.Add(areaManager);
        await unitOfWorkService.CommitAsync();
        EmailHelper.SendEmailCreateAccount(createAreaManagerDto.UserName!, "Người quản lý khu vực " + createAreaManagerDto.Name!, password, createAreaManagerDto.Email!);
        return Ok(mapper.Map<AreaManagerDto>(areaManager));
    }

    /// <summary>
    /// Update AreaManager
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<AreaManagerDto>> UpdateAreaManager([FromBody] UpdateAreaManagerDto updateAreaManagerDto)
    {
        AreaManager? areaManager = await unitOfWorkService.AreaManagerService.GetAreaManagerById(updateAreaManagerDto.Id!.Value);
        if (areaManager == null)
        {
            return NotFound();
        }
        if (User.GetUserRole() == "AreaManager")
        {
            updateAreaManagerDto.Status = areaManager.Status;// Hoi Ming
        }
        if (updateAreaManagerDto.UserName != areaManager.UserName && await UserNameEmailPhoneNumberChecker.CheckUserName(updateAreaManagerDto.UserName, unitOfWorkService))
        {
            return BadRequest(new { message = "UserName is already exist" });
        }

        if (updateAreaManagerDto.Email != areaManager.Email && await UserNameEmailPhoneNumberChecker.CheckEmail(updateAreaManagerDto.Email, unitOfWorkService))
        {
           return BadRequest(new { message = "Email is already exist" });
        }

        if (updateAreaManagerDto.PhoneNumber != areaManager.PhoneNumber && await UserNameEmailPhoneNumberChecker.CheckPhoneNumber(updateAreaManagerDto.PhoneNumber, unitOfWorkService))
        {
            return BadRequest(new { message = "PhoneNumber is already exist" });
        }
        mapper.Map(updateAreaManagerDto, areaManager);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<AreaManagerDto>(areaManager));
    }

    /// <summary>
    /// Delete AreaManager
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<AreaManagerDto>> DeleteAreaManager(decimal id)
    {
        AreaManager? areaManager = await unitOfWorkService.AreaManagerService.GetAreaManagerById(id);
        if (areaManager == null)
        {
            return NotFound();
        }
        areaManager = unitOfWorkService.AreaManagerService.Delete(areaManager);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<AreaManagerDto>(areaManager));
    }

    /// <summary>
    /// Login for areaManager
    /// </summary>
    [HttpPost("authenticate")]
    public async Task<ActionResult<AuthenticateRespone<AreaManagerDto>>> Authenticate([FromBody] AuthenticateRequest request)
    {
        if (request.UserName != null && request.Password != null)
        {
            var areaManager = await unitOfWorkService.AreaManagerService.GetAreaManagerByUserName(request.UserName);
            if (areaManager == null)
            {
                return BadRequest(new { message = "Invalid username or password" });
            }
            if (areaManager.Status == MyConstant.AreaManager.Status.Deleted)
            {
                return BadRequest(new { message = "Account is inactive" });
            }

            var hmac = new HMACSHA512(areaManager.PasswordSalt!);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != areaManager.PasswordHash![i])
                {
                    return BadRequest(new { message = "Invalid username or password" });
                }
            }

            return Ok(new AuthenticateRespone<AreaManagerDto>
            {
                Token = TokenHelpers.CreateToken(areaManager.Id.ToString(), "AreaManager", configuration),
                User = mapper.Map<AreaManagerDto>(areaManager)
            });
        }
        return BadRequest(new { message = "Invalid username or password" });
    }

    /// <summary>
    /// Admin update areaManager password
    /// </summary>
    //[Authorize(Policy = "RequireAgentOrAreaManagerRole")]
    [HttpPut("{id}/password")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> ChangePassword(int id, [FromBody] string newPassword)
    {
        var areaManager = await unitOfWorkService.AreaManagerService.GetAreaManagerById(id);
        if (areaManager == null)
        {
            return NotFound();
        }

        var hmac = new HMACSHA512();
        areaManager.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword!));
        areaManager.PasswordSalt = hmac.Key;
        await unitOfWorkService.CommitAsync();
        return Ok();
    }

    /// <summary>
    /// AreaManager Update their password
    /// </summary>
    [HttpPut("profile/password")]
    [Authorize(Roles = "AreaManager")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var areaManager = await unitOfWorkService.AreaManagerService.GetAreaManagerById(User.GetUserId());
        if (areaManager == null)
        {
            return NotFound();
        }

        var hmac = new HMACSHA512(areaManager.PasswordSalt!);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.OldPassword!));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != areaManager.PasswordHash![i])
            {
                return BadRequest(new { message = "Invalid old password" });
            }
        }

        hmac = new HMACSHA512();
        areaManager.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword!));
        areaManager.PasswordSalt = hmac.Key;
        areaManager.Status = 2;
        await unitOfWorkService.CommitAsync();
        return Ok();
    }

    /// <summary>
    /// Update Area Manager avatar
    /// </summary>
    //[Authorize(Policy = "RequireAreaManagerRole")]
    [HttpPut("{id}/avatar")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<AreaManagerDto>> UpdateAreaManagerAvatar(decimal id, IFormFile file)
    {
        if (file == null)
        {
            return BadRequest(new { message = "No file" });
        }

        var areaManager = await unitOfWorkService.AreaManagerService.GetAreaManagerById(id);

        if (areaManager == null)
        {
            return NotFound();
        }

        if (areaManager.AvatarFileId != null)
        {
            await fileService.DeleteFileAsync(areaManager.AvatarFileId!);
        }
        var imagekitResponse = await fileService.UploadFileAsync(file);
        areaManager.AvatarFileId = imagekitResponse.FileId;
        areaManager.AvatarLink = imagekitResponse.URL;
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<AreaManagerDto>(areaManager));
    }

    /// <summary>
    /// Area Manager update their avatar
    /// </summary>
    //[Authorize(Policy = "RequireAreaManagerRole")]
    [HttpPut("profile/avatar")]
    [Authorize(Roles = "AreaManager")]
    public async Task<ActionResult<AreaManagerDto>> UpdateAreaManagerAvatar(IFormFile file)
    {
        if (file == null)
        {
            return BadRequest(new { message = "No file" });
        }

        var areaManager = await unitOfWorkService.AreaManagerService.GetAreaManagerById(User.GetUserId());

        if (areaManager == null)
        {
            return NotFound();
        }

        if (areaManager.AvatarFileId != null)
        {
            await fileService.DeleteFileAsync(areaManager.AvatarFileId!);
        }
        var imagekitResponse = await fileService.UploadFileAsync(file);
        areaManager.AvatarFileId = imagekitResponse.FileId;
        areaManager.AvatarLink = imagekitResponse.URL;
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<AreaManagerDto>(areaManager));
    }

    /// <summary>
    /// Area Manager reset password
    /// </summary>
    [HttpPut("reset-password")]
    public async Task<ActionResult> ResetPassword([FromBody] string email)
    {
        var areaManager = await unitOfWorkService.AreaManagerService.GetAreaManagerByEmail(email);
        if (areaManager == null || areaManager.Status == MyConstant.AreaManager.Status.Deleted)
        {
            return NotFound();
        }

        var newPassword = new PasswordGenerater(
            1, 1, 1, 1, 8
        ).Generate();

        if ((EmailHelper.SendEmailResetPassword(areaManager.UserName, areaManager.Name, newPassword, areaManager.Email)))
        {
            var hmac = new HMACSHA512();
            areaManager.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
            areaManager.PasswordSalt = hmac.Key;
            areaManager.Status = 1;
            await unitOfWorkService.CommitAsync();
            return Ok();
        }
        return Ok(new { message = "Can not send mail" });
    }
}
