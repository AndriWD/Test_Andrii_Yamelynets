using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.EmployeeController.RequestModels;

public class EnableEmployeeRequest
{
    [Required]
    [Min(1)]
    public int EmployeeID { get; set; }
}
