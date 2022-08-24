using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Test.Repositories;
namespace Test.Controllers
{
    [ApiController]
    [Route("GiveMeAdvice")]
    public class AdvicesController : ControllerBase
    {
        
      
        private readonly IAdvicesRepository _advicesRepository;
        public AdvicesController( IAdvicesRepository advicesRepository)
        {
       
             _advicesRepository = advicesRepository;
        }

        
      
        [HttpGet]
        public async Task<ActionResult<Advices>> Get([BindRequired]string topic, int? amount)
        {
              Advices result = new Advices();
              result = await _advicesRepository.GetAdvices(topic, amount);
              if (result.adviceList == null)
                return NotFound();
              return Ok(result);  
        
        }
    }
}
