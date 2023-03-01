using Part2.Crypto;
using Part2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Part2.Controllers
{
    public class BobController : ApiController
    {
        //=============================================================================================================
        //=================================Gets prime number from another party========================================
        //=============================================================================================================
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/GetPrime")]
        public string GetPrime(string id)
        {
            clsPublic.Prime = Convert.ToInt32(id);

            clsPublic.g = -1;

            while (clsPublic.g == -1)
            {
                clsPublic.g = CyclicGroup.findPrimitiveRoot(clsPublic.Prime);
            }
            clsPublic.CyclicGroupNumbers = CyclicGroup.FormCyclicGroup(clsPublic.Prime);

            List<string> cyclic = clsPublic.CyclicGroupNumbers.ConvertAll<string>(x => x.ToString());

            Random R = new Random();
            // Alice will choose the private key a
            int random = R.Next(3, clsPublic.Prime - 1);
            clsPublic.PrivateKeyBob = clsPublic.CyclicGroupNumbers[random]; //Bob Private Key
            clsPublic.PublicKeyBob = CyclicGroup.power(clsPublic.g, clsPublic.PrivateKeyBob, clsPublic.Prime);// Bob Public Key
            //Person.PublicKey = clsPublic.PublicKeyBob;

            return id;
        }

        //=============================================================================================================
        //===============================Gets public key from another party===========================================
        //=============================================================================================================
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/GetPublicKey")]
        public string GetPublicKey(string id)
        {
            string AliceKey = id;
            string BobPublicKey = clsPublic.PublicKeyBob.ToString();
            return BobPublicKey;
        }

        //=============================================================================================================
        //=================================== Get Encoded text from another party =====================================
        //=============================================================================================================
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/UploadEncodeText")]
        public string UploadEncodeText(string encodetext)
        {
            clsCipher.CipherText = encodetext;
            string DecodeText = clsCipher.DecodeTextWithKey();
            return DecodeText;
        }

        //=============================================================================================================
        //=============================================================================================================
        //=============================================================================================================
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/UploadFile")]
        public HttpResponseMessage UploadFile()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/Content/files/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

        //=============================================================================================================
        //=============================================================================================================
        //=============================================================================================================

    }
}
