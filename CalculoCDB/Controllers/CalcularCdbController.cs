using Microsoft.AspNetCore.Mvc;

namespace CalculoCDB.Controllers
{
    [Route("")]
    [ApiController]
    public class CalcularCdbController : ControllerBase
    {
        private readonly decimal _tb = 108;
        private readonly decimal _cdi = 0.9M;
        private readonly IReadOnlyCollection<Imposto> _impostos = new List<Imposto>()
        {
            new Imposto() { MesesInicial = 0, MesesFinal = 6, Porcentagem = 22.5M },
            new Imposto() { MesesInicial = 7, MesesFinal = 12, Porcentagem = 20 },
            new Imposto() { MesesInicial = 13, MesesFinal = 24, Porcentagem = 17.5M },
            new Imposto() { MesesInicial = 25, MesesFinal = int.MaxValue, Porcentagem = 15 }
        };
        private readonly ILogger<CalcularCdbController> _logger;

        public CalcularCdbController(ILogger<CalcularCdbController> logger)
        {
            _logger = logger;
        }

        [HttpGet("calcular-cdb")]
        public IActionResult Get([FromQuery]int? meses, [FromQuery]decimal? valor)
        {
            List<string> mensagensErro = new();

            try
            {
                if (meses is null || meses.TrataValor() < 2)
                    mensagensErro.Add("A quantidade de meses precisa ser maior que 1");

                if (valor is null || valor.TrataValor() < 0.01M)
                    mensagensErro.Add("O valor precisa ser maior ou igual a R$ 0,01");

                if (mensagensErro.Any())
                    return BadRequest(mensagensErro);

                decimal valorBruto = valor.TrataValor();

                for (int i = 0; i < meses; i++)
                    valorBruto *= 1 + (_cdi.ConverterPorcentagemEmDecimal() * _tb.ConverterPorcentagemEmDecimal());

                decimal lucro = valorBruto - valor.TrataValor();
                decimal porcentagem = meses.TrataValor().RetornaPorcentagemImposto(_impostos);
                decimal lucroLiquido = lucro - (lucro * porcentagem);
                decimal valorLiquido = valor.TrataValor() + lucroLiquido;

                var resultado = new ResultadoCalculoCdb
                {
                    DataRetirada = DateOnly.FromDateTime(DateTime.Now.AddMonths(meses.TrataValor())),
                    ValorBruto = valorBruto,
                    Porcentagem = porcentagem,
                    ValorInicial = valor.TrataValor(),
                    ValorLiquido = valorLiquido
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Mensagem: {Message} - StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
                mensagensErro.Add("Aconteceu alguma coisa errada, por favor, contate o suporte.");
                return BadRequest(mensagensErro);
            }
        }
    }
}
