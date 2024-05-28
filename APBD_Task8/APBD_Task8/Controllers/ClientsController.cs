using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using APBD_Task8.Models;
using APBD_Task8.Repositories;
using APBD_Task8.Models;
using APBD_Task8.Repositories;

namespace APBD_Task8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;

        public ClientsController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            var client = await _tripRepository.GetClientById(idClient);
            if (client == null)
                return NotFound(new { message = "Client not found" });

            if (client.ClientTrips.Any())
                return BadRequest(new { message = "Client has assigned trips and cannot be deleted" });

            var result = await _tripRepository.DeleteClient(idClient);
            if (!result)
                return StatusCode(500, new { message = "Error deleting client" });

            return NoContent();
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] Client client)
        {
            var result = await _tripRepository.AssignClientToTrip(client, idTrip);
            if (!result)
                return BadRequest(new { message = "Error assigning client to trip" });

            return Ok(new { message = "Client assigned to trip successfully" });
        }
    }
}