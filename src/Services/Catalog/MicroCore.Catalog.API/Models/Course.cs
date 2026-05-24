namespace MicroCore.Catalog.API.Models;

public class Course
{

    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public Guid UserId { get; set; }
    public string? ImageUrl { get; set; }

    public DateTime Created { get; set; }

    public int PurchaseCount { get; set; } = 0;

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public Feature Feature { get; set; } = default!;
}
