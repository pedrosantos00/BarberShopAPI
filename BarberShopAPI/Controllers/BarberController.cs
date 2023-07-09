using BarberShopAPI.BusinessLogic;
using BarberShopAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BarberShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberController : ControllerBase
    {

        private readonly BarberService _barberService;

        public BarberController( BarberService barberService)
        {
            _barberService = barberService;
        }



        // GET: api/<BarberController>
        [HttpGet("GetAvailabilityByDate")]
        public async Task<IEnumerable<AvailabilityTimeSlot>> GetAvailabilityByDate(DateTime desiredDate)
        {
            List<AvailabilityTimeSlot> availability =  await _barberService.GetBarbersAvailability(desiredDate);
            return availability;
        }


        // POST api/<BarberController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPost ("create")]
        public async Task<IActionResult> CreateBarber([FromBody] Barber? barber)
        {
            await _barberService.Create(barber);
            return Ok();
        }


        // PUT api/<BarberController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BarberController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    
}
