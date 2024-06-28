using back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        public InfoController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listInfo = await _context.GetAllInfoAsync();

                return Ok(listInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var info = await _context.Info.FindAsync(id);

                if (info == null)
                {
                    return NotFound();
                }

                return Ok(info);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Info info)
        {
            try
            {
                await _context.InsertInfoAsync(info.Nombre, info.Apellido, info.Edad, info.FechaCreacion);

                return Ok(info);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Info info)
        {
            try
            {
                if (id != info.Id)
                {
                    return BadRequest();
                }

                await _context.Database.ExecuteSqlInterpolatedAsync($"CALL UpdateInfo({info.Id}, {info.Nombre}, {info.Apellido}, {info.Edad}, {info.FechaCreacion})");

                return Ok(new { message = "Se actualizó correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"CALL DeleteInfo({id})");

                return Ok(new { message = "Se eliminó correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
