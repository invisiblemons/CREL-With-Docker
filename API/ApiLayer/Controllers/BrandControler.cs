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
[Route("api/" + MyConstant.apiVersion + "/brands")]
public class BrandController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;
    private readonly IFileService fileService;
    private readonly IConfiguration configuration;

    public BrandController(IUnitOfWorkService unitOfWorkService, IMapper mapper, IFileService fileService, IConfiguration configuration)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
        this.fileService = fileService;
        this.configuration = configuration;
    }

    /// <summary>
    /// Get Brand by ID
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<BrandDto>> GetBrand(decimal id)
    {
        BrandDto? brandDto = await unitOfWorkService.BrandService.GetBrandDtoById(id);
        if (brandDto == null)
        {
            return NotFound();
        }
        brandDto.IsNeedUpdatePassword = await FireStoreHelper.IsContainAsync(brandDto.Email ?? "");
        return Ok(brandDto);
    }

    /// <summary>
    /// Search Brand
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<BrandDto>>> SearchBrand([FromQuery] BrandFilter brandFilter)
    {
        PagedList<BrandDto> pagedListBrand = await unitOfWorkService.BrandService.GetBrandDtos(brandFilter);
        Response.AddPaginationHeader(pagedListBrand);
        return Ok(pagedListBrand);
    }

    /// <summary>
    /// Create Brand
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BrandDto>> CreateBrand([FromBody] CreateBrandDto createBrandDto)
    {
        Brand brand = mapper.Map<Brand>(createBrandDto);
        if (await UserNameEmailPhoneNumberChecker.CheckUserName(createBrandDto.UserName, unitOfWorkService))
        {
            return BadRequest(new { message = "UserName is already exist" });
        }

        if (await UserNameEmailPhoneNumberChecker.CheckEmail(createBrandDto.Email, unitOfWorkService))
        {
            return BadRequest(new { message = "Email is already exist" });
        }

        if (await UserNameEmailPhoneNumberChecker.CheckPhoneNumber(createBrandDto.PhoneNumber, unitOfWorkService))
        {
            return BadRequest(new { message = "PhoneNumber is already exist" });
        }

        if (createBrandDto.Password != null)
        {
            var hmac = new HMACSHA512();
            brand.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(createBrandDto.Password!));
            brand.PasswordSalt = hmac.Key;
            brand.UserName = createBrandDto.UserName;
        }

        await unitOfWorkService.BrandService.Add(brand);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<BrandDto>(brand));
    }

    /// <summary>
    /// Update Brand
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BrandDto>> UpdateBrand([FromBody] UpdateBrandDto updateBrandDto)
    {
        Brand? brand = await unitOfWorkService.BrandService.GetBrandById(updateBrandDto.Id!.Value);
        if (brand == null)
        {
            return NotFound();
        }
        if (updateBrandDto.UserName != brand.UserName && await UserNameEmailPhoneNumberChecker.CheckUserName(updateBrandDto.UserName, unitOfWorkService))
        {
            return BadRequest(new { message = "UserName is already exist" });
        }

        if (updateBrandDto.Email != brand.Email && await UserNameEmailPhoneNumberChecker.CheckEmail(updateBrandDto.Email, unitOfWorkService))
        {
            return BadRequest(new { message = "Email is already exist" });
        }

        if (updateBrandDto.PhoneNumber != brand.PhoneNumber && await UserNameEmailPhoneNumberChecker.CheckPhoneNumber(updateBrandDto.PhoneNumber, unitOfWorkService))
        {
            return BadRequest(new { message = "PhoneNumber is already exist" });
        }
        if (updateBrandDto.UserName != null && updateBrandDto.Password != null)
        {
            var hmac = new HMACSHA512();
            brand.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(updateBrandDto.Password!));
            brand.PasswordSalt = hmac.Key;
            brand.UserName = updateBrandDto.UserName;
        }

        bool isVerifiedAccount = brand.Status == 1 && updateBrandDto.Status == 2;
        bool isNotVerifiedAccount = brand.Status == 1 && updateBrandDto.Status == 3;

        mapper.Map(updateBrandDto, brand);
        await unitOfWorkService.CommitAsync();
        if (isVerifiedAccount)
        {
            EmailHelper.SendEmailVerifiedAccount(brand.UserName ?? "", brand.Name ?? "", brand.Email ?? "");
        }
        if (isNotVerifiedAccount)
        {
            EmailHelper.SendEmailNotVerifiedAccount(brand.UserName ?? "", brand.Name ?? "", brand.Email ?? "", brand.RejectMessage ?? "");
        }
        return Ok(mapper.Map<BrandDto>(brand));
    }

    /// <summary>
    /// Delete Brand
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BrandDto>> DeleteBrand(decimal id)
    {
        Brand? brand = await unitOfWorkService.BrandService.GetBrandById(id);
        if (brand == null)
        {
            return NotFound();
        }
        brand = unitOfWorkService.BrandService.Delete(brand);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<BrandDto>(brand));
    }

    /// <summary>
    /// Login for brand
    /// </summary>
    [HttpPost("authenticate")]
    public async Task<ActionResult<AuthenticateRespone<BrandDto>>> Authenticate([FromBody] BrandAuthenticateRequest request)
    {
        if (request.Token != null)
        {
            string firebaseUid = await FirebaseHelpers.ValidateTokenAsync(request.Token);
            var brand = await unitOfWorkService.BrandService.GetBrandByFirebaseId(firebaseUid);
            if (brand == null)
            {
                brand = new Brand
                {
                    Status = 1,
                    FirebaseUid = firebaseUid,
                };
                await unitOfWorkService.BrandService.Add(brand);
                await unitOfWorkService.CommitAsync();
                brand = await unitOfWorkService.BrandService.GetBrandByFirebaseId(firebaseUid);
            }

            if (brand!.Status == MyConstant.Brand.Status.Deleted)
            {
                return BadRequest(new { message = "Account is inactive" });
            }
            return Ok(new AuthenticateRespone<BrandDto>
            {
                Token = TokenHelpers.CreateToken(brand.Id!.ToString(), "Brand", configuration),
                User = mapper.Map<BrandDto>(brand)
            });
        }
        if (request.UserName != null && request.Password != null)
        {
            var brand = await unitOfWorkService.BrandService.GetBrandByUserName(request.UserName);
            if (brand == null)
            {
                return BadRequest(new { message = "Invalid username or password" });
            }
            if (brand.Status == MyConstant.Brand.Status.Deleted)
            {
                return BadRequest(new { message = "Account is inactive" });
            }

            var hmac = new HMACSHA512(brand.PasswordSalt!);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != brand.PasswordHash![i])
                {
                    return BadRequest(new { message = "Invalid username or password" });
                }
            }

            return Ok(new AuthenticateRespone<BrandDto>
            {
                Token = TokenHelpers.CreateToken(brand.Id!.ToString(), "Brand", configuration),
                User = mapper.Map<BrandDto>(brand)
            });
        }
        return BadRequest(new { message = "Invalid username or password" });
    }

    /// <summary>
    /// Admin update brand password
    /// </summary>
    //[Authorize(Policy = "RequireAgentOrBrandRole")]
    [HttpPut("{id}/password")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> ChangePassword(int id, [FromBody] string newPassword)
    {
        var brand = await unitOfWorkService.BrandService.GetBrandById(id);
        if (brand == null)
        {
            return NotFound();
        }

        var hmac = new HMACSHA512();
        brand.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword!));
        brand.PasswordSalt = hmac.Key;
        await unitOfWorkService.CommitAsync();
        return Ok();
    }

    /// <summary>
    /// Brand Update their password
    /// </summary>
    //[Authorize(Policy = "RequireAgentOrBrandRole")]
    [HttpPut("profile/password")]
    [Authorize(Roles = "Brand")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var brand = await unitOfWorkService.BrandService.GetBrandById(User.GetUserId());
        if (brand == null)
        {
            return NotFound();
        }

        if (brand.PasswordSalt != null && brand.PasswordHash != null)
        {
            var thmac = new HMACSHA512(brand.PasswordSalt!);
            var computedHash = thmac.ComputeHash(Encoding.UTF8.GetBytes(request.OldPassword!));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != brand.PasswordHash![i])
                {
                    return BadRequest(new { message = "Invalid old password" });
                }
            }
        }

        var hmac = new HMACSHA512();
        brand.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword!));
        brand.PasswordSalt = hmac.Key;
        await FireStoreHelper.RemoveEmailAsync(brand.Email ?? "");
        await unitOfWorkService.CommitAsync();
        return Ok();
    }

    /// <summary>
    /// Update brand avatar
    /// </summary>
    //[Authorize(Policy = "RequireBrandRole")]
    [HttpPut("{id}/avatar")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<BrandDto>> UpdateBrandAvatar(decimal id, IFormFile file)
    {
        if (file == null)
        {
            return BadRequest(new { message = "No file" });
        }

        var brand = await unitOfWorkService.BrandService.GetBrandById(id);

        if (brand == null)
        {
            return NotFound();
        }

        if (brand.AvatarFileId != null)
        {
            await fileService.DeleteFileAsync(brand.AvatarFileId);
        }
        var imagekitResponse = await fileService.UploadFileAsync(file);
        brand.AvatarFileId = imagekitResponse.FileId;
        brand.AvatarLink = imagekitResponse.URL;
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<BrandDto>(brand));
    }

    /// <summary>
    /// Update brand profile
    /// </summary>
    //[Authorize(Policy = "RequireBrandRole")]
    [HttpPut("profile")]
    [Authorize(Roles = "Brand")]
    public async Task<ActionResult<BrandDto>> UpdateBrandProfile(BrandProfileDto brandProfile)
    {
        var brand = await unitOfWorkService.BrandService.GetBrandById(User.GetUserId());

        if (brand == null)
        {
            return NotFound();
        }

        if (brandProfile.UserName != brand.UserName && await UserNameEmailPhoneNumberChecker.CheckUserName(brandProfile.UserName, unitOfWorkService))
        {
            return BadRequest(new { message = "UserName is already exist" });
        }

        if (brandProfile.Email != brand.Email && await UserNameEmailPhoneNumberChecker.CheckEmail(brandProfile.Email, unitOfWorkService))
        {
            return BadRequest(new { message = "Email is already exist" });
        }

        if (brandProfile.PhoneNumber != brand.PhoneNumber && await UserNameEmailPhoneNumberChecker.CheckPhoneNumber(brandProfile.PhoneNumber, unitOfWorkService))
        {
            return BadRequest(new { message = "PhoneNumber is already exist" });
        }

        brand.UserName = brandProfile.UserName;
        brand.Name = brandProfile.Name;
        brand.Email = brandProfile.Email;
        brand.PhoneNumber = brandProfile.PhoneNumber;
        brand.Description = brandProfile.Description;
        brand.IndustryId = brandProfile.IndustryId;
        brand.RegistrationNumber = brandProfile.RegistrationNumber;
        if (brandProfile.AvatarLink != null)
        {
            brand.AvatarLink = brandProfile.AvatarLink;
        }

        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<BrandDto>(brand));
    }

    /// <summary>
    /// Brand update their avatar
    /// </summary>
    //[Authorize(Policy = "RequireBrandRole")]
    [HttpPut("profile/avatar")]
    [Authorize(Roles = "Brand")]
    public async Task<ActionResult<BrandDto>> UpdateBrandAvatar(IFormFile file)
    {
        if (file == null)
        {
            return BadRequest(new { message = "No file" });
        }

        var brand = await unitOfWorkService.BrandService.GetBrandById(User.GetUserId());

        if (brand == null)
        {
            return NotFound();
        }

        if (brand.AvatarFileId != null)
        {
            await fileService.DeleteFileAsync(brand.AvatarFileId);
        }
        var imagekitResponse = await fileService.UploadFileAsync(file);
        brand.AvatarFileId = imagekitResponse.FileId;
        brand.AvatarLink = imagekitResponse.URL;
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<BrandDto>(brand));
    }

    /// <summary>
    /// Brand reset password
    /// </summary>
    [HttpPut("reset-password")]
    public async Task<ActionResult> ResetPassword([FromBody] string email)
    {
        var brand = await unitOfWorkService.BrandService.GetBrandByEmail(email);
        if (brand == null || brand.Status == MyConstant.Brand.Status.Deleted)
        {
            return NotFound();
        }

        if (brand.UserName == null || brand.Name == null || brand.Email == null)
        {
            return BadRequest(new { message = "UserName = null OR Name = null OR Email = null" });
        }

        var newPassword = new PasswordGenerater(
            1, 1, 1, 1, 8
        ).Generate();

        if ((EmailHelper.SendEmailResetPassword(brand.UserName, brand.Name, newPassword, brand.Email)))
        {
            var hmac = new HMACSHA512();
            brand.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
            brand.PasswordSalt = hmac.Key;
            await FireStoreHelper.AddEmailAsync(brand.Email ?? "");
            await unitOfWorkService.CommitAsync();
            return Ok();
        }
        throw new Exception("Can not send mail");
    }

    /// <summary>
    /// add recommend properties for brand
    /// </summary>
    [HttpPost("{brandId}/recommend-properties")]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<List<PropertyBrandDto>>> SuggetPropertyToBrand(decimal brandId, [FromBody] ICollection<decimal> propertyIds)
    {
        var response = new List<PropertyBrandDto>();
        var properties = new List<PropertyDto>();
        var brand = await unitOfWorkService.BrandService.GetBrandDtoById(brandId);
        if (brand == null)
        {
            return NotFound();
        }
        foreach (var propertyId in propertyIds)
        {
            PropertyBrand? propertyBrand = await unitOfWorkService.PropertyBrandService.GetPropertyBrandById(propertyId, brandId);
            if (propertyBrand == null)
            {
                propertyBrand = new PropertyBrand
                {
                    PropertyId = propertyId,
                    BrandId = brandId,
                    Type = 1
                };
                await unitOfWorkService.PropertyBrandService.Add(propertyBrand);
            }
            else
            {
                propertyBrand.Type = BitHelper.SetBit(propertyBrand.Type, 0, true);
            }

            var property = await unitOfWorkService.PropertyService.GetPropertyDtoById(propertyId);
            if (property == null)
            {
                return NotFound();
            }
            properties.Add(property);
            response.Add(mapper.Map<PropertyBrandDto>(propertyBrand));
        }
        await unitOfWorkService.CommitAsync();
        EmailHelper.SendEmailSuggestProperty(brand.Name ?? "", brand.Email ?? "", properties);
        return Ok(response);
    }

    /// <summary>
    /// add recommend properties for brand
    /// </summary>
    [HttpDelete("{brandId}/recommend-properties")]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<List<PropertyBrandDto>>> UnSuggetPropertyToBrand(decimal brandId, [FromBody] ICollection<decimal> propertyIds)
    {
        var response = new List<PropertyBrandDto>();
        foreach (var propertyId in propertyIds)
        {
            PropertyBrand? propertyBrand = await unitOfWorkService.PropertyBrandService.GetPropertyBrandById(propertyId, brandId);
            if (propertyBrand != null)
            {
                propertyBrand.Type = BitHelper.SetBit(propertyBrand.Type, 0, false);
                response.Add(mapper.Map<PropertyBrandDto>(propertyBrand));
            }
        }
        await unitOfWorkService.CommitAsync();
        return Ok(response);
    }

    /// <summary>
    /// Brand Mark Property
    /// </summary>
    [HttpPost("marked-property")]
    [Authorize(Roles = "Brand")]
    public async Task<ActionResult<List<PropertyBrandDto>>> BrandMarkProperty([FromBody] ICollection<decimal> propertyIds)
    {
        var response = new List<PropertyBrandDto>();
        foreach (var propertyId in propertyIds)
        {
            PropertyBrand? propertyBrand = await unitOfWorkService.PropertyBrandService.GetPropertyBrandById(propertyId, User.GetUserId());
            if (propertyBrand == null)
            {
                propertyBrand = new PropertyBrand
                {
                    PropertyId = propertyId,
                    BrandId = User.GetUserId(),
                    Type = 2
                };
                await unitOfWorkService.PropertyBrandService.Add(propertyBrand);
            }
            else
            {
                propertyBrand.Type = BitHelper.SetBit(propertyBrand.Type, 1, true);
            }
            response.Add(mapper.Map<PropertyBrandDto>(propertyBrand));
        }
        await unitOfWorkService.CommitAsync();
        return Ok(response);
    }

    /// <summary>
    /// Brand Unmark Property
    /// </summary>
    [HttpDelete("marked-property")]
    [Authorize(Roles = "Brand")]
    public async Task<ActionResult<List<PropertyBrandDto>>> BrandUnMarkProperty([FromBody] ICollection<decimal> propertyIds)
    {
        var response = new List<PropertyBrandDto>();
        foreach (var propertyId in propertyIds)
        {
            PropertyBrand? propertyBrand = await unitOfWorkService.PropertyBrandService.GetPropertyBrandById(propertyId, User.GetUserId());
            if (propertyBrand != null)
            {
                propertyBrand.Type = BitHelper.SetBit(propertyBrand.Type, 1, false);
                response.Add(mapper.Map<PropertyBrandDto>(propertyBrand));
            }
        }
        await unitOfWorkService.CommitAsync();
        return Ok(response);
    }
}
