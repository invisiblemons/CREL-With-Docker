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
[Route("api/" + MyConstant.apiVersion + "/stores")]
public class StoreController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;

    public StoreController(IUnitOfWorkService unitOfWorkService, IMapper mapper)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get Store by ID
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "Brand,AreaManager,Admin")]
    public async Task<ActionResult<StoreDto>> GetStore(decimal id)
    {
        StoreDto? storeDto = await unitOfWorkService.StoreService.GetStoreDtoById(id);
        if (storeDto == null)
        {
            return NotFound();
        }
        return Ok(storeDto);
    }

    /// <summary>
    /// Search Store
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Brand,AreaManager,Admin")]
    public async Task<ActionResult<PagedList<StoreDto>>> SearchStore([FromQuery] StoreFilter storeFilter)
    {
        PagedList<StoreDto> pagedListStore = await unitOfWorkService.StoreService.GetStoreDtos(storeFilter);
        Response.AddPaginationHeader(pagedListStore);
        return Ok(pagedListStore);
    }

    /// <summary>
    /// Create Store
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Brand")]
    public async Task<ActionResult<StoreDto>> CreateStore([FromBody] CreateStoreDto createStoreDto)
    {
        Store store = mapper.Map<Store>(createStoreDto);
        await unitOfWorkService.StoreService.Add(store);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<StoreDto>(store));
    }

    /// <summary>
    /// Update Store
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Brand")]
    public async Task<ActionResult<StoreDto>> UpdateStore([FromBody] UpdateStoreDto updateStoreDto)
    {
        Store? store = await unitOfWorkService.StoreService.GetStoreById(updateStoreDto.Id!.Value);
        if (store == null)
        {
            return NotFound();
        }
        mapper.Map(updateStoreDto, store);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<StoreDto>(store));
    }

    /// <summary>
    /// Delete Store
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Brand")]
    public async Task<ActionResult<StoreDto>> DeleteStore(decimal id)
    {
        Store? store = await unitOfWorkService.StoreService.GetStoreById(id);
        if (store == null)
        {
            return NotFound();
        }
        store = unitOfWorkService.StoreService.Delete(store);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<StoreDto>(store));
    }
}
