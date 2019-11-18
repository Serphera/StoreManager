using StoreManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManager.Data.Validation {


    public interface ICompaniesValidator {

        bool Validate(Companies entity);
    }
}
