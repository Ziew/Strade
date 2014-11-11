namespace StockTrader.App_Start
{
    public interface IStockValuesRepository
    {
        double getValue(string companySymbol);
    }
}