using StoreManager.Data.Interfaces;
using StoreManager.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreManager.Data.Validation {


    public class StoresService<T> : IStoresService<T> where T : Stores {

        private StoreManagerContext _context;
        private IStoresValidator _validator;
        private IGeocoding _geocoder;


        public StoresService(StoreManagerContext context, IGeocoding geocoder, IStoresValidator validator) {

            _context = context;
            _validator = validator;
            _geocoder = geocoder;
        }


        public async Task<bool> Create(T entity) {

            if (_validator.Validate(entity)) {

                await _context.Stores.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<Stores> Get(Guid? id) {

            if (id != null) {

                return await _context.Set<Stores>().Where(q => q.Id == id).Include(q => q.Company).FirstAsync();                    
            }

            return null;
        }


        public async Task<Stores> FirstOrDefault(Expression<Func<Stores, bool>> predicate) {

            return await _context.Stores.FirstOrDefaultAsync(predicate);
        }


        public async Task<IEnumerable<Stores>> ListEntities() {

            return await _context.Set<T>().Include(c => c.Company).ToListAsync();
        }


        public async Task Remove(T entity) {

            _context.Stores.Remove(entity);
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


        public Task<bool> Update(StoreViewModel storeVM) {

            storeVM.Store.CompanyId = storeVM.SelectedCompany;

            return Update(((T)storeVM.Store));
        }


        public async Task<IEnumerable<Companies>> GetCompanies() {

            return await _context.Set<Companies>().ToListAsync();
        }


        public async Task<bool> Create(StoreViewModel storeVM) {

            storeVM.Store.CompanyId = storeVM.SelectedCompany;

            return await Create((T)storeVM.Store);
        }



        public async Task<object> Geocoding(string searchParam) {

            _geocoder.SetEndPoint("https://eu1.locationiq.com/v1/search.php");
            _geocoder.SetRequest("?key=KEY_HERE&q=SEARCH_STRING&format=json&addressdetails=1");

            return await _geocoder.Geocoding(searchParam);
        }

        public async Task<object> ReverseGeocoding(string lat, string lng) {

            _geocoder.SetEndPoint("https://eu1.locationiq.com/v1/reverse.php");
            _geocoder.SetRequest("?key=KEY_HERE&lat=LATITUDE&lon=LONGITUDE&format=json");

            return await _geocoder.ReverseGeocoding(lat, lng);
        }
    }
}
