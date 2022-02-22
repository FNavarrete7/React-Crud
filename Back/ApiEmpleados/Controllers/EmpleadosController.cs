using ApiEmpleados.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEmpleados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadosContext _context;
        public EmpleadosController(EmpleadosContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<List<Empleado>> Get()
        {
            using (var db = new EmpleadosContext())
            {
                var empleados = db.Empleados.ToList();
                return empleados;
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Empleado> GetById(int id)
        {
            using (var db = new EmpleadosContext())
            {
                var empleados = db.Empleados.Find(id);
                return empleados;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Empleado>> CrearEmpleado([FromBody] Empleado empleado)
        {
            _context.Add(empleado);
            await _context.SaveChangesAsync();

            return Ok(empleado);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> ModificarEmpleado(int id,[FromBody] Empleado empleado)
        {
            try
            {
                _context.Update(empleado);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Se actualizo correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarEmpleado(int id, [FromBody] Empleado empleado)
        {
            try
            {
                if (id != empleado.Id)
                {
                    return NotFound();
                }
                _context.Remove(empleado);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Se borro correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
