namespace CalculoCDB.Teste
{
    [TestClass]
    public class CalcularCdbTeste
    {
        [TestMethod]
        public void DadosValidosDeveDarSucesso()
        {
            var resultado = CalcularCdb.CalcularInvestimento(10, 100);
            Assert.IsTrue(resultado.IsValid);
        }

        [TestMethod]
        public void MesesIgualAZeroDeveDarErro()
        {
            var resultado = CalcularCdb.CalcularInvestimento(0, 100);
            Assert.IsTrue(!resultado.IsValid);
        }

        [TestMethod]
        public void MesesMenorQueZeroDeveDarErro()
        {
            var resultado = CalcularCdb.CalcularInvestimento(-10, 100);
            Assert.IsTrue(!resultado.IsValid);
        }

        [TestMethod]
        public void MesesIgualAUmDeveDarErro()
        {
            var resultado = CalcularCdb.CalcularInvestimento(1, 100);
            Assert.IsTrue(!resultado.IsValid);
        }

        [TestMethod]
        public void ValorIgualAZeroDeveDarErro()
        {
            var resultado = CalcularCdb.CalcularInvestimento(5, 0);
            Assert.IsTrue(!resultado.IsValid);
        }

        [TestMethod]
        public void ValorNegativoDeveDarErro()
        {
            var resultado = CalcularCdb.CalcularInvestimento(5, -100);
            Assert.IsTrue(!resultado.IsValid);
        }
    }
}
