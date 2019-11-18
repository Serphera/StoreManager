using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StoreManager.Models;

namespace StoreManager.Data.Validation {
    public interface ICompanyService<T> where T : Companies {
        Task<bool> Create(T entity);
        Task<Companies> FirstOrDefault(Expression<Func<Companies, bool>> predicate);
        Task<Companies> Get(Guid? id);
        Task<IEnumerable<Companies>> ListEntities();
        Task Remove(T entity);
        Task<bool> Update(T entity);
    }
}