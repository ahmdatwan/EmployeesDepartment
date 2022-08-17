using AutoMapper;
using EmployeesDepartment.API.Entities;
using EmployeesDepartment.API.Models;
using EmployeesDepartment.API.Repository;
using EmployeesDepartment.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesDepartment.API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository?? throw new ArgumentNullException(nameof(employeeRepository));
            _departmentRepository = departmentRepository?? throw new ArgumentNullException(nameof(departmentRepository));
            _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>>GetEmployees()
        {
          var employeeEntities = await _employeeRepository.GetAllAsync();
          return Ok(_mapper.Map<IEnumerable<EmployeeDTO>>(employeeEntities));
        }
        [HttpGet("{employeeId}" , Name= "GetEmployee")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if(employee == null)
                return NotFound();
            return Ok(_mapper.Map<EmployeeDTO>(employee));
        }
        [HttpGet("departmentEmployees/{departmentId}")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetDepartmentEmployees(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
                return NotFound();
            var departmentEmployees = await _employeeRepository.GetDepartmentEmployees(departmentId);
            return Ok(_mapper.Map<IEnumerable<EmployeeDTO>>(departmentEmployees));
        }
        [HttpPost("createEmployeeAtDepartment/{departmentId}")]
        public async Task<ActionResult> CreateEmployee( int  departmentId, EmployeeForCreationDTO employee)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
                return NotFound();
            var employeeEntity = _mapper.Map<Employee>(employee);
            _employeeRepository.Insert(employeeEntity,department);
            await _employeeRepository.SaveChangesAsync();
            var createdEmployee = _mapper.Map<EmployeeDTO>(employeeEntity);
            return CreatedAtRoute("GetEmployee", new {employeeId = createdEmployee.Id},createdEmployee);

        }
        [HttpPut("{employeeId}")]
        public async Task<ActionResult> UpdateEmployee(int employeeId, EmployeeForCreationDTO employee)
        {
            var employeeToUpdate = await _employeeRepository.GetByIdAsync(employeeId);
            if (employeeToUpdate == null)
                return NotFound();
            _mapper.Map(employee, employeeToUpdate);
            await _employeeRepository.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{employeeId}")]
        public async Task<ActionResult> PartiallyUpdateEmployee(int employeeId, JsonPatchDocument<EmployeeForCreationDTO> patchDocument)
        {
            var employeeToUpdate = await _employeeRepository.GetByIdAsync(employeeId);
            if (employeeToUpdate == null)
                return NotFound();
            var employeeToPatch = _mapper.Map<EmployeeForCreationDTO>(employeeToUpdate);
            patchDocument.ApplyTo(employeeToPatch, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!TryValidateModel(employeeToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(employeeToPatch,employeeToUpdate);

            await _employeeRepository.SaveChangesAsync();

            return NoContent();

        }
        [HttpDelete("{employeeId}")]
        public async Task<ActionResult> DeleteEmployee(int employeeId)
        {
            var employeeToDelete = await _employeeRepository.GetByIdAsync(employeeId);
            if (employeeToDelete == null)
                return NotFound();


            _employeeRepository.DeleteAsync(employeeToDelete);
            await _employeeRepository.SaveChangesAsync();
            return NoContent();
        }
       
        
    }
}
