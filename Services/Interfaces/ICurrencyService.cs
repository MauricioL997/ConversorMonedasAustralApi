using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICurrencyService
    {
        List<CurrencyDto> GetAllCurrencies();
        CurrencyDto GetCurrencyById(int id);
        int AddCurrency(CurrencyDto currencyDto);
        bool UpdateCurrency(int id, CurrencyDto currencyDto);
        bool DeleteCurrency(int id);
    }
}
