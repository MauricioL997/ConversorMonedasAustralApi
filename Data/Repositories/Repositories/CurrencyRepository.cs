using Data.Context;
using Data.Entities;
using Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ApplicationContext _context;

        public CurrencyRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Currency> GetAllCurrency()
        {
            return _context.Currency.ToList();
        }

        public Currency GetCurrencyById(int id)
        {
            return _context.Currency.FirstOrDefault(c => c.Id == id);
        }

        public Currency GetCurrencyByCode(string code) // Implementar el método
        {
            return _context.Currency.FirstOrDefault(c => c.Code == code);
        }

        public int AddCurrency(Currency currency)
        {
            _context.Currency.Add(currency);
            _context.SaveChanges();
            return currency.Id;
        }

        public bool UpdateCurrency(Currency currency)
        {
            var existingCurrency = _context.Currency.FirstOrDefault(c => c.Id == currency.Id);
            if (existingCurrency != null)
            {
                existingCurrency.Code = currency.Code;
                existingCurrency.Symbol = currency.Symbol;
                existingCurrency.Legend = currency.Legend;
                existingCurrency.ConvertibilityIndex = currency.ConvertibilityIndex;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteCurrency(int id)
        {
            var currency = _context.Currency.FirstOrDefault(c => c.Id == id);
            if (currency != null)
            {
                _context.Currency.Remove(currency);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
