using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace vk1
{
    public interface ICityBikeDataFetcher
    {
        Task<int> GetBikeCountInStation(string stationName);
    }

    class Program
    {
        static void Main(string[] args)
        {
            try  
            { 
                if (args[1] == "offline")
                {
                    OfflineCityBikeDataFetcher fetcher = new OfflineCityBikeDataFetcher();
                    var count = fetcher.GetBikeCountInStation(args[0]);
                    Task.WaitAll(count);
                    Console.WriteLine(count.Result);
                }
                else if (args[1] == "realtime")
                {
                    RealTimeCityBikeDataFetcher fetcher = new RealTimeCityBikeDataFetcher();
                    var count = fetcher.GetBikeCountInStation(args[0]);
                    Task.WaitAll(count);
                    Console.WriteLine(count.Result);
                }
            }
            catch(System.AggregateException nfe)
            {
                
                Console.WriteLine(nfe);
            }
        }
    }

    public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {
        public async Task<int> GetBikeCountInStation(string stationName)
        {
            string resultString = Regex.Match(stationName, @"\d+").Value;

            if (resultString!="")
            {
                System.ArgumentException argEx = new System.ArgumentException("Invalid argument: " + stationName);
                throw argEx;
            }

            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string response = await client.GetStringAsync("https://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
            BikeRentalStationList stations = JsonConvert.DeserializeObject<BikeRentalStationList>(response);

            foreach (Station s in stations.Stations)
            {

                if (s.Name == stationName)
                {
                    return s.BikesAvailable;
                }
            }

            NotFoundException argEx2 = new NotFoundException("Not found: " + stationName);
            throw argEx2;
        }
    }

    class BikeRentalStationList
    {
        [JsonProperty("stations")]
        public List<Station> Stations { get; set; }
    }

    class Station
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    
        [JsonProperty("name")]
        public string Name { get; set; }
    
        [JsonProperty("x")]
        public double X { get; set; }
    
        [JsonProperty("y")]
        public double Y { get; set; }
    
        [JsonProperty("bikesAvailable")]
        public int BikesAvailable { get; set; }
    
        [JsonProperty("spacesAvailable")]
        public int SpacesAvailable { get; set; }
    
        [JsonProperty("allowDropoff")]
        public bool AllowDropoff { get; set; }
    
        [JsonProperty("isFloatingBike")]
        public bool IsFloatingBike { get; set; }
    
        [JsonProperty("stationOn")]
        public bool StationOn { get; set; }
    
 /*        [JsonProperty("networks")]
        public string Networks { get; set; } */
    
        [JsonProperty("realTimeData")]
        public bool RealTimeData { get; set; }
    }

    public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
    {
        public async Task<int> GetBikeCountInStation(string stationName)
        {
            string resultString = Regex.Match(stationName, @"\d+").Value;

            if (resultString!="")
            {
                System.ArgumentException argEx = new System.ArgumentException("Invalid argument: " + stationName);
                throw argEx;
            }

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Kepuli\Desktop\kkkk\serverikurssi\vk1\bikedata.txt");
            foreach (string line in lines)
            {
                string stringg = Regex.Match(line, stationName).Value;

                if (stringg!="")
                {
                    string count = Regex.Match(line, @"\d+").Value;
                    return Int32.Parse(count);
                }
            }

            NotFoundException argEx2 = new NotFoundException("Not found: " + stationName);
            throw argEx2;
        }
    }

    [Serializable()]
    public class NotFoundException : System.Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected NotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
