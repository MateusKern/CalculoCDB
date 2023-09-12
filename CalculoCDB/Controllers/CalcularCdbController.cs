using Microsoft.AspNetCore.Mvc;

namespace CalculoCDB.Controllers
{
    [Route("")]
    [ApiController]
    public class CalcularCdbController : ControllerBase
    {
        private readonly ILogger<CalcularCdbController> _logger;

        public CalcularCdbController(ILogger<CalcularCdbController> logger)
        {
            _logger = logger;
        }

        [HttpGet("calcular-cdb")]
        public IActionResult Get([FromQuery]int? meses, [FromQuery]decimal? valor)
        {
            try
            {
                var resultado = CalcularCdb.CalcularInvestimento(meses.TrataValor(), valor.TrataValor());

                if (!resultado.IsValid)
                    return BadRequest(resultado.Notifications.Select(n => n.Message));

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Mensagem: {Message} - StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
                return BadRequest(new List<string>() { "Aconteceu alguma coisa errada, por favor, contate o suporte." });
            }
        }
    }
}
