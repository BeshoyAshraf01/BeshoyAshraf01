namespace Esam.ModelVm;

public class GetEmployeeVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Salary { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    
    public IFormFile Image { get; set; }
    public string? ImageName { get; set; }
}
