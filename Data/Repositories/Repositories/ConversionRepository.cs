using Data.Context;
using Data.Entities;
using Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories.Repositories
{
    public class ConversionRepository : IConversionRepository
    {
        private readonly ApplicationContext _context;

        public ConversionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public int AddConversion(Conversion conversion)
        {
            _context.Conversion.Add(conversion);
            _context.SaveChanges();
            return conversion.Id;
        }

        public List<Conversion> GetConversionsByUserId(int userId)
        {
            return _context.Conversion.Where(c => c.UserId == userId).ToList();
        }
    }
}
