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
        private readonly ClientService _clientService;

        public AppointmentController(BarberService barberService, AppointmentService appointmentService, ClientService clientService)
        {
            _barberService = barberService;
            _appointmentService = appointmentService;
            _clientService = clientService;
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

            Client client = await _clientService.GetByPhoneNumber(appointment.Client.PhoneNumber);

            if (appointment.Barber.Name == "noPreference")
            {

              List<AvailabilityTimeSlot> avaiableBarbers =   await _barberService.GetBarbersAvailability(appointment.AppointmentDate, appointment.ExpectedTime);

                avaiableBarbers.RemoveAll(barber => !barber.AvailableTimeSlots.Contains(appointment.AppointmentDate));

                List<int> barberIds = avaiableBarbers.Select(barber => barber.BarberId).ToList();

                Random random = new Random();
                int randomBarberId = barberIds[random.Next(0, barberIds.Count)];
                int barberId = randomBarberId;

                appointment.Barber = await _barberService.GetById(barberId);
                appointment.BarberId = barberId;
            }
            else
            {
                appointment.Barber = await _barberService.GetById(appointment.BarberId);
                appointment.BarberId = appointment.Barber.Id;
            }

            if (client != null)
            {
                appointment.Client = client;
                appointment.ClientId = client.Id;
            }
                
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
