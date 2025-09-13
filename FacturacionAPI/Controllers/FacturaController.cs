using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FacturacionAPILibreria.Domain;
using FacturacionAPILibreria.Service;

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private FacturaService dataApi;

        public FacturaController()
        {
            dataApi = new FacturaService();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Factura> lst = null;
            try
            {
                lst = dataApi.GetAll();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpPost]
        public IActionResult PostFactura(Factura factura)
        {
            try
            {
                if (factura == null)
                {
                    return BadRequest("Datos de presupuesto incorrectos!");
                }
                return Ok(dataApi.SaveFactura(factura));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        // PUT: api/factura/{id}
        [HttpPut("{id}")]
        public IActionResult PutFactura(int id, Factura factura)
        {
            try
            {
                if (factura == null || factura.nroFactura != id)
                    return BadRequest("Datos de factura inválidos!");

                bool actualizado = dataApi.UpdateFactura(factura);

                if (!actualizado)
                    return NotFound("No se pudo actualizar la factura, puede que no exista.");

                return Ok("Factura actualizada correctamente.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        // DELETE: api/factura/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteFactura(int id)
        {
            try
            {
                bool eliminado = dataApi.DeleteFactura(id);

                if (!eliminado)
                    return NotFound("La factura no existe o no pudo eliminarse.");

                return Ok("Factura eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }

        }
    }
}
