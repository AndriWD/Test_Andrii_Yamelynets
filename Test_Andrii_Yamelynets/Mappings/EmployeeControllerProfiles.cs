using AutoMapper;
using WebApp.Core.DTO;
using WebApp.Models.EmployeeController.ResponseModels;

namespace WebApp.Mappings;

public class EmployeeControllerProfiles : Profile
{
    public EmployeeControllerProfiles()
    {
        this.CreateMap<EmployeeTreeDto, GetEmployeeTreeResponse>();
    }
}
