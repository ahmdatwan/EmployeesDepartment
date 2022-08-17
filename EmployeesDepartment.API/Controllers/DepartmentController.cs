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
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartments()
        {
            var departmentsEntities = await _departmentRepository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<DepartmentDTO>>(departmentsEntities));
        }
        [HttpGet("{departmentId}", Name = "GetDepartment")]
        public async Task<ActionResult<DepartmentDTO>> GetDepartment(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DepartmentDTO>(department));

        }
        [HttpPost]
        public async Task<ActionResult> CreateDepartment(DepartmentForCreationDTO department)
        {
            var departmentEntity = _mapper.Map<Department>(department);
            _departmentRepository.Insert(departmentEntity);
            await _departmentRepository.SaveChangesAsync();
            var createdDepartment = _mapper.Map<DepartmentDTO>(departmentEntity);
            return CreatedAtRoute("GetDepartment", new { departmentId = createdDepartment.Id }, createdDepartment);
        }

        [HttpPut("{departmentId}")]
        public async Task<ActionResult> UpdateDepartment(int departmentId,DepartmentForCreationDTO department)
        {
            var departmentToUpdate = await _departmentRepository.GetByIdAsync(departmentId);
            if (departmentToUpdate == null)
                return NotFound();
            _mapper.Map(department, departmentToUpdate);
            await _departmentRepository.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{departmentId}")]
        public async Task<ActionResult> PartiallyUpdateDepartment(int departmentId, JsonPatchDocument<DepartmentForCreationDTO> patchDocument)
        {
            var departmentToUpdate = await _departmentRepository.GetByIdAsync(departmentId);
            if (departmentToUpdate == null)
                return NotFound();
            var departmentToPatch = _mapper.Map<DepartmentForCreationDTO>(departmentToUpdate);
            patchDocument.ApplyTo(departmentToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!TryValidateModel(departmentToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(departmentToPatch, departmentToUpdate);

            await _departmentRepository.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{departmentId}")]
        public async Task<ActionResult> DeleteDepartment(int departmentId)
        {
            var departmentToDelete = await _departmentRepository.GetByIdAsync(departmentId);
            if (departmentToDelete == null  )
                return NotFound();
           
           
            _departmentRepository.DeleteAsync(departmentToDelete);
            await _departmentRepository.SaveChangesAsync();
            return NoContent();

        }




    }
}
