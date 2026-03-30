namespace MicroCore.Catalog.API.Models;

public class Feature
{
    public int Duration { get; set; }
    public float Rating { get; set; }

    public string EducatorFullName { get; set; } = null!;
}
