using Common.DTO;
using Common.Enum;
using Data.Repositories.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public SubscriptionDto GetSubscriptionByType(SubscriptionType type)
        {
            var subscription = _subscriptionRepository.GetSubscriptionByType(type);
            if (subscription == null) return null;

            return new SubscriptionDto
            {
                Id = subscription.Id,
                Type = subscription.Type,
                ConversionLimit = GetConversionLimit(subscription.Type),
                MonthlyReset = subscription.MonthlyReset
            };
        }

        public List<SubscriptionDto> GetAllSubscriptions()
        {
            var subscriptions = _subscriptionRepository.GetAllSubscriptions();

            return subscriptions.Select(subscription => new SubscriptionDto
            {
                Id = subscription.Id,
                Type = subscription.Type,
                ConversionLimit = GetConversionLimit(subscription.Type),
                MonthlyReset = subscription.MonthlyReset
            }).ToList();
        }

        public int GetConversionLimit(SubscriptionType type)
        {
            return type switch
            {
                SubscriptionType.Free => 10,
                SubscriptionType.Trial => 100,
                SubscriptionType.Pro => int.MaxValue,
            };
        }
    }
}
