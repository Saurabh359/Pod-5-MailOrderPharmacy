using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MfpeDrugsApi.Models;
using MfpeDrugsApi.Provider;
using MfpeDrugsApi.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MfpeDrugsApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DrugsApiController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(DrugsApiController));
        IProvider _prov;

        public DrugsApiController(IProvider drugprov)
        {
            _prov = drugprov;

        }
        [HttpGet("{id:int}", Name = "Get")]
        public IActionResult searchDrugsByID(int id)
        {
                _log4net.Info("Drug ID " + id + " Entered For Searching");
                if (id <= 0)
                    return BadRequest();
                return Ok(_prov.searchDrugsByID(id));    
        }


        [HttpGet("{name}")]
        public IActionResult searchDrugsByName(string name)
        {
            _log4net.Info(" Drug Name "+name+" Entered For Searching");
            if (name == null)
                return BadRequest();
            return Ok(_prov.searchDrugsByName(name));
        }

         /*   [HttpPost]
        public bool getDispatchableDrugStock([FromBody] DrugLocation model)
        {
            _log4net.Info("Input Recieved From Another Api");
            DrugRepository obj = new DrugRepository();
            return obj.getDispatchableDrugStock((int)model.Id, (string)model.Location);
        }*/
        [HttpPost("{DrugId}/{Location}")]
        public IActionResult getDispatchableDrugStock([FromRoute] int DrugId, string Location)
        {
            _log4net.Info("Drug Id = "+DrugId+" and Location = "+Location+" Recieved From refill Api");
            if (DrugId <= 0 || Location == null)
                return BadRequest();
            return Ok(_prov.getDispatchableDrugStock(DrugId, Location));
        }

    }
}
