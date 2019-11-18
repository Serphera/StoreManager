using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManager.Data.Interfaces {
    public interface IGeocoding {

        Task<object> Geocoding(string searchparam);
        Task<object> ReverseGeocoding(string lat, string lng);

        void SetEndPoint(string endPoint);
        void SetRequest(string request);
    }
}
