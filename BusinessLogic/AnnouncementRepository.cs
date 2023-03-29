
using Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence;
using System.Net.Http.Headers;


namespace BusinessLogic;

public class AnnouncementRepository:IAnnouncementRepository
{
    private AnnouncementDbContext db;
   
    public AnnouncementRepository(AnnouncementDbContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<Announcement>> CreateAll()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://www.bitmex.com");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var  announcements = await GetAll();


        var annoucementsData = announcements.Select(a => new Announcement
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            Date=a.Date, 
            Link=a.Link, 
            

        });

        await db.Announcements.AddRangeAsync(annoucementsData);

        await db.SaveChangesAsync();
     
        return  announcements;

    }
    public async Task<IEnumerable<Announcement>> GetAll()
    {
        var apiResponse = await GetApiResponse("/api/v1/announcement");
        return apiResponse is null ? Enumerable.Empty<Announcement>() : apiResponse;
    }

    private async Task<IEnumerable<Announcement>> GetApiResponse(string endpoint)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://www.bitmex.com");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = await client.GetAsync(endpoint);
        string apiResponse = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<IEnumerable<Announcement>>(apiResponse);
        return  result!;
    }


    public async Task<IEnumerable<Announcement>> FilterByDate(DateTime date)
    {
        var utcDate= DateTime.SpecifyKind(date, DateTimeKind.Utc);

        var announcements= await db.Announcements 
                                .Where(a => a.Date.Date== utcDate.Date)
                                .OrderBy(a=>a.Date)
                                .ToListAsync();
        return announcements;
    }

    public async Task<IEnumerable<Announcement>> GetByDateDescending()
    {
        var sortedAnnouncements = await db.Announcements
                                .OrderByDescending(a => a.Date)
                                .ToListAsync(); 
        
        return sortedAnnouncements;
    }

   
}

