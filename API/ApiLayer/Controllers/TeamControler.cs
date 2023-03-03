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
[Route("api/" + MyConstant.apiVersion + "/teams")]
public class TeamController : ControllerBase
{
    private readonly IUnitOfWorkService unitOfWorkService;
    private readonly IMapper mapper;

    public TeamController(IUnitOfWorkService unitOfWorkService, IMapper mapper)
    {
        this.unitOfWorkService = unitOfWorkService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get Team by ID
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "Broker,AreaManager,Admin")] 
    public async Task<ActionResult<TeamDto>> GetTeam(decimal id)
    {
        TeamDto? teamDto = await unitOfWorkService.TeamService.GetTeamDtoById(id);
        if (teamDto == null)
        {
            return NotFound();
        }
        return Ok(teamDto);
    }

    /// <summary>
    /// Search Team
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "AreaManager,Admin")] 
    public async Task<ActionResult<PagedList<TeamDto>>> SearchTeam([FromQuery] TeamFilter teamFilter)
    {
        PagedList<TeamDto> pagedListTeam = await unitOfWorkService.TeamService.GetTeamDtos(teamFilter);
        Response.AddPaginationHeader(pagedListTeam);
        return Ok(pagedListTeam);
    }

    /// <summary>
    /// Create Team
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")] 
    public async Task<ActionResult<TeamDto>> CreateTeam([FromBody] CreateTeamDto createTeamDto)
    {
        Team team = mapper.Map<Team>(createTeamDto);
        await unitOfWorkService.TeamService.Add(team);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<TeamDto>(team));
    }

    /// <summary>
    /// Update Team
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Admin")] 
    public async Task<ActionResult<TeamDto>> UpdateTeam([FromBody] UpdateTeamDto updateTeamDto)
    {
        Team? team = await unitOfWorkService.TeamService.GetTeamById(updateTeamDto.Id!.Value);
        if (team == null)
        {
            return NotFound();
        }
        mapper.Map(updateTeamDto, team);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<TeamDto>(team));
    }

    /// <summary>
    /// Delete Team
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] 
    public async Task<ActionResult<TeamDto>> DeleteTeam(decimal id)
    {
        Team? team = await unitOfWorkService.TeamService.GetTeamById(id);
        if (team == null)
        {
            return NotFound();
        }
        team = unitOfWorkService.TeamService.Delete(team);
        await unitOfWorkService.CommitAsync();
        return Ok(mapper.Map<TeamDto>(team));
    }

    /// <summary>
    /// Add Brokers for Team
    /// </summary>
    [HttpPost("{id}/brokers")]
    [Authorize(Roles = "Admin")] 
    public async Task<ActionResult<TeamDto>> AddBrokersToTeam(decimal id, [FromBody] ICollection<decimal> ids)
    {
        var team = await unitOfWorkService.TeamService.GetTeamById(id);

        if (team == null)
        {
            return NotFound();
        }

        if (ids.Count == 0)
        {
            return BadRequest(new { message = "No ID" });
        }

        foreach (var brokerId in ids)
        {
            var broker = await unitOfWorkService.BrokerService.GetBrokerById(brokerId);
            if (broker == null)
            {
                return NotFound();
            }
            team.Brokers.Add(broker);
        }

        await unitOfWorkService.CommitAsync();

        foreach (var brokerId in ids)
        {
             await NotificationHelper.SendNotifications(
                new NotificationDto{
                    BrokerId = brokerId,
                    TeamName = team.Name,
                    SendDateTime = DateTime.Now,
                    Type = MyConstant.Notification.Type.TeamAdd,
                }
            );
        }
        return Ok(await unitOfWorkService.TeamService.GetTeamDtoById(id));
    }

    /// <summary>
    /// Remove Brokers from Team
    /// </summary>
    [HttpDelete("{id}/brokers")]
    [Authorize(Roles = "Admin")] 
    public async Task<ActionResult<TeamDto>> RemoveBrokersFromTeam(decimal id, [FromBody] ICollection<decimal> ids)
    {
        var team = await unitOfWorkService.TeamService.GetTeamById(id);

        if (team == null)
        {
            return NotFound();
        }

        if (ids.Count == 0)
        {
            return BadRequest(new { message = "No ID" });
        }

        foreach (var brokerId in ids)
        {
            var broker = await unitOfWorkService.BrokerService.GetBrokerById(brokerId);
            if (broker == null)
            {
                return NotFound();
            }
            team.Brokers.Remove(broker);
        }

        await unitOfWorkService.CommitAsync();

        foreach (var brokerId in ids)
        {
             await NotificationHelper.SendNotifications(
                new NotificationDto{
                    BrokerId = brokerId,
                    TeamName = team.Name,
                    SendDateTime = DateTime.Now,
                    Type = MyConstant.Notification.Type.TeamRemove,
                }
            );
        }
        return Ok(await unitOfWorkService.TeamService.GetTeamDtoById(id));
    }


    /// <summary>
    /// add Area Manager to Team
    /// </summary>
    [HttpPost("{teamId}/area-managers")]
    [Authorize(Roles = "Admin")] 
    public async Task<ActionResult<List<AreaManagerTeamDto>>> AddAreaManagerToTeam(decimal teamId, [FromBody] ICollection<decimal> areaManagerIds)
    {
        var response = new List<AreaManagerTeamDto>();
        foreach (var areaManagerId in areaManagerIds)
        {
            AreaManagerTeam? areaManagerTeam = await unitOfWorkService.AreaManagerTeamService.GetAreaManagerTeamyByAreaManagerIdTeamId(areaManagerId, teamId);
            if (areaManagerTeam == null)
            {
                areaManagerTeam = new AreaManagerTeam
                {
                    AreaManagerId = areaManagerId,
                    TeamId = teamId,
                    StartDate = DateTime.Now
                };
                await unitOfWorkService.AreaManagerTeamService.Add(areaManagerTeam);
            }
            response.Add(mapper.Map<AreaManagerTeamDto>(areaManagerTeam));
        }
        await unitOfWorkService.CommitAsync();
        return Ok(response);
    }

    /// <summary>
    /// remove Area Manager from Team
    /// </summary>
    [HttpDelete("{teamId}/area-managers")]
    [Authorize(Roles = "Admin")] 
    public async Task<ActionResult<List<AreaManagerTeamDto>>> RemoveAreaManagerFromTeam(decimal teamId, [FromBody] ICollection<decimal> areaManagerIds)
    {
        var response = new List<AreaManagerTeamDto>();
        foreach (var areaManagerId in areaManagerIds)
        {
            AreaManagerTeam? areaManagerTeam = await unitOfWorkService.AreaManagerTeamService.GetAreaManagerTeamyByAreaManagerIdTeamId(areaManagerId, teamId);
            if (areaManagerTeam != null)
            {
                areaManagerTeam.EndDate = DateTime.Now;
                response.Add(mapper.Map<AreaManagerTeamDto>(areaManagerTeam));
            }
        }
        await unitOfWorkService.CommitAsync();
        return Ok(response);
    }
}
