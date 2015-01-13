namespace StockTrader.App_Start
{
    /// <summary>
    /// Interfejs
    /// </summary>
    public interface IStockValuesRepository
    {
        double getValue(string companySymbol);
    }
}