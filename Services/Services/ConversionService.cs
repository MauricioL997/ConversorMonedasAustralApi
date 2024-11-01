using Common.DTO;
using Data.Entities;
using Data.Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ConversionService : IConversionService
    {
        private readonly IConversionRepository _conversionRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IUserRepository _userRepository;

        public ConversionService(
            IConversionRepository conversionRepository,
            ICurrencyRepository currencyRepository,
            ISubscriptionService subscriptionService,
            IUserRepository userRepository)
        {
            _conversionRepository = conversionRepository;
            _currencyRepository = currencyRepository;
            _subscriptionService = subscriptionService;
            _userRepository = userRepository;
        }

        public ConversionDto PerformConversion(int userId, string fromCurrency, string toCurrency, decimal amount)
        {
            // Verificar si el usuario existe
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                throw new Exception("Usuario no encontrado");

            // Verificar el límite de conversiones del usuario
            var limit = _subscriptionService.GetConversionLimit(user.Type);
            var conversions = _conversionRepository.GetConversionsByUserId(userId);
            if (conversions.Count >= limit)
                throw new Exception("Límite de conversiones alcanzado");

            // Obtener tasas de convertibilidad de las monedas
            var fromCurrencyEntity = _currencyRepository.GetCurrencyByCode(fromCurrency);
            var toCurrencyEntity = _currencyRepository.GetCurrencyByCode(toCurrency);

            if (fromCurrencyEntity == null || toCurrencyEntity == null)
                throw new Exception("Moneda no encontrada");

            // Calcular el resultado de la conversión
            decimal result = amount * (toCurrencyEntity.ConvertibilityIndex / fromCurrencyEntity.ConvertibilityIndex);

            // Guardar la conversión en el repositorio y capturar el Id de la conversión creada
            var conversion = new Conversion
            {
                UserId = userId,
                User = user,
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency,
                Amount = amount,
                Result = result,
                Date = DateTime.UtcNow
            };
            int conversionId = _conversionRepository.AddConversion(conversion);

            // Devolver el DTO de conversión
            return new ConversionDto
            {
                ConversionId = conversionId,
                UserId = userId,
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency,
                Amount = amount,
                Result = result,
                Date = conversion.Date
            };
        }

        public List<ConversionDto> GetUserConversions(int userId)
        {
            var conversions = _conversionRepository.GetConversionsByUserId(userId);
            return conversions.Select(c => new ConversionDto
            {
                ConversionId = c.Id,
                UserId = c.UserId,
                FromCurrency = c.FromCurrency,
                ToCurrency = c.ToCurrency,
                Amount = c.Amount,
                Result = c.Result,
                Date = c.Date
            }).ToList();
        }
    }
}
