namespace Esam.Models;
public class Employee{
    public Employee
    (
        int id,
        string name,
        double salary
    ){
        Id = id;
        Name = name;
        Salary = salary;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public double Salary { get; set; }
    public override string ToString(){
        return $"Id: {Id}, Name: {Name}, Salary: {Salary}";
    }
}
