using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StoreManager.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreManager.Data.Validation {

    public class CompanyService<T> : ICompanyService<T> where T : Companies {

        private StoreManagerContext _context;
        private ICompaniesValidator _validator;


        public CompanyService(StoreManagerContext context, ICompaniesValidator validator) {

            _context = context;
            _validator = validator;
        }


        public async Task<bool> Create(T entity) {

            if (_validator.Validate(entity)) {

                await _context.Companies.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<Companies> Get(Guid? id) {

            if (id != null) {

                return await _context.Set<Companies>().Where(q => q.Id == id).Include(q => q.Stores).FirstAsync();
            }

            return null;
        }


        public async Task<Companies> FirstOrDefault(Expression<Func<Companies, bool>> predicate) {

            return await _context.Companies.FirstOrDefaultAsync(predicate);
        }


        public async Task<IEnumerable<Companies>> ListEntities() {

            return await _context.Companies.ToListAsync();
        }


        public async Task Remove(T entity) {

            _context.Companies.Remove(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> Update(T entity) {

            if (_validator.Validate(entity)) {

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
