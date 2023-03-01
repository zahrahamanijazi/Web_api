using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Part2.Controllers
{
    public class CallAliceController : Controller
    {
        // GET: CallBob
        public ActionResult Index()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44360/");
                var T = client.GetAsync("api/GetPublicKey");
                T.Wait();
                if (T.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var TData = T.Result.Content.ReadAsStringAsync();
                    TData.Wait();
                    ViewBag.AliceMessage = TData.Result;
                }
            }

            return View();
        }



    }
}