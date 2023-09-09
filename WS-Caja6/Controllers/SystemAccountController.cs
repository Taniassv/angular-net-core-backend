using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WS_Caja6.Models.Request;
using WS_Caja6.Models.Response;
using WS_Caja6.Services;

namespace WS_Caja6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemAccountController : ControllerBase
    {
        private ISystemAccountService _systemAccountService;

        public SystemAccountController(ISystemAccountService systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }

        [HttpPost("login")]
        public IActionResult Authentication([FromBody] AuthRequest model) 
        {
            Respuesta oRespuesta = new Respuesta();
            var systemResponse = _systemAccountService.Auth(model);
            if(systemResponse == null ) 
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "Usuario o contraseña incorrecta";
                return BadRequest();
            }
            oRespuesta.Exito = 0;
            oRespuesta.Data = systemResponse;
            return Ok(oRespuesta);

        }
    }
}
