namespace Esam.DAL.Entities;
public class Employee{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Salary { get; set; }
    public string Image { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdateBy { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; } = false;

    public override string ToString(){
        return $"Id: {Id}, Name: {Name}, Salary: {Salary}";
    }
}
