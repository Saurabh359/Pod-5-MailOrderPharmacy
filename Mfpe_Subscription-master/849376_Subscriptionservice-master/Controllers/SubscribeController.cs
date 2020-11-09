using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Repository;
using SubscriptionService.Provider;
using SubscriptionService.Models;
using Microsoft.AspNetCore.Authorization;

namespace SubscriptionService.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
   
    public class SubscribeController : ControllerBase
    {

        ISubscribeProvider Provider;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SubscribeController));
        
        public SubscribeController(ISubscribeProvider subscribeDrugs1)
        {
            Provider = subscribeDrugs1;
        }
        
        [HttpPost("{PolicyDetails}/{MemberId}")]
        public IActionResult PostSubscribe([FromBody] PrescriptionDetails details,[FromRoute] string PolicyDetails, int MemberId, [FromHeader(Name="Authorization")] string auth)
        {
            SubscriptionDetails data = new SubscriptionDetails() ;
            
                if (details == null || PolicyDetails == null || MemberId <= 0 || auth==null)
                {
                    _log4net.Info("PrescriptionDetails is null or " + "MemberId is= " + MemberId + " PolicyDetails is= " + PolicyDetails + " less or equal to  zero");
                    return BadRequest();
                }
                _log4net.Info("Subscription Request is raised from client side  for Drug= " + details.DrugName);

                 data=Provider.Subscribe(details, PolicyDetails, MemberId, auth);
           
            return Ok(data);
        }
     
        [HttpPost("{MemberId}/{SubscriptionId}")]
        public IActionResult PostUnsubscribe([FromRoute]int MemberId,int SubscriptionId, [FromHeader(Name="Authorization")] string auth)
        {
            SubscriptionDetails data = new SubscriptionDetails();
            _log4net.Info("UnSubscribe Request is raised from client side for subscriptionid = " + SubscriptionId);
           
                if (MemberId <= 0 || SubscriptionId <= 0||auth==null)
                {
                    _log4net.Info("MemberId is" + MemberId + "SubscriptionId is " + SubscriptionId + " less or equal to  zero");
                    return BadRequest();
                }
                data = Provider.UnSubscribe(MemberId, SubscriptionId,auth);
           
            return Ok(data);
        }
        
    }
}
