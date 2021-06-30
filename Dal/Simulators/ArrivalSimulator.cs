using Models;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Timers;

namespace Dal.Simulators
{
    public class ArrivalSimulator : IArrivalSimulator
    {
        private string _serverBaseUrl = "http://localhost:56691/Airport";
        private List<string> _cultureList;
        private Timer _timer;

        public ArrivalSimulator()
        {
            _cultureList = new List<string>();
            GetAllContries();
        }

        private void GetAllContries()
        {
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo getCulture in getCultureInfo)
            {
                RegionInfo getRegionInfo = new RegionInfo(getCulture.LCID);
                if (!(_cultureList.Contains(getRegionInfo.EnglishName)))
                {
                    _cultureList.Add(getRegionInfo.EnglishName);
                }
            }
        }

        public void EmitArrival()
        {
            GenerateRandomFlight();
        }


        private void GenerateRandomFlight()
        {
            Random rnd = new Random();
            int num = rnd.Next(0, _cultureList.Count);
            for (int i = 0; i < _cultureList.Count; i++)
            {
                if (i == num)
                {
                    SendFlight(new FlightHistory { FlightNum = new Guid().ToString(), Contry = _cultureList[i], IsArrival = true, IsDeparture = false, Date = DateTime.Now });
                    break;
                }
            }
        }

        private void SendFlight(FlightHistory flight)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{_serverBaseUrl}/NewFlight");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(flight);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        public void Start()
        {
            _timer = new Timer(5000);
            _timer.Elapsed += (s, e) => EmitArrival();
            _timer.Start();
        }
    }
}
