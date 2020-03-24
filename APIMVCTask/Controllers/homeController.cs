using APIMVCTask.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace APIMVCTask.Controllers
{
    public class homeController : Controller
    {
        //Hosted web API Service base url  
        string Baseurl = "https://rules.sos.ri.gov";

        public async Task<ActionResult> Index()
        {
            List<Regulation> regulationInfo = new List<Regulation>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource api_get_rules_by_org_id_and_keyword using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("/regulations/api_get_rules_by_org_id_and_keyword/active/108");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var regulationResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the  list  
                    regulationInfo = JsonConvert.DeserializeObject<List<Regulation>>(regulationResponse);

                }
                //returning the employee list to view  
                return View(regulationInfo);
            }
        }
    }
}