using CafeManagement.API.Models;
using CafeManagement.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _repository;

        public RoleController(IRoleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _repository.GetAllAsync();

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _repository.GetByIdAsync(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            var id = await _repository.CreateAsync(role);

            role.RoleId = id;

            return CreatedAtAction(
                nameof(GetById),
                new { id = id },
                role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            Role role)
        {
            role.RoleId = id;

            var result = await _repository.UpdateAsync(role);

            if (!result)
                return NotFound();

            return Ok(new
            {
                message = "Cập nhật Role thành công."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok(new
            {
                message = "Xóa Role thành công."
            });
        }
    }
}