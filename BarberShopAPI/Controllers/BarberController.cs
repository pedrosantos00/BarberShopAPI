using BarberShopAPI.BusinessLogic;
using BarberShopAPI.DAL;
using BarberShopAPI.Domain;
using BarberShopAPI.ModelDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BarberShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberController : ControllerBase
    {

        private readonly BarberService _barberService;
        private readonly JWTService _jwtService;
        private readonly BarberShopDbContext _context;


        public BarberController( BarberService barberService, JWTService JWTService, BarberShopDbContext context)
        {
            _barberService = barberService;
            _jwtService = JWTService;
            _context = context;
        }

        // GET: api/<BarberController>
        [HttpGet("getBarbers")]
        public async Task<IEnumerable<Barber>> GetBarbers()
        {
            List<Barber> barberList = await _barberService.GetBarbers();
            return barberList;
        }

        // GET: api/<BarberController>
        [HttpGet("GetAvailabilityByDate")]
        public async Task<IEnumerable<AvailabilityTimeSlot>> GetAvailabilityByDate(DateTime desiredDate, int appointmentDuration)
        {
            List<AvailabilityTimeSlot> availability =  await _barberService.GetBarbersAvailability(desiredDate, appointmentDuration);
            return availability;
        }


        // POST: /barber/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Barber? barber)
        {
            // Check if barber object is null
            if (barber == null)
                return BadRequest();


            Barber barberExists = await _barberService.GetById(barber.Id);

            // Check if barber exists
            if (barberExists == null)
                return NotFound(new { Message = "Barber Not Found!" });

            // Verify password
            if (!PasswordHasher.VerifyPassword(barber.Password, barberExists.Password))
                return BadRequest(new { Message = "Incorrect Password" });


            // Generate access token and refresh token
            barberExists.Token = _jwtService.CreateJwt(barberExists);
            var newAccessToken = barberExists.Token;
            var newRefreshToken = _jwtService.CreateRefreshToken();
            barberExists.RefreshToken = newRefreshToken;
            barberExists.RefreshTokenExpiryTime = DateTime.Now.AddDays(14);
            await _context.SaveChangesAsync();

            // Return tokens to the client
            return Ok(new TokenApiDTO()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        // POST: /barber/refresh
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenApiDTO? tokenApiDto)
        {
            if (tokenApiDto is null)
                return BadRequest("Invalid Client Request");

            string accessToken = tokenApiDto.AccessToken;
            string refreshToken = tokenApiDto.RefreshToken;

            var principal = _jwtService.GetPrincipalFromExpiredToken(accessToken);
            var id = Convert.ToInt32(principal.Identity.Name);

            Barber barber = await _barberService.GetById(id);

            // Check if barber exists and if the provided refresh token is valid
            if (barber is null || barber.RefreshToken != refreshToken || barber.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid Request");

            // Generate new access token and refresh token
            var newAccessToken = _jwtService.CreateJwt(barber);
            var newRefreshToken = _jwtService.CreateRefreshToken();
            barber.RefreshToken = newRefreshToken;
            await _context.SaveChangesAsync();

            // Return new tokens to the client
            return Ok(new TokenApiDTO()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
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
