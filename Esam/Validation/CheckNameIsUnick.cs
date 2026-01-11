namespace Esam.Validation;
using System.ComponentModel.DataAnnotations;
using DAL.Database;
using Microsoft.EntityFrameworkCore;

public class CheckNameIsUnick:ValidationAttribute{
    private readonly ApplicationDbContext _dbContext;
    public CheckNameIsUnick(){
        _dbContext = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
    }
    public override bool IsValid
    (
        object? value
    ){
                                                 return !_dbContext.Employees.Any(e=>value != null && e.Name.Equals(value.ToString(),StringComparison.OrdinalIgnoreCase));
    }
}
