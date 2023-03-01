using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Part2.Controllers
{
    public class CallBobController : Controller
    {
        // GET: CallBob
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadFileToBob()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetPublicKeyFromBob()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/");
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


        [HttpPost]
        public ActionResult UploadFileToBob(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var filePath = Server.MapPath("~/Content/files/" + file.FileName);
                file.SaveAs(filePath);

                /*
                  
                  
 string baseUrl = "http://localhost" + "/api/Payment/AddMedicineOrder";

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("username", "benemanuel");
            parameters.Add("FullName", "benjamin emanuel");
            parameters.Add("Phone", "0800-0800000");
            parameters.Add("CNIC", "1234");
            parameters.Add("address", "an address");
            parameters.Add("Email", "ben@benemanuel.net");
            parameters.Add("dateofbirth", "14/05/1974");
            parameters.Add("Gender", "Male");
            parameters.Add("PaymentMethod", "Credit Card");
            parameters.Add("Title", "Mr");
            parameters.Add("PhramaList", "123");

            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:54169");
            MultipartFormDataContent form = new MultipartFormDataContent();
            HttpContent content = new StringContent("fileToUpload");
            HttpContent DictionaryItems = new FormUrlEncodedContent(parameters);
            form.Add(content, "fileToUpload");
            form.Add(DictionaryItems, "medicineOrder");

            var stream = new FileStream("c:\\TemporyFiles\\test.jpg", FileMode.Open);
            content = new StreamContent(stream);
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "fileToUpload",
                FileName = "AFile.txt"
            };
            form.Add(content);

            HttpResponseMessage response = null;

            try
            {
                response = (client.PostAsync("http://localhost:54169/api/Values/AddMedicineOrder?username=ben", form)).Result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var k = response.Content.ReadAsStringAsync().Result;                  
                  

                 
                 
                 * */




                ViewBag.Message = "File Uploaded Successfully";
            }


            return View();
        }
    }
}