using AutoMapper;
using CREL_BE.Extensions;
using CREL_BE.Helpers;
using CREL_BE.Entities;
using CREL_BE.Dtos;
using CREL_BE.Filter;
using Microsoft.AspNetCore.Mvc;
using CREL_BE.Services;
using Microsoft.AspNetCore.Authorization;

namespace CREL_BE.Controllers;
[ApiController]
[Route("api/" + MyConstant.apiVersion + "/contracts")]
public class ContractController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;
    private readonly IFileService fileService;

    public ContractController(IUnitOfWorkService unitOfWorkService, IMapper mapper, IFileService fileService)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
        this.fileService = fileService;
    }

    /// <summary>
    /// Get Contract by ID
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ContractDto>> GetContract(decimal id)
    {
        ContractDto? contractDto = await unitOfWorkService.ContractService.GetContractDtoById(id);
        if (contractDto == null)
        {
            return NotFound();
        }
        return Ok(contractDto);
    }

    /// <summary>
    /// Search Contract
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PagedList<ContractDto>>> SearchContract([FromQuery] ContractFilter contractFilter)
    {
        PagedList<ContractDto> pagedListContract = await unitOfWorkService.ContractService.GetContractDtos(contractFilter);
        Response.AddPaginationHeader(pagedListContract);
        return Ok(pagedListContract);
    }

    /// <summary>
    /// Create Contract
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Broker")]
    public async Task<ActionResult<ContractDto>> CreateContract([FromBody] CreateContractDto createContractDto)
    {
        Contract contract = mapper.Map<Contract>(createContractDto);
        if (createContractDto.EndDate == null)
        {
            contract.EndDate = contract.StartDate.AddYears(Convert.ToInt32(Math.Floor(contract.LeaseLength ?? 0))).AddMonths(Convert.ToInt32(Math.Floor(((contract.LeaseLength ?? 0) * 12) % 12)));
        }
        await unitOfWorkService.ContractService.Add(contract);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<ContractDto>(contract));
    }

    /// <summary>
    /// Update Contract
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Broker")]
    public async Task<ActionResult<ContractDto>> UpdateContract([FromBody] UpdateContractDto updateContractDto)
    {
        Contract? contract = await unitOfWorkService.ContractService.GetContractById(updateContractDto.Id!.Value);
        if (contract == null)
        {
            return NotFound();
        }
        if (contract.ContractTerms != null)
        {
            foreach (ContractTerm contractTerms in contract.ContractTerms)
            {
                if (contract.ContractTerms != null)
                {
                    foreach (ContractTerm contractTerms2 in contractTerms.ContractTerms)
                    {

                        unitOfWorkService.ContractTermService.Delete(contractTerms2);
                    }
                }
                unitOfWorkService.ContractTermService.Delete(contractTerms);
            }
        }
        
        var isSigned = contract.Status == 1 && updateContractDto.Status == 2;
        mapper.Map(updateContractDto, contract);
        if (updateContractDto.EndDate == null)
        {
            contract.EndDate = contract.StartDate.AddYears(Convert.ToInt32(Math.Floor(contract.LeaseLength ?? 0))).AddMonths(Convert.ToInt32(Math.Floor(((contract.LeaseLength ?? 0) * 12) % 12)));
        }
        await unitOfWorkService.CommitAsync();
        if (isSigned){
            var brandDto = await unitOfWorkService.BrandService.GetBrandDtoById(contract.BrandId);
            if (brandDto != null)
            {
                EmailHelper.SendEmailContractSigned(
                    brandDto.Name ?? "",
                    brandDto.Email ?? "",
                    $"https://crel.site/hop-dong/danh-sach/chi-tiet?id={contract.Id}"
                );
            }
        }
        return Ok(mapper.Map<ContractDto>(contract));
    }

    /// <summary>
    /// Delete Contract
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Broker")]
    public async Task<ActionResult<ContractDto>> DeleteContract(decimal id)
    {
        Contract? contract = await unitOfWorkService.ContractService.GetContractById(id);
        if (contract == null)
        {
            return NotFound();
        }
        contract = unitOfWorkService.ContractService.Delete(contract);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<ContractDto>(contract));
    }

    /// <summary>
    /// Add Media for Contract
    /// </summary>
    [HttpPost("{id}/media")]
    [Authorize(Roles = "Broker")]
    public async Task<ActionResult<ContractDto>> AddMediaToContract(decimal id, IFormFileCollection files)
    {
        var contract = await unitOfWorkService.ContractService.GetContractById(id);

        if (contract == null)
        {
            return NotFound();
        }

        if (files.Count == 0)
        {
            return BadRequest(new { message = "No file" });
        }

        foreach (var file in files)
        {
            var uploadFile = await fileService.UploadFileAsync(file);
            contract.Media.Add(new Media
            {
                FileId = uploadFile.FileId,
                Link = uploadFile.URL,
                Type = 1
            });
        }

        await unitOfWorkService.CommitAsync();
        return Ok(await unitOfWorkService.ContractService.GetContractDtoById(id));
    }

    /// <summary>
    /// Get Contract word file by ID
    /// </summary>
    [HttpGet("{id}/word-file")]
    [Authorize]
    public async Task<IActionResult> ExportContractToWord(decimal id)
    {
        ContractDto? contractDto = await unitOfWorkService.ContractService.GetContractDtoById(id);
        if (contractDto == null)
        {
            return NotFound();
        }
        ContractInformationForExport contractInformationForExport = mapper.Map<ContractInformationForExport>(contractDto);
        contractInformationForExport.SignAddress = "Hồ Chí Minh";
        contractInformationForExport.Lessor = " ";
        contractInformationForExport.LessorName = contractDto.Owner == null ? null : contractDto.Owner.Name;
        contractInformationForExport.LessorPhoneNumber = contractDto.Owner == null ? null : contractDto.Owner.PhoneNumber;
        contractInformationForExport.Address = contractDto.getAddress();
        contractInformationForExport.Renter = contractDto.Brand == null ? null : contractDto.Brand.Name;
        contractInformationForExport.RegistrationNumber = contractDto.Brand == null ? null : contractDto.Brand.RegistrationNumber;
        contractInformationForExport.Area = contractDto.Property == null ? null : contractDto.Property.Area;
        contractInformationForExport.PriceInText = contractDto.Price == null ? null : contractDto.Price.Value.ToText();

        return File(System.IO.File.OpenRead(await ExportHelper.ExportContractToWordAsync(contractInformationForExport))
        , "application/octet-stream", contractInformationForExport.Renter + "_" + contractInformationForExport.StartDate!.Value.ToShortDateString().Replace("/", "-") + "_" + (new Random()).Next() + ".doc");
    }

    /// <summary>
    /// Get Contract PDF file by ID
    /// </summary>
    [HttpGet("{id}/pdf-file")]
    [Authorize]
    public async Task<IActionResult> ExportContractToPDF(decimal id)
    {
        ContractDto? contractDto = await unitOfWorkService.ContractService.GetContractDtoById(id);
        if (contractDto == null)
        {
            return NotFound();
        }
        ContractInformationForExport contractInformationForExport = mapper.Map<ContractInformationForExport>(contractDto);
        contractInformationForExport.SignAddress = "Hồ Chí Minh";
        contractInformationForExport.Lessor = " ";
        contractInformationForExport.LessorName = contractDto.Owner == null ? null : contractDto.Owner.Name;
        contractInformationForExport.LessorPhoneNumber = contractDto.Owner == null ? null : contractDto.Owner.PhoneNumber;
        contractInformationForExport.Address = contractDto.getAddress();
        contractInformationForExport.Renter = contractDto.Brand == null ? null : contractDto.Brand.Name;
        contractInformationForExport.RegistrationNumber = contractDto.Brand == null ? null : contractDto.Brand.RegistrationNumber;
        contractInformationForExport.Area = contractDto.Property == null ? null : contractDto.Property.Area;
        contractInformationForExport.PriceInText = contractDto.Price == null ? null : contractDto.Price.Value.ToText();

        return File(System.IO.File.OpenRead(await ExportHelper.ExportContractToPdfAsync(contractInformationForExport))
        , "application/octet-stream", contractInformationForExport.Renter + "_" + contractInformationForExport.StartDate!.Value.ToShortDateString().Replace("/", "-") + "_" + (new Random()).Next() + ".pdf");
    }
}
