using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace PulltoRefresh
{
    public class CitiesManager
    {
    
        static readonly Lazy<CitiesManager> laxy = new Lazy<CitiesManager>(() => new CitiesManager());
        public static CitiesManager SharedInstance { get => laxy.Value; }


        HttpClient httpClient;
        Dictionary<string, List<string>> cities;

        CitiesManager()
        {
            httpClient = new HttpClient();
        }

        public event EventHandler<CitiesEventArgs> CitiesFeched;
        public event EventHandler<EventArgs> FetchedCitiesFailed;



        public Dictionary<string, List<string>> GetDafultCities(){
            var citiesJson = File.ReadAllText("citites-incomplete.json");
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(citiesJson);
        }


        public void FetchCities(){

            Task.Factory.StartNew(FetchCitiesAsync);


            async Task FetchCitiesAsync()
            {
                try
                {
                    var citiesJson = await httpClient.GetStringAsync("https://dl.dropbox.com/s/0adq8yw6vd5r6bj/cities.json?dl=0");
                    cities = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(citiesJson);

                    if (CitiesFeched == null)
                        return;

                    var e = new CitiesEventArgs(cities);
                    CitiesFeched(this, e);
                }
                catch (Exception ex)
                {
                    if (FetchedCitiesFailed == null)
                        return;

                    FetchedCitiesFailed(this, new EventArgs());
                }
            } 
        }
    }

    public class CitiesEventArgs : EventArgs
    {
        public Dictionary<string, List<string>> Cities { get; private set; }
        public CitiesEventArgs(Dictionary<string, List<string>> cities){
            Cities = cities;
        }

    }
}
