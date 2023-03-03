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
[Route("api/" + MyConstant.apiVersion + "/projects")]
public class ProjectController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;

    public ProjectController(IUnitOfWorkService unitOfWorkService, IMapper mapper)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get Project by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(decimal id)
    {
        ProjectDto? projectDto = await unitOfWorkService.ProjectService.GetProjectDtoById(id);
        if (projectDto == null)
        {
            return NotFound();
        }
        return Ok(projectDto);
    }

    /// <summary>
    /// Search Project
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedList<ProjectDto>>> SearchProject([FromQuery] ProjectFilter projectFilter)
    {
        PagedList<ProjectDto> pagedListProject = await unitOfWorkService.ProjectService.GetProjectDtos(projectFilter);
        Response.AddPaginationHeader(pagedListProject);
        return Ok(pagedListProject);
    }

    /// <summary>
    /// Create Project
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectDto createProjectDto)
    {
        Project project = mapper.Map<Project>(createProjectDto);
        await unitOfWorkService.ProjectService.Add(project);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<ProjectDto>(project));
    }

    /// <summary>
    /// Update Project
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<ProjectDto>> UpdateProject([FromBody] UpdateProjectDto updateProjectDto)
    {
        Project? project = await unitOfWorkService.ProjectService.GetProjectById(updateProjectDto.Id!.Value);
        if (project == null)
        {
            return NotFound();
        }
        mapper.Map(updateProjectDto, project);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<ProjectDto>(project));
    }

    /// <summary>
    /// Delete Project
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "AreaManager,Admin")]
    public async Task<ActionResult<ProjectDto>> DeleteProject(decimal id)
    {
        Project? project = await unitOfWorkService.ProjectService.GetProjectById(id);
        if (project == null)
        {
            return NotFound();
        }
        project = unitOfWorkService.ProjectService.Delete(project);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<ProjectDto>(project));
    }
}
