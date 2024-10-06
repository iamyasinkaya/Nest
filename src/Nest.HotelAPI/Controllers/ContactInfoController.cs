using Microsoft.AspNetCore.Mvc;
using Nest.Application;

namespace Nest.HotelAPI;

[Route("api/[controller]")]
[ApiController]
public class ContactInfoController : ControllerBase
{
    private readonly IContactInfoService _contactInfoService;

    public ContactInfoController(IContactInfoService contactInfoService)
    {
        _contactInfoService = contactInfoService;
    }


    [HttpGet("{id}")]

    public async Task<IActionResult> GetContactInfoWithDetails(Guid id)
    {
        var contactInfo = await _contactInfoService.GetContactInfoWithDetailsAsync(id);
        return contactInfo != null ? Ok(contactInfo) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateHotelAsync([FromBody] CreateContactInfoDto contactInfo)
    {
        try
        {
            var newContactInfo = await _contactInfoService.CreateAsync(contactInfo);
            return Created(nameof(GetContactInfoWithDetails), newContactInfo.Id);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        await _contactInfoService.RemoveContactInfoAsync(id);
        return NoContent();
    }
}
