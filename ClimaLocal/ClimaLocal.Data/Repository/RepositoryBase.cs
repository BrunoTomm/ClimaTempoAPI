using ClimaLocal.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaLocal.Data.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ClimaContext _context;

        public RepositoryBase(ClimaContext context)
        {
            _context = context;
        }

        public void Adicionar(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            using (_context)
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }
        }
    }

}
