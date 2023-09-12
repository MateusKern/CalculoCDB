namespace CalculoCDB
{
    public static class Extensoes
    {
        public static int TrataValor(this int? valor) =>
            valor == null ? 0 : valor.Value;

        public static decimal TrataValor(this decimal? valor) =>
            valor == null ? 0 : valor.Value;

        public static decimal ConverterPorcentagemEmDecimal(this decimal porcentagem) =>
            porcentagem / 100;

        public static decimal RetornaPorcentagemImposto(this int meses, IReadOnlyCollection<Imposto> impostos)
        {
            if (impostos is not null)
            {
                Imposto imposto = impostos.First(i => meses >= i.MesesInicial && meses <= i.MesesFinal);

                if (imposto is not null)
                    return imposto.Porcentagem.ConverterPorcentagemEmDecimal();
            }
            throw new Exception("Porcentagem não encontrada");
        }
    }
}
