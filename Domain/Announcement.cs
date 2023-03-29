using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Announcement
{
    [Key]
    public int Id { get; set; }

    public string? Link { get; set;}

    public string? Title { get; set;}

    public string? Content { get; set;}

    public DateTime Date { get; set;}
}
