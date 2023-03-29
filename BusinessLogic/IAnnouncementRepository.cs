using Domain;

namespace BusinessLogic;


public interface IAnnouncementRepository
{
    Task<IEnumerable<Announcement>> CreateAll();
    Task<IEnumerable<Announcement>> GetAll();
    Task<IEnumerable<Announcement>>GetByDateDescending();
    Task<IEnumerable<Announcement>> FilterByDate(DateTime date); 


}
