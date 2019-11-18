using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManager.Models {

    public class StoreViewModel {

        public Stores Store { get; set; }
        public SelectList Companies { get; set; }
        public Guid SelectedCompany { get; set; }
    }
}
