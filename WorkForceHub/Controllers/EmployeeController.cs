using WorkForceHub.Data;
using WorkForceHub.Model.DTos;
using WorkForceHub.Model.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpWebApp.Controllers
{
    [Route("api/v1/staff")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _dbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            // 1. Map the DTO to the Database Entity
            var employeeEntity = new Employee
            {
                FirstName = addEmployeeDto.FirstName,
                LastName = addEmployeeDto.LastName,
                Email = addEmployeeDto.Email,
                PhoneNumber = addEmployeeDto.PhoneNumber,
                Department = addEmployeeDto.Department,
                Position = addEmployeeDto.Position,
                Salary = addEmployeeDto.Salary,

                // Auto-assign system values (Business Logic)
                HireDate = DateTime.Now,
                Status = true
            };

            // 2. Add to EF Core tracking
            await _dbContext.Employees.AddAsync(employeeEntity);

            // 3. Save changes to SQL Server
            await _dbContext.SaveChangesAsync();

            // 4. Return success response
            return Ok(employeeEntity);
        }

        // 1. GET EMPLOYEE BY ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // 2. UPDATE EXISTING EMPLOYEE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] AddEmployeeDto updateDto)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            // Map the new values to the existing entity
            employee.FirstName = updateDto.FirstName;
            employee.LastName = updateDto.LastName;
            employee.Email = updateDto.Email;
            employee.PhoneNumber = updateDto.PhoneNumber;
            employee.Department = updateDto.Department;
            employee.Position = updateDto.Position;
            employee.Salary = updateDto.Salary;

            await _dbContext.SaveChangesAsync();

            return Ok(employee);
        }

        // 3. DELETE EMPLOYEE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}