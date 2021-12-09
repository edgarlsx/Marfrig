using Core.DTO;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraGadoController : ControllerBase
    {
        // GET: api/<CompraGadoController>
        [HttpGet]
        public ICollection<GetAllDto> Get(string strAll)
        {
            GetAllDto getAll = new GetAllDto();
            if (!String.IsNullOrEmpty(strAll))
                getAll = JsonConvert.DeserializeObject<GetAllDto>(strAll);

            return GadoServices.GetAllFilters(getAll);
        }

        // POST api/<CompraGadoController>
        [HttpPost]
        public SendAllDto Post(string strAll)
        {
            SendAllDto getAll = new SendAllDto();
            if (!String.IsNullOrEmpty(strAll)) 
                getAll = JsonConvert.DeserializeObject<SendAllDto>(strAll);

            return GadoServices.CreateAll(getAll);
        }

        // PUT api/<CompraGadoController>/5
        [HttpPut]
        public SendAllDto Put(string strAll)
        {
            SendAllDto getAll = new SendAllDto();
            if (!String.IsNullOrEmpty(strAll))
                getAll = JsonConvert.DeserializeObject<SendAllDto>(strAll);

            return GadoServices.CreateAll(getAll);

        }

        // DELETE api/<CompraGadoController>/5
        [HttpDelete]
        public DelAllDto Delete(string strAll)
        {

            DelAllDto getAll = new DelAllDto();
            if (!String.IsNullOrEmpty(strAll))
                JsonConvert.DeserializeObject<DelAllDto>(strAll);

            return GadoServices.DeleteAll(getAll);
        }
    }
}
