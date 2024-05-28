using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using APBD_Task8.Models;
using APBD_Task8.Repositories;
using APBD_Task8.Repositories;
using APBD_Task8.Repositories;

namespace APBD_Task8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;

        public TripsController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var trips = await _tripRepository.GetTrips(page, pageSize);
            var totalTrips = await _tripRepository.GetTripsCount();
            var totalPages = (int)Math.Ceiling((double)totalTrips / pageSize);

            return Ok(new
            {
                pageNum = page,
                pageSize,
                allPages = totalPages,
                trips = trips.Select(t => new
                {
                    t.Name,
                    t.Description,
                    t.DateFrom,
                    t.DateTo,
                    t.MaxPeople,
                    Countries = t.CountryTrips.Select(ct => new { ct.Country.Name }),
                    Clients = t.ClientTrips.Select(ct => new { ct.Client.FirstName, ct.Client.LastName })
                })
            });
        }
    }
}