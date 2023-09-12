namespace CalculoCDB.Teste
{
    [TestClass]
    public class ExtensoesTeste
    {
        [TestMethod]
        [DataRow(0)]
        [DataRow(5)]
        [DataRow(10)]
        public void TratarIntDeveDarSucesso(int? numero)
        {
            Assert.IsTrue(numero.TrataValor() == numero);
        }

        [TestMethod]
        public void TratarIntDeveDarSucesso()
        {
            int? numero = null;
            Assert.IsTrue(numero.TrataValor() == 0);
        }

        [TestMethod]
        public void TratarDecimalDeveDarSucesso()
        {
            decimal? numero = 10.5M;
            Assert.IsTrue(numero.TrataValor() == numero);
        }

        [TestMethod]
        public void TratarDecimalNuloDeveDarSucesso()
        {
            decimal? numero = null;
            Assert.IsTrue(numero.TrataValor() == 0);
        }

        [TestMethod]
        public void ConverterPorcentagemEmDecimalDeveDarSucesso()
        {
            decimal porcentagem = 98;
            Assert.IsNotNull(porcentagem.ConverterPorcentagemEmDecimal());
        }

        [TestMethod]
        public void ConverterZeroEmDecimalDeveDarSucesso()
        {
            decimal porcentagem = 0;
            Assert.IsNotNull(porcentagem.ConverterPorcentagemEmDecimal());
        }

        [TestMethod]
        public void BuscarPorcentagemComMesValidoDeveDarSucesso()
        {
            Assert.IsNotNull(10.RetornaPorcentagemImposto());
        }

        [TestMethod]
        public void BuscarPorcentagemComNenhumMesDeveDarSucesso()
        {
            Assert.IsNotNull(0.RetornaPorcentagemImposto());
        }

        [TestMethod]
        public void BuscarPorcentagemNegativaDeveDarZero()
        {
            int meses = -10;
            Assert.IsTrue(meses.RetornaPorcentagemImposto() == 0);
        }
    }
}
