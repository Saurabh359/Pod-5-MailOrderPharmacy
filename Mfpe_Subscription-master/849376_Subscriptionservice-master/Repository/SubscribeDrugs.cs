using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SubscriptionService.Models;
using SubscriptionService.Controllers;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;

namespace SubscriptionService.Repository
{
    public class SubscribeDrugs : ISubscribeDrugs
    {
        static List<SubscriptionDetails> details = new List<SubscriptionDetails>() {
               new SubscriptionDetails{Id=1, MemberId=1, MemberLocation="Haldwani", PrescriptionId=2, RefillOccurrence="Weekly", SubscriptionDate=Convert.ToDateTime("2020-11-24 12:12:00 PM"), Status=true },
               new SubscriptionDetails{Id=2, MemberId=2, MemberLocation="Haldwani", PrescriptionId=3, RefillOccurrence="Weekly", SubscriptionDate=Convert.ToDateTime("2020-11-24 12:12:00 PM"), Status=true },
               new SubscriptionDetails{Id=3, MemberId=1, MemberLocation="Haldwani", PrescriptionId=2, RefillOccurrence="Weekly", SubscriptionDate=Convert.ToDateTime("2020-11-24 12:12:00 PM"), Status=true },
               new SubscriptionDetails{Id=4, MemberId=3, MemberLocation="Haldwani", PrescriptionId=3, RefillOccurrence="Monthly", SubscriptionDate=Convert.ToDateTime("2020-11-24 12:12:00 PM"), Status=true },
              };
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SubscribeController));
        
        public SubscriptionDetails PostSubscription(PrescriptionDetails prescription, string PolicyDetails, int Member_Id, string auth)
        {
            _log4net.Info("DruApi is being called to check for the availability of the DrugName= "+prescription.DrugName);
            
            List<LocationWiseDrug> location = new List<LocationWiseDrug>();
              var drugs = "";
              var query = prescription.DrugName;
              
              string[] token=auth.Split(" ");
              
              HttpClient client = new HttpClient();
              HttpResponseMessage result=null;
              
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44393/api/DrugsApi/searchDrugsByName/" + query);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token[1]);

                result = client.SendAsync(request).Result;

                if (!result.IsSuccessStatusCode)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _log4net.Error("Exception occured in calling Drug Api" + nameof(SubscribeDrugs) + " and error is" + ex.Message);

                return null;
            }
            drugs = result.Content.ReadAsStringAsync().Result;
            location = JsonConvert.DeserializeObject<List<LocationWiseDrug>>(drugs);
            if (location.Count!=0)
            {
                _log4net.Info(prescription.DrugName+" Drug Available");
                var last = details.Last();
                SubscriptionDetails subscription= new SubscriptionDetails { Id = (details[details.Count-1].Id+1), MemberId = Member_Id, MemberLocation = "Delhi", PrescriptionId = 3, RefillOccurrence = prescription.RefillOccurrence, Status = true, SubscriptionDate = DateTime.Now };
                details.Add(subscription);
                return subscription;
            }
            else
            {
                _log4net.Info(prescription.DrugName+" Drug NotAvailable");
                return new SubscriptionDetails { Id = 0, MemberId = 0, MemberLocation = "", PrescriptionId = 0, RefillOccurrence = "", Status = false, SubscriptionDate = Convert.ToDateTime("2020-12-01 01:01:00 AM") };
            }
        }
        public SubscriptionDetails PostUnSubscription(int Member_Id, int Subscription_Id, string auth)
        {

            // Get the data from refill microservice 
            _log4net.Info("Checking for Subscriptionid= "+Subscription_Id);
            
                SubscriptionDetails result = new SubscriptionDetails();
                var subs = details.Find(p => p.Id == Subscription_Id);
                if (subs != null)
                {
                    _log4net.Info("Interacting with refill microservice for the payment status for subscription id =" + Subscription_Id);

                try
                {

                    using (var httpClient = new HttpClient())
                    {
                        string[] token=auth.Split(" ");
                        
                        var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44365/api/RefillOrders/RefillDues/" + Subscription_Id);
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token[1]);

                        using (var response = httpClient.SendAsync(request).Result)
                        {

                            if (!response.IsSuccessStatusCode)
                            {
                                return result;
                            }

                            var data = response.Content.ReadAsStringAsync().Result;

                            var due = JsonConvert.DeserializeObject<int>(data);

                            result = subs;

                            _log4net.Info("Number of Refills pending for payment" + due + "for subscriptionId" + Subscription_Id);


                            if (due == 0 || due==-1)
                            {
                                var unsubscribe = details.Find(p => p.Id == Subscription_Id);
                                result.Status = false;
                                details.Remove(unsubscribe);
                            }


                        }
                    }
                }
                catch(Exception ex)
                {
                    _log4net.Error(ex.Message);
                    return null;
                }
                    return result;

                }
                return null;
            
        }
    }
}
