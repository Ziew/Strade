namespace StockTrader.Models
{
    /// <summary>
    /// Klasa, która służy do przechowywania informacji o aktulnej cenie akcji
    /// </summary>
    public class CompanyViewModel
    {
        public string name { get; set; }
        public double price { get; set; }

        public string symbol { get; set; }
    }
}