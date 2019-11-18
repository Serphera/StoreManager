using StoreManager.Data.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreManager.Data {



    public class Geodcoder : IGeocoding {

        // Normally you'd store this somewhere secure
        private string _key;
        private string _endPoint;
        private string _request;


        public Geodcoder(string key) {

            _key = key;
        }

        public void SetEndPoint(string endPoint) {

            _endPoint = endPoint;
        }

        public void SetRequest(string request) {

            _request = request;
        }


        public async Task<object> Geocoding(string searchParam) {

            _request = _request.Replace("SEARCH_STRING", searchParam);
            _request = _request.Replace("KEY_HERE", _key);

            return await GetData();
        }


        public async Task<object> ReverseGeocoding(string lat, string lng) {
            
            _request = _request.Replace("LATITUDE", lat);
            _request = _request.Replace("LONGITUDE", lng);
            _request = _request.Replace("KEY_HERE", _key);

            return await GetData();
        }


        private async Task<object> GetData() {

            var client = new HttpClient();

            var response = await client.GetAsync(_endPoint + _request);

            if (response.IsSuccessStatusCode) {

                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<object>(jsonString);
            }

            return null;
        }
    }


}
