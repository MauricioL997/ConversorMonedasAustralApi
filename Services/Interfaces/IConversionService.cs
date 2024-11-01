using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IConversionService
    {
        List<ConversionDto> GetUserConversions(int userId);
        ConversionDto PerformConversion(int userId, string fromCurrency, string toCurrency, decimal amount);
    }
}
