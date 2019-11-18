using StoreManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManager.Data.Validation {

    public class CompanyValidator : ICompaniesValidator{

        private bool _modelState;

        public bool Validate(Companies entity) {

            _modelState = true;

            if (entity == null) {

                _modelState = false;
            }

            if (string.IsNullOrEmpty(entity.Name) || entity.Name.Length > 255) {

                _modelState = false;
            }

            if (entity.OrganizationNumber.ToString().Length < 1 || entity.OrganizationNumber <= 0) {

                _modelState = false;
            }

            return _modelState;
        }
    }
}
