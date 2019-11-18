using StoreManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManager.Data.Validation {

    public class StoreValidator : IStoresValidator {

        private bool _modelState;

        public bool Validate(Stores entity) {

            _modelState = true;

            if (entity.Id == null) {

                _modelState = false;
            }

            if (entity.CompanyId == Guid.Empty) {

                _modelState = false;
            }

            if (string.IsNullOrEmpty(entity.Name) || entity.Name.Length > 100) {

                _modelState = false;
            }

            if (string.IsNullOrEmpty(entity.Address) || entity.Address.Length > 512) {

                _modelState = false;
            }

            if (string.IsNullOrEmpty(entity.Zip) || entity.Zip.Length > 16) {

                _modelState = false;
            }

            if (string.IsNullOrEmpty(entity.Country) || entity.Country.Length > 50) {

                _modelState = false;
            }

            return _modelState;
        }
    }
}
