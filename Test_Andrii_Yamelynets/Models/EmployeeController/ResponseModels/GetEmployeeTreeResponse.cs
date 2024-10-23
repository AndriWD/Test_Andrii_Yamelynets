using Newtonsoft.Json;

namespace WebApp.Models.EmployeeController.ResponseModels
{
    public class GetEmployeeTreeResponse
    {
        public GetEmployeeTreeResponse()
        {
            Employees = new List<GetEmployeeTreeResponse>();
        }

        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ManagerID")]
        public int? ManagerID { get; set; }

        [JsonProperty("Employees")]
        public List<GetEmployeeTreeResponse> Employees { get; set; }
    }
}
