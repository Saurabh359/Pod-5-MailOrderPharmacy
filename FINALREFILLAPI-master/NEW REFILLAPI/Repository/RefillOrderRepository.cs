using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RefillApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RefillApi.Repository
{
    public class RefillOrderRepository : IRefillOrderRepository
    {

       static private List<RefillOrder> refill = new List<RefillOrder>()
        {
            new RefillOrder{ Id =1, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=true,Payment=true, SubscriptionId=1},
            new RefillOrder{ Id =2, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=true,Payment=true, SubscriptionId=3},
            new RefillOrder{ Id =3, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=false,Payment=false, SubscriptionId=1}
        };

        // public RefillOrder AdhocRefill( int PolicyId, int MemberId, int SubscriptionId)
        public RefillOrder RefillStatus(int SubscriptionId)
        {

            //  var result = refill.Last(x => (x.SubscriptionId == SubscriptionId) && (x.RefillDelivered) && (x.Payment));

            //  var result = refill.Count(x => (x.SubscriptionId == SubscriptionId) && (!x.RefillDelivered) && (!x.Payment));
            //return result;

            try
            {
                var result = refill.Last(x => (x.SubscriptionId == SubscriptionId) && (x.RefillDelivered) && (x.Payment));
                return result;
            }

            catch (Exception e)
            {
                return null;
            }
            // throw new NotImplementedException();
        }

        public int RefillDues(int id)
        {
            try
            {
                var result = refill.Count(x => (x.SubscriptionId == id) && (!x.RefillDelivered) && (!x.Payment));
                return (result);
            } // throw new NotImplementedException();


            catch(Exception e)
            {
                return (-1);
            }
        }

        // public RefillOrder RefillStatus(int SubscriptionId)
        public RefillOrder AdhocRefill(int PolicyId, int MemberID, int SubscriptionId, string auth)
        {
            // drugId and Location is taken from subscription service with the help of MemberId
         try {
                int DrugId = 1;
                string Location = "Delhi";
                RefillOrder result = new RefillOrder();
                using (var httpClient = new HttpClient())
                {


                    string[] token = auth.Split(" ");

                    
                    var content = new StringContent(JsonConvert.SerializeObject("hello"), Encoding.UTF8, "application/json");
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44393/api/DrugsApi/getDispatchableDrugStock/" + DrugId + "/" + Location)
                {
                        Content = content
                    };
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token[1]);

                    using (var response = httpClient.SendAsync(request).Result)
                    {

                        if (!response.IsSuccessStatusCode)
                        {
                            return null;
                        }

                        var data = response.Content.ReadAsStringAsync().Result;

                        var check = JsonConvert.DeserializeObject<bool>(data);

                        if (check)
                        {
                            result.Id = refill[refill.Count - 1].Id + 1;
                            result.RefillDate = DateTime.Now;
                            result.DrugQuantity = 10;
                            result.RefillDelivered = false;
                            result.Payment = false;
                            result.SubscriptionId = SubscriptionId;
                        }
                        refill.Add(result);
                        return result;
                    }
                }

            }

                    catch (Exception e)
                    {
                        return null;
                    }
        
        }
     
    }
}
