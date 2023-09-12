namespace CalculoCDB
{
    public static class Extensoes
    {
        private static readonly IReadOnlyCollection<Imposto> _impostos = new List<Imposto>()
        {
            new Imposto() { MesesInicial = 0, MesesFinal = 6, Porcentagem = 22.5M },
            new Imposto() { MesesInicial = 7, MesesFinal = 12, Porcentagem = 20 },
            new Imposto() { MesesInicial = 13, MesesFinal = 24, Porcentagem = 17.5M },
            new Imposto() { MesesInicial = 25, MesesFinal = int.MaxValue, Porcentagem = 15 }
        };

        public static int TrataValor(this int? valor) =>
            valor == null ? 0 : valor.Value;

        public static decimal TrataValor(this decimal? valor) =>
            valor == null ? 0 : valor.Value;

        public static decimal ConverterPorcentagemEmDecimal(this decimal porcentagem) =>
            porcentagem / 100;

        public static decimal RetornaPorcentagemImposto(this int meses)
        {
            Imposto? imposto = _impostos.FirstOrDefault(i => meses >= i.MesesInicial && meses <= i.MesesFinal);

            if (imposto is not null)
                return imposto.Porcentagem.ConverterPorcentagemEmDecimal();

            return 0;
        }
    }
}
