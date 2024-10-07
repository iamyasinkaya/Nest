using Microsoft.AspNetCore.Mvc;
using Nest.Application;

namespace Nest.HotelAPI;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelsController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetailsAsync(Guid id)
    {
        var hotel = await _hotelService.GetHotelWithDetailsAsync(id);
        return hotel != null ? Ok(hotel) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateHoteldDto hotel)
    {
        var newHotel = await _hotelService.CreateHotelAsync(hotel);
        return Created("GetDetailsAsync", newHotel.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateHotelDto hotel)
    {
        if (id != hotel.Id) return BadRequest();
        await _hotelService.UpdateHotelAsync(hotel);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _hotelService.DeleteHotelAsync(id);
        return NoContent();
    }
}
