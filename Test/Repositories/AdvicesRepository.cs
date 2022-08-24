 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;  
using Microsoft.Extensions.Caching.Distributed;
namespace Test.Repositories
{

   public class AdvicesRepository : IAdvicesRepository 
{
    private readonly IDistributedCache _cache;
    public AdvicesRepository (IDistributedCache cache)
    {
         _cache = cache;
    }

  async Task SetDataToCache(string key, string data)
            {
                if (data != null)
                {
                    var json = data;
                    await _cache.SetAsync(key, Encoding.ASCII.GetBytes(json), new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    });
                }
            }
    

       public async Task<Advices> GetAdvices(string topic, int? amount)
    {
          HttpClient httpClient = new HttpClient(); 
          Advices result = new Advices();
          AdviceDTO advices = new AdviceDTO();

            try
           {
                var key = topic;
                var valoreCachato = await _cache.GetAsync(key);
                if (valoreCachato != null)
                {
                        advices = JsonConvert.DeserializeObject<AdviceDTO>(Encoding.UTF8.GetString(valoreCachato));
                        
                }
                else
                {
                    var response = httpClient.GetAsync($"https://api.adviceslip.com/advice/search/{topic}").Result;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var contentStream = response.Content.ReadAsStringAsync().Result;
                        await SetDataToCache(topic, contentStream);               
                        advices = JsonConvert.DeserializeObject<AdviceDTO>(contentStream);                    
                                                                        
                    }
                }
                    if (advices.total_results > 0)
                        { 
                        var maxResults = amount >= 0 && amount <= advices.slips.Length ? amount : advices.slips.Length ;
                        result.adviceList = advices.slips.Where(x =>Array.IndexOf(advices.slips,x) <  maxResults).Select( x => {
                            while (advices.slips.Length < maxResults)
                            {
                            x = advices.slips.ElementAt(Array.IndexOf(advices.slips,x));
                            }
                            return x.advice;          
                            }).ToArray();
                        }  
                
                
                    
                    return result;     
            } 
            catch(Exception ex)
            {
                throw new Exception ($"errore durante il recupero degli advice per il topic selezionato:  {ex.Message}");
            }   
         
    }

}
}
