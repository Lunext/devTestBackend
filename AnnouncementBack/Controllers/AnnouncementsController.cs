using BusinessLogic;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementRepository repo; 

        public AnnouncementsController(IAnnouncementRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Announcement>))]
        public async Task<IEnumerable<Announcement>> GetAnnouncements()
        {
            return await repo.GetAll();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)] 
        public async Task<ActionResult<IEnumerable<Announcement>>> CreateAllAnnoucements( )
        {
           var  announcements = await repo.GetAll();

            if (!announcements.Any())
            {
                return BadRequest("There's no annucemnts from the api you are consuming"); 
                
            }

            var addedAnnouncements = await repo.CreateAll();

            if (!addedAnnouncements.Any())
            {
                return BadRequest("There are no any annoucements created");
            }

            return CreatedAtAction(nameof(CreateAllAnnoucements), addedAnnouncements); 
        }

        [HttpGet("AnnoucementsByDate")]
        [ProducesResponseType(200,  Type = typeof(IEnumerable<Announcement>) )]
        public async Task<IEnumerable<Announcement>> GetAnnouncementsByDescedingDate()
        {
            return await repo.GetByDateDescending();
        }

        [HttpGet("{date}")]
        [ProducesResponseType(200, Type=typeof(IEnumerable<Announcement>))]
        [ProducesResponseType(404)]
        public async Task<IEnumerable<Announcement>> FilterByDate(DateTime date)
        {



            var announcements = repo.FilterByDate(date);
            return await announcements;
            
        }
    }
}


