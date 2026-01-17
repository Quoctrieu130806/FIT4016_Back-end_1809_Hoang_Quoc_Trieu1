using System.ComponentModel.DataAnnotations;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [RegularExpression(@"^ORD-\d{8}-\d{4}$", ErrorMessage = "Invalid format. Use ORD-YYYYMMDD-XXXX")]
    public string OrderNumber { get; set; }

    [Required, StringLength(100, MinimumLength = 2)]
    public string CustomerName { get; set; }

    [Required, EmailAddress]
    public string CustomerEmail { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public virtual Product Product { get; set; }
}