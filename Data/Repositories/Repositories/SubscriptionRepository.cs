using Common.Enum;
using Data.Context;
using Data.Entities;
using Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationContext _context;

        public SubscriptionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Subscription> GetAllSubscriptions()
        {
            return _context.Subscription.ToList();
        }

        public Subscription GetSubscriptionByType(SubscriptionType type)
        {
            return _context.Subscription.FirstOrDefault(s => s.Type == type);
        }
    }
}
