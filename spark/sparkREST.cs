using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Web;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace spark
{
    public class sparkREST
    {
        public string roomID { get; set; }

        public async Task<HttpResponseMessage> sparkRESTRequest(string endPoint, Dictionary<string, string> dataInput)
        {
            try
            {
                var client = new HttpClient();
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer NTgyNDJlOGUtNjlkNS00YTViLWIwNjUtY2VlYWEwNTc4MzEyNWJmNWYwOWYtOTJk");
                var uri = "https://api.ciscospark.com/v1/" + endPoint;
                byte[] byteData = Encoding.UTF8.GetBytes("{body}");
                var data = new FormUrlEncodedContent(dataInput);
                HttpResponseMessage response = await client.PostAsync(uri, data);
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async void createRoom(sparkREST obj, string roomName)
        {
            var dictt = new Dictionary<string, string>();
            dictt.Add("title", roomName);
            var result = obj.sparkRESTRequest("rooms", dictt).Result;
            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            var text = await result.Content.ReadAsStringAsync();
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(text));
            var data = (RootObject)serializer.ReadObject(ms);
            roomID = data.id;
        }

        public StreamContent createMembership(sparkREST obj, string roomID, string personEmail)
        {
            var dictt = new Dictionary<string, string>();
            dictt.Add("roomId", roomID);
            dictt.Add("personEmail", personEmail);
            var result = obj.sparkRESTRequest("memberships", dictt).Result;
            return null;
        }

        public StreamContent createMessage(sparkREST obj, string roomID)
        {
            var dictt = new Dictionary<string, string>();
            dictt.Add("roomId", roomID);
            var result = obj.sparkRESTRequest("messages", dictt).Result;
            return null;
        }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public bool isLocked { get; set; }
        [DataMember]
        public string lastActivity { get; set; }
        [DataMember]
        public string created { get; set; }
    }

}
