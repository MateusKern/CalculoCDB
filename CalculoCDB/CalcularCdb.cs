namespace CalculoCDB
{
    public static class CalcularCdb
    {
        private static readonly decimal _tb = 108;
        private static readonly decimal _cdi = 0.9M;

        public static ResultadoCalculoCdb CalcularInvestimento(int meses, decimal valor)
        {
            var resultado = new ResultadoCalculoCdb();
            decimal porcentagem = meses.RetornaPorcentagemImposto();

            if (meses < 2)
                resultado.AddNotification("meses", "A quantidade de meses precisa ser maior que 1");

            if (valor < 0.01M)
                resultado.AddNotification("valor", "O valor precisa ser maior ou igual a R$ 0,01");

            if (porcentagem == 0)
                resultado.AddNotification("porcentagem", "Porcentagem não encontrada");

            if (!resultado.IsValid)
                return resultado;

            decimal valorBruto = valor;
            decimal multiplicador = 1 + (_cdi.ConverterPorcentagemEmDecimal() * _tb.ConverterPorcentagemEmDecimal());

            for (int i = 0; i < meses; i++)
                valorBruto *= multiplicador;

            decimal lucro = valorBruto - valor;
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
