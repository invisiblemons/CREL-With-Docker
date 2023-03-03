using AutoMapper;
using CREL_BE.Extensions;
using CREL_BE.Helpers;
using CREL_BE.Entities;
using CREL_BE.Dtos;
using CREL_BE.Filter;
using Microsoft.AspNetCore.Mvc;
using CREL_BE.Services;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using CREL_BE.ApiLayer.Helpers;

namespace CREL_BE.Controllers;
[ApiController]
[Route("api/" + MyConstant.apiVersion + "/brokers")]
public class BrokerController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;
    private readonly IFileService fileService;
    private readonly IConfiguration configuration;

    public BrokerController(IUnitOfWorkService unitOfWorkService, IMapper mapper, IFileService fileService, IConfiguration configuration)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
        this.fileService = fileService;
        this.configuration = configuration;
    }

    /// <summary>
    /// Get Broker by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<BrokerDto>> GetBroker(decimal id)
    {
        BrokerDto? brokerDto = await unitOfWorkService.BrokerService.GetBrokerDtoById(id);
        if (brokerDto == null)
        {
            return NotFound();
        }
        return Ok(brokerDto);
    }

    /// <summary>
    /// Search Broker
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Brand,AreaManager,Admin")]
    public async Task<ActionResult<PagedList<BrokerDto>>> SearchBroker([FromQuery] BrokerFilter brokerFilter)
    {
        PagedList<BrokerDto> pagedListBroker = await unitOfWorkService.BrokerService.GetBrokerDtos(brokerFilter);
        Response.AddPaginationHeader(pagedListBroker);
        return Ok(pagedListBroker);
    }

    /// <summary>
    /// Create Broker
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BrokerDto>> CreateBroker([FromBody] CreateBrokerDto createBrokerDto)
    {
        createBrokerDto.Status = 1;
        Broker broker = mapper.Map<Broker>(createBrokerDto);
        var password = new PasswordGenerater(
            1, 1, 1, 1, 8
        ).Generate();

        if (await UserNameEmailPhoneNumberChecker.CheckUserName(createBrokerDto.UserName, unitOfWorkService))
        {
            return BadRequest(new { message = "UserName is already exist" });
        }

        if (await UserNameEmailPhoneNumberChecker.CheckEmail(createBrokerDto.Email, unitOfWorkService))
        {
           return BadRequest(new { message = "Email is already exist" });
        }

        if (await UserNameEmailPhoneNumberChecker.CheckPhoneNumber(createBrokerDto.PhoneNumber, unitOfWorkService))
        {
            return BadRequest(new { message = "PhoneNumber is already exist" });
        }
        var hmac = new HMACSHA512();
        broker.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        broker.PasswordSalt = hmac.Key;
        await unitOfWorkService.BrokerService.Add(broker);
        await unitOfWorkService.CommitAsync();
        EmailHelper.SendEmailCreateAccount(createBrokerDto.UserName!, "Người môi giới " + createBrokerDto.Name!, password, createBrokerDto.Email!);
        return Ok(mapper.Map<BrokerDto>(broker));
    }

    /// <summary>
    /// Update Broker
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Broker,Admin")]
    public async Task<ActionResult<BrokerDto>> UpdateBroker([FromBody] UpdateBrokerDto updateBrokerDto)
    {
        Broker? broker = await unitOfWorkService.BrokerService.GetBrokerById(updateBrokerDto.Id!.Value);
        if (broker == null)
        {
            return NotFound();
        }
        if (User.GetUserRole() == "Broker")
        {
            updateBrokerDto.Status = broker.Status;
            updateBrokerDto.TeamId = broker.TeamId;
        }
        if (updateBrokerDto.UserName != broker.UserName && await UserNameEmailPhoneNumberChecker.CheckUserName(updateBrokerDto.UserName, unitOfWorkService))
        {
            return BadRequest(new { message = "UserName is already exist" });
        }

        if (updateBrokerDto.Email != broker.Email && await UserNameEmailPhoneNumberChecker.CheckEmail(updateBrokerDto.Email, unitOfWorkService))
        {
           return BadRequest(new { message = "Email is already exist" });
        }

        if (updateBrokerDto.PhoneNumber != broker.PhoneNumber && await UserNameEmailPhoneNumberChecker.CheckPhoneNumber(updateBrokerDto.PhoneNumber, unitOfWorkService))
        {
            return BadRequest(new { message = "PhoneNumber is already exist" });
        }
        mapper.Map(updateBrokerDto, broker);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<BrokerDto>(broker));
    }

    /// <summary>
    /// Delete Broker
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BrokerDto>> DeleteBroker(decimal id)
    {
        Broker? broker = await unitOfWorkService.BrokerService.GetBrokerById(id);
        if (broker == null)
        {
            return NotFound();
        }
        broker = unitOfWorkService.BrokerService.Delete(broker);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<BrokerDto>(broker));
    }

    /// <summary>
    /// Login for broker
    /// </summary>
    [HttpPost("authenticate")]
    public async Task<ActionResult<AuthenticateRespone<BrokerDto>>> Authenticate([FromBody] AuthenticateRequest request)
    {
        if (request.UserName != null && request.Password != null)
        {
            var broker = await unitOfWorkService.BrokerService.GetBrokerByUserName(request.UserName);
            if (broker == null)
            {
                return BadRequest(new { message = "Invalid username or password" });
            }
            if (broker.Status == MyConstant.Broker.Status.Deleted)
            {
                return BadRequest(new { message = "Account is inactive" });
            }

            var hmac = new HMACSHA512(broker.PasswordSalt!);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != broker.PasswordHash![i])
                {
                    return BadRequest(new { message = "Invalid username or password" });
                }
            }

            return Ok(new AuthenticateRespone<BrokerDto>
            {
                Token = TokenHelpers.CreateToken(broker.Id.ToString(), "Broker", configuration),
                User = mapper.Map<BrokerDto>(broker)
            });
        }
        return BadRequest(new { message = "Invalid username or password" });
    }

    /// <summary>
    /// Admin update broker password
    /// </summary>
    //[Authorize(Policy = "RequireAgentOrBrokerRole")]
    [HttpPut("{id}/password")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> ChangePassword(int id, [FromBody] string newPassword)
    {
        var broker = await unitOfWorkService.BrokerService.GetBrokerById(id);
        if (broker == null)
        {
            return NotFound();
        }

        var hmac = new HMACSHA512();
        broker.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword!));
        broker.PasswordSalt = hmac.Key;
        await unitOfWorkService.CommitAsync();
        return Ok();
    }

    /// <summary>
    /// Broker Update their password
    /// </summary>
    //[Authorize(Policy = "RequireAgentOrBrokerRole")]
    [HttpPut("profile/password")]
    [Authorize(Roles = "Broker")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var broker = await unitOfWorkService.BrokerService.GetBrokerById(User.GetUserId());
        if (broker == null)
        {
            return NotFound();
        }

        var hmac = new HMACSHA512(broker.PasswordSalt!);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.OldPassword!));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != broker.PasswordHash![i])
            {
                return BadRequest(new { message = "Invalid old password" });
            }
        }

        hmac = new HMACSHA512();
        broker.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword!));
        broker.PasswordSalt = hmac.Key;
        broker.Status = 2;
        await unitOfWorkService.CommitAsync();
        return Ok();
    }

    /// <summary>
    /// Update broker avatar
    /// </summary>
    //[Authorize(Policy = "RequireBrokerRole")]
    [HttpPut("{id}/avatar")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BrokerDto>> UpdateBrokerAvatar(decimal id, IFormFile file)
    {
        if (file == null)
        {
            return BadRequest(new { message = "No file" });
        }

        var broker = await unitOfWorkService.BrokerService.GetBrokerById(id);

        if (broker == null)
        {
            return NotFound();
        }

        if (broker.AvatarFileId != null)
        {
            await fileService.DeleteFileAsync(broker.AvatarFileId!);
        }
        var imagekitResponse = await fileService.UploadFileAsync(file);
        broker.AvatarFileId = imagekitResponse.FileId;
        broker.AvatarLink = imagekitResponse.URL;
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<BrokerDto>(broker));
    }

    /// <summary>
    /// Broker update their avatar
    /// </summary>
    //[Authorize(Policy = "RequireBrokerRole")]
    [HttpPut("profile/avatar")]
    [Authorize(Roles = "Broker")]
    public async Task<ActionResult<BrokerDto>> UpdateBrokerAvatar(IFormFile file)
    {
        if (file == null)
        {
            return BadRequest(new { message = "No file" });
        }

        var broker = await unitOfWorkService.BrokerService.GetBrokerById(User.GetUserId());

        if (broker == null)
        {
            return NotFound();
        }

        if (broker.AvatarFileId != null)
        {
            await fileService.DeleteFileAsync(broker.AvatarFileId!);
        }
        var imagekitResponse = await fileService.UploadFileAsync(file);
        broker.AvatarFileId = imagekitResponse.FileId;
        broker.AvatarLink = imagekitResponse.URL;
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<BrokerDto>(broker));
    }

    /// <summary>
    /// Broker reset password
    /// </summary>
    [HttpPut("reset-password")]
    public async Task<ActionResult> ResetPassword([FromBody] string email)
    {
        var broker = await unitOfWorkService.BrokerService.GetBrokerByEmail(email);
        if (broker == null || broker.Status == MyConstant.Broker.Status.Deleted)
        {
            return NotFound();
        }

        var newPassword = new PasswordGenerater(
            1, 1, 1, 1, 8
        ).Generate();

        if ((EmailHelper.SendEmailResetPassword(broker.UserName, broker.Name, newPassword, broker.Email)))
        {
            var hmac = new HMACSHA512();
            broker.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
            broker.PasswordSalt = hmac.Key;
            broker.Status = 1;
            await unitOfWorkService.CommitAsync();
            return Ok();
        }
        return Ok(new { message = "Can not send mail" });
    }
}
