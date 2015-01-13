using System;

namespace StockDataWebApi.ApiRepository
{
    /// <summary>
    /// Interfejs dla finansowych danych
    /// </summary>
    public interface IFinancialData
    {
        Results GetFinancialDataFromCompany(String companyName);
        Results GetFinancialDataFromCompanies();

        string GetNewsForCompany(string companyName);
    }
}