using Microsoft.AspNetCore.Http;
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

        public CalcularCdbController()
        {
        }

        [HttpGet("calcular-cdb")]
        public ResultadoCalculoCdb Get([FromQuery]int meses, [FromQuery]decimal valor)
        {
            decimal valorBruto = valor * (1 + (_cdi.ConverterPorcentagemEmDecimal() * _tb.ConverterPorcentagemEmDecimal()));
            decimal lucro = valorBruto - valor;
            decimal porcentagem = meses.RetornaPorcentagemImposto(_impostos);
            decimal lucroLiquido = lucro - (lucro * porcentagem);
            decimal valorLiquido = valor + lucroLiquido;

            return new ResultadoCalculoCdb
            {
                DataRetirada = DateOnly.FromDateTime(DateTime.Now.AddMonths(meses)),
                ValorBruto = valorBruto,
                Porcentagem = porcentagem,
                ValorInicial = valor,
                ValorLiquido = valorLiquido
            };
        }
    }
}
