using Microsoft.AspNetCore.Mvc;
namespace Example02.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Employee> Get()
    {
        return GetEmployees();
    }

    [HttpGet("{id}", Name = "Get")]
    public Employee Get(int id)
    {
        return GetEmployees().Find(e => e.Id == id);
    }

    [HttpPost]
    [Produces("application/json")]
    public Employee Post([FromBody] Employee employee)
    {
        return new Employee();
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Employee employee)
    { }

    [HttpDelete("{id}")]
    public void Delete(int id)
    { }

    private List<Employee> GetEmployees()
    {
        return new List<Employee>()
        {
            new Employee()
            {
                Id = 1,
                FirstName = "Trung",
                LastName = "Vuong",
                EmailId = "trung.ftios@gmail.com"
            },
            new Employee()
            {
                Id = 2,
                FirstName = "An",
                LastName = "Nguyen",
                EmailId = "annguyen@gmail.com"
            },
            new Employee()
            {
                Id = 3,
                FirstName = "Tu",
                LastName = "Nguyen",
                EmailId = "nguyentu@gmail.com"
            }
        };
    }
}