using Common.DTO;
using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISubscriptionService
    {
        SubscriptionDto GetSubscriptionByType(SubscriptionType type);
        List<SubscriptionDto> GetAllSubscriptions();
        int GetConversionLimit(SubscriptionType type);
    }
}
