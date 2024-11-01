using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface ICurrencyRepository
    {
        List<Currency> GetAllCurrency();
        Currency GetCurrencyById(int id);
        Currency GetCurrencyByCode(string code); 
        int AddCurrency(Currency currency);
        bool UpdateCurrency(Currency currency);
        bool DeleteCurrency(int id);
    }
}

