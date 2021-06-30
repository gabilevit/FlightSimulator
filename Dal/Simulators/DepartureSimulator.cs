using Models;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Timers;

namespace Dal.Simulators
{
    public class DepartureSimulator : IDeparturesimulator
    {
        private string _serverBaseUrl = "http://localhost:56691/Airport";
        private List<string> _cultureList;
        private Timer _timer;

        public void EmitDeparture()
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
                    SendFlight(new FlightHistory { FlightNum = new Guid().ToString(), Contry = _cultureList[i], IsArrival = false, IsDeparture = true, Date = DateTime.Now });
                    break;
                }
            }
        }

        private void SendFlight(FlightHistory flightHistory)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{_serverBaseUrl}/NewFlight");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(flightHistory);
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
            _timer.Elapsed += (s, e) => EmitDeparture();
            _timer.Start();
        }
    }
}
