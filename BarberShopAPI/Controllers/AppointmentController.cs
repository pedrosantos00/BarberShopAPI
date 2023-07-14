using BarberShopAPI.BusinessLogic;
using BarberShopAPI.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BarberShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {

        private readonly BarberService _barberService;
        private readonly AppointmentService _appointmentService;

        public AppointmentController(BarberService barberService, AppointmentService appointmentService)
        {
            _barberService = barberService;
            _appointmentService = appointmentService;
        }


        // GET: api/<AppointmentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AppointmentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        

        // POST api/<AppointmentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


        // POST api/<AppointmentController>
        [HttpPost("BookAnAppointment")]
        public async Task<IActionResult> BookAnAppointment([FromBody] Appointment? appointment)
        {
            if (appointment == null || appointment.Client == null) return BadRequest();

            

            if(appointment.Barber.Name == "noPreference")
            {
                // Escolher ao calhas um Barbeiro com data disponivel e colocar appointment para este

                await _appointmentService.Create(appointment);

                return Ok();
            }

                appointment.Barber =  await  _barberService.GetById(appointment.BarberId);
                await _appointmentService.Create(appointment);
                return Ok();
        }


        // PUT api/<AppointmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AppointmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
