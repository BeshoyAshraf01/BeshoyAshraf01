namespace Esam.Validation;
using System.ComponentModel.DataAnnotations;
using DAL.Database;
using Microsoft.EntityFrameworkCore;

public class CheckNameIsUnique:ValidationAttribute{
    private readonly ApplicationDbContext _dbContext;
    public CheckNameIsUnique(){
        _dbContext = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext){
        var name = value as string;
        var exists = _dbContext.Employees.Any(e => string.Equals(e.Name, name, StringComparison.Ordinal));
        return exists ? new ValidationResult("Name must be unique.") : ValidationResult.Success;
    } 
}
