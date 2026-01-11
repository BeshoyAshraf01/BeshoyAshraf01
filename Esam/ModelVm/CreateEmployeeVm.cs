namespace Esam.ModelVm;
#region

using System.ComponentModel.DataAnnotations;
using Validation;

#endregion

public class CreateEmployeeVm{
    [Required(ErrorMessage = "Name is required"), MinLength(10, ErrorMessage = "Name must be at least 10 characters long")]
    [CheckNameIsUnick]
    public string Name { get; set; }

    public double Salary { get; set; }
    public IFormFile Image { get; set; }
}
