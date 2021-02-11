﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Week5Projects
{
    public class NetworkingManager
    {
        private string url = "https://raw.githubusercontent.com/RaniaArbash/MAP526-DPS926-Projects/master/Cars.json";

        private HttpClient client = new HttpClient();


        private string yahoo_url1 = "http://d.yimg.com/autoc.finance.yahoo.com/autoc?query=";
        private string yahoo_url2 = "&region=1&lang=en";
        public NetworkingManager()
        {
        }

        public async Task<IEnumerable<CarClass>> GetAllPosts()
        {
            var response = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<CarClass>>(response);
        }

        public async Task<IEnumerable<CarClass>> getCars()
        {

            var response = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<CarClass>>(response);
        }



        public void PostOneCar()
        {
            var car = new CarClass();
            car.CarModel = "Tesla";
            car.carmodel2 = "Tesla";
            car.year = 2021;
            car.id = 22;

            var content = JsonConvert.SerializeObject(car);
             client.PostAsync(url, new StringContent(content));
        }
    
        public async Task<List<Stock>> searchForStock(string key)
        {
            var url = yahoo_url1 + key + yahoo_url2;
            var response1 = await client.GetAsync(url);
            if (response1.StatusCode == HttpStatusCode.NotFound)
                return new List<Stock>();

            var responseString = await response1.Content.ReadAsStringAsync();
             
            var dic = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(responseString);
            var array = dic.ElementAt(0).Value.ElementAt(1).Value;
            var list = JsonConvert.DeserializeObject<List<Stock>>(array.ToString());

            return list;
        }


    }
}
