using Common.DTO;
using Data.Entities;
using Data.Repositories.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public List<CurrencyDto> GetAllCurrencies()
        {
            var currencies = _currencyRepository.GetAllCurrency();
            return currencies.Select(c => new CurrencyDto
            {
                Code = c.Code,
                Legend = c.Legend,
                Symbol = c.Symbol,
                ConvertibilityIndex = c.ConvertibilityIndex
            }).ToList();
        }

        public CurrencyDto GetCurrencyById(int id)
        {
            var currency = _currencyRepository.GetCurrencyById(id);
            if (currency == null)
            {
                return null;
            }
            return new CurrencyDto
            {
                Code = currency.Code,
                Legend = currency.Legend,
                Symbol = currency.Symbol,
                ConvertibilityIndex = currency.ConvertibilityIndex
            };
        }

        public int AddCurrency(CurrencyDto currencyDto)
        {
            var currency = new Currency
            {
                Code = currencyDto.Code,
                Legend = currencyDto.Legend,
                Symbol = currencyDto.Symbol,
                ConvertibilityIndex = currencyDto.ConvertibilityIndex
            };
            return _currencyRepository.AddCurrency(currency);
        }

        public bool UpdateCurrency(int id, CurrencyDto currencyDto)
        {
            var currency = new Currency
            {
                Id = id,
                Code = currencyDto.Code,
                Legend = currencyDto.Legend,
                Symbol = currencyDto.Symbol,
                ConvertibilityIndex = currencyDto.ConvertibilityIndex
            };
            return _currencyRepository.UpdateCurrency(currency);
        }

        public bool DeleteCurrency(int id)
        {
            return _currencyRepository.DeleteCurrency(id);
        }
    }
}
