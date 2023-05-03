using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;


namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _DashboardServicio;

        public DashboardController(IDashboardService dashboardServicio)
        {
            _DashboardServicio = dashboardServicio;
        }

        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            var rsp = new Response<DashboardDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _DashboardServicio.Resumen();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }
    }
}
