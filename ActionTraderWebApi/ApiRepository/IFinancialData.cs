using System;

namespace StockDataWebApi.ApiRepository
{
    public interface IFinancialData
    {
        Results GetFinancialDataFromCompany(String companyName);
        Results GetFinancialDataFromCompanies();
    }
}