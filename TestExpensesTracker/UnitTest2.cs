namespace TestExpensesTracker
{
    [TestClass]
    public class UnitTest2
    {
        public class StubBuscadorTasas : IBuscadorTasas
        {
            public const float TASA_USD_DOP_BHD_LEON = 55.5f;
            public const float TASA_DOP_USD_BHD_LEON = 45.6f;

            public int llamadasParaObtenerTasas = 0;
            public Task<List<Tasa>> ObtenerTasas()
            {
                llamadasParaObtenerTasas++;
                List<Tasa> tasas = new List<Tasa>();
                tasas.Add(new Tasa(TASA_USD_DOP_BHD_LEON, "USD", "DOP", "Banco BHD León"));
                tasas.Add(new Tasa(TASA_DOP_USD_BHD_LEON, "DOP", "USD", "Banco BHD León"));
                return Task.FromResult(tasas);
            }
        }

        [TestMethod]
        public void TestBuscadorDeTasas()
        {
            // ARRANGE
            float tasaCorrecta = StubBuscadorTasas.TASA_USD_DOP_BHD_LEON;
            IBuscadorTasas buscadorTasas = new StubBuscadorTasas();
            Convertir sut = new Convertir(buscadorTasas);

            // ACT
            float pesos = sut.DolaresAPesos(50);

            //ASSERT
            Assert.AreEqual(50 * tasaCorrecta, pesos, 2);
            Assert.AreEqual(1, ((StubBuscadorTasas)buscadorTasas).llamadasParaObtenerTasas);
        }
    }
}