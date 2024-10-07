using Microsoft.AspNetCore.Mvc;
using Nest.Application;

namespace Nest.HotelAPI;

[Route("api/[controller]")]
[ApiController]
public class ManagersController : ControllerBase
{
    private readonly IManagerService _managerService;

    public ManagersController(IManagerService managerService)
    {
        _managerService = managerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateManagerDto manager)
    {
        try
        {
            var newManager = await _managerService.CreateManagerAsync(manager);
            return Ok(newManager.Id);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
