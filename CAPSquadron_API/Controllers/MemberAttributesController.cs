﻿using CAPSquadron_API.Models;
using CAPSquadron_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAPSquadron_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemberAttributesController(IMemberAttributesService memberAttributesService) : ControllerBase
{

    // GET: api/MemberAttributes
    [HttpGet(Name = nameof(GetMemberAttributes))]
    [ProducesResponseType<IEnumerable<MemberAttributesDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MemberAttributesDto>>> GetMemberAttributes(CancellationToken cancellationToken)
    {
        var result = await memberAttributesService.GetMemberAttributesAsync(cancellationToken);
        return Ok(result);
    }

    // GET: api/MemberAttributes/{capid}
    [HttpGet("{capid}", Name = nameof(GetMemberAttributesByCapid))]
    [ProducesResponseType<MemberAttributesDto>(StatusCodes.Status200OK)]
    public async Task<ActionResult<MemberAttributesDto>> GetMemberAttributesByCapid(int capid, CancellationToken cancellationToken)
    {
        var result = await memberAttributesService.GetMemberAttributesByCapidAsync(capid, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
