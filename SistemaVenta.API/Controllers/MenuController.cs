using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _MenuServicio;

        public MenuController(IMenuService menuServicio)
        {
            _MenuServicio = menuServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> List(int idUsuario)
        {
            var rsp = new Response<List<MenuDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _MenuServicio.Lista(idUsuario);
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
