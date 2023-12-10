using Domain.Interface;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Ropository
{
 
        public class BaseRopository<T> : IBaseRopository<T> where T : class
        {
            protected ApplicationDbContext _context;

            public BaseRopository(ApplicationDbContext context)
            {
                this._context = context;
            }

            public async Task<T> AddAsync(T item)
            {
                _context.Set<T>().Add(item);
                _context.SaveChanges();

                return item;
            }

            public T DeleteAsync(T item)
            {
                _context.Set<T>().Remove(item);
                _context.SaveChanges();

                return item;

            }

            public async Task<T> FindName(Expression<Func<T, bool>> match, string[] includes = null)
            {
                IQueryable<T> query = _context.Set<T>();

                if (includes != null)
                    foreach (var include in includes)
                        query = query.Include(include);


                return await query.SingleOrDefaultAsync(match);
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _context.Set<T>().ToListAsync();
            }

            public async Task<T> GetByIdAsync(int id)
            {
                return await _context.Set<T>().FindAsync(id);
            }


            public T UpdateAsync(T item)
            {
                _context.Update(item);
                _context.SaveChanges();

                return item;
            }
        }
    }

