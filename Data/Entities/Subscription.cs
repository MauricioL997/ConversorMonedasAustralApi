using Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public int ConversionLimit { get; set; }
        public bool MonthlyReset { get; set; }
        public Common.Enum.SubscriptionType State { get; set; }
}
}
