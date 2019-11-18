using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StoreManager.Models;

namespace StoreManager.Data.Validation {
    public interface IStoresService<T> where T : Stores {
        Task<bool> Create(StoreViewModel storeVM);
        Task<bool> Create(T entity);
        Task<Stores> FirstOrDefault(Expression<Func<Stores, bool>> predicate);
        Task<Stores> Get(Guid? id);
        Task<IEnumerable<Companies>> GetCompanies();
        Task<IEnumerable<Stores>> ListEntities();
        Task Remove(T entity);
        Task<bool> Update(StoreViewModel storeVM);
        Task<bool> Update(T entity);

        Task<object> Geocoding(string searchparam);
        Task<object> ReverseGeocoding(string lat, string lng);
    }
}