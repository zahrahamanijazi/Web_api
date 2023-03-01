using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Part2.Models;
using Part2.Crypto;
using System.Web.Http.Cors;
using System.Net.Http;
using System.IO;

namespace Part2.Controllers
{
    public class HomeController : Controller
    {
        private byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        private int BlockSize = 128;
        public byte[] encrypted;
        public byte[] decrypted;
        public double[] key;
        //public static List<int> CyclicGroupNumbers;
        //public static int g = -1;
        //public static int Prime=0;
        //public static string BobPublicKey;

        //public static int PrivateKeyBob;
        //public static int PublicKeyBob;
        //public static int PublicKeyAlice;
        //public static int SharedKeyBob;
        //public static int[] SecretKeyBob;

        //public static string Bob_secret_key;

        //=======================================================================================================
        //=======================================================================================================
        //=======================================================================================================
        public ActionResult Index()
        {
            //CyclicGroup cyclicGroup = new CyclicGroup();

            if (clsPublic.Prime == -1)
            {
                clsPublic.g = -1;
                while (clsPublic.g == -1)
                {
                    clsPublic.Prime = CyclicGroup.FindP();
                    //clsPublic.Prime = 29569;
                    clsPublic.g = CyclicGroup.findPrimitiveRoot(clsPublic.Prime);
                }

                    //-----------------------------------------------------------------
                    //--------------------- Send Prime To Alice -------------------------
                    //-----------------------------------------------------------------
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44360/");
                        var T = client.GetAsync("api/GetPrime?id=" + clsPublic.Prime.ToString());
                        T.Wait();
                        if (T.Result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var TData = T.Result.Content.ReadAsStringAsync();
                            TData.Wait();
                            string ReturnPrime = TData.Result.Substring(1, TData.Result.Length - 2);
                            if (ReturnPrime != clsPublic.Prime.ToString())
                                clsPublic.Prime = Convert.ToInt32(ReturnPrime);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            clsPublic.CyclicGroupNumbers = CyclicGroup.FormCyclicGroup(clsPublic.Prime);

            List<string> cyclic = clsPublic.CyclicGroupNumbers.ConvertAll<string>(x => x.ToString());

            Random R = new Random();
            // Alice will choose the private key a
            int random = R.Next(3, clsPublic.Prime - 1);
            clsPublic.PrivateKeyBob = clsPublic.CyclicGroupNumbers[random]; //Bob Private Key
            clsPublic.PublicKeyBob = CyclicGroup.power(clsPublic.g, clsPublic.PrivateKeyBob, clsPublic.Prime);// Bob Public Key
            //clsPublic.PublicKeyBob = Convert.ToInt32(Math.Pow(clsPublic.g, clsPublic.PrivateKeyBob) % clsPublic.Prime);
            //if (clsPublic.PublicKeyBob < 0) clsPublic.PublicKeyBob = clsPublic.PublicKeyBob + clsPublic.Prime;

            return View();
        }

        //=======================================================================================================
        //==============================Exchanging public keys with Api=======================================
        //=======================================================================================================
        [HttpPost]
        public ActionResult ExchangeKey(string mystr)
        {
            //string BobKey = "456";
            string AliceKey = "";
            string Bob_secret_key = "[";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44360/");
                var T = client.GetAsync("api/GetPublicKey?id=" + clsPublic.BobPublicKey);
                T.Wait();
                if (T.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var TData = T.Result.Content.ReadAsStringAsync();
                    TData.Wait();
                    AliceKey = TData.Result.Substring(1, TData.Result.Length - 2);
                    clsPublic.PublicKeyAlice = Convert.ToInt32(AliceKey);
                    //Session["AlicePublicKey"] = AliceKey;
                }
            }

            //====================apply Diffe Hellman Algorithm================
             clsPublic.SharedKeyBob = CyclicGroup.power(clsPublic.PublicKeyAlice, clsPublic.PrivateKeyBob, clsPublic.Prime);
            //clsPublic.SharedKeyBob = Convert.ToInt32(Math.Pow(clsPublic.PublicKeyAlice, clsPublic.PrivateKeyBob)) % clsPublic.Prime;
            //if (clsPublic.SharedKeyBob < 0)
            //{
            //    clsPublic.SharedKeyBob = clsPublic.SharedKeyBob + clsPublic.Prime;
            //}

            //=================== Apply BBSHub==================================
            int p = clsPublic.Prime;
            int q = (p - 1) / 2;
            int n = p * q;

            int[] key = new int[5];
            int[] X = new int[5];
            X[0] = clsPublic.SharedKeyBob;
            key[0] = clsPublic.SharedKeyBob % 2;

            for (int i = 1; i < key.Length; i++)
            {
                X[i] = Convert.ToInt32(Math.Pow(X[i - 1], 2) % n);
                if (X[i] < 0) X[i] = X[i] + n;
                key[i] = X[i] % 2;
            }

            clsPublic.SecretKeyBob = X;
            //Person.SecretKey = X;
            for (int i = 0; i < clsPublic.SecretKeyBob.Length; i++)
            {
                Bob_secret_key += clsPublic.SecretKeyBob[i] + ",";

                if (i == clsPublic.SecretKeyBob.Length - 1)
                {
                    Bob_secret_key += clsPublic.SecretKeyBob[i] + "]";
                }
            }

            Session["SharedKey"] = clsPublic.SharedKeyBob.ToString();
            Session["SecretKey"] = Bob_secret_key;
            Session["BobPublicKey"] = clsPublic.PublicKeyBob.ToString();
            Session["BobPrivateKey"] = clsPublic.PrivateKeyBob;
            Session["AlicePublicKey"] = clsPublic.PublicKeyAlice;
            Session["g"] = clsPublic.g;
            Session["Prime"] = clsPublic.Prime;
            return View("Index");
        }
        //=======================================================================================================
        //============================================  Uploading File===========================================
        //=======================================================================================================

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            string _FileName = "";
            string _path = "";
            try
            {
                if (file.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(file.FileName);
                    _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                }

                using (var sr = new StreamReader(_path))
                {
                    // Read the stream as a string, and write the string to the console.
                    clsPublic.Message = sr.ReadToEnd();
                }

                Session["Uploaded"] = "File Uploaded Successfully!!";
                return View("index");
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View("index");
            }
        }

        //=======================================================================================================
        //============================================  Encrypting Message  =====================================
        //=======================================================================================================
        [HttpPost]
        public ActionResult Encryptfile()
        {
            string FinalCipher = clsCipher.EncodeTextWithKey();
            Session["FinalCipher"] = FinalCipher;


            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44360/");
                var T = client.GetAsync("api/UploadEncodeText?encodetext=" + FinalCipher);
                T.Wait();
                if (T.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var TData = T.Result.Content.ReadAsStringAsync();
                    TData.Wait();
                    string AliceDecodeText = TData.Result.Substring(1, TData.Result.Length - 2);
                    Session["AliceconfirmationFile"] = "I recieved Bob's File";
                }
            }
            
            return View("Index");
        }
        //=======================================================================================================
        //=========================================Encryption Algorithm with KES and secret key ====================
        //=======================================================================================================

        [HttpPost]
        public ActionResult EncryptSendData(string mystr)
        {
            if (mystr != "")
            {
                clsPublic.Message = mystr;
            }
            string FinalCipher = clsCipher.EncodeTextWithKey();
            Session["FinalCipher"] = FinalCipher;


            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44360/");
                var T = client.GetAsync("api/UploadEncodeText?encodetext=" + FinalCipher);
                T.Wait();
                if (T.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var TData = T.Result.Content.ReadAsStringAsync();
                    TData.Wait();
                    string AliceDecodeText = TData.Result.Substring(1, TData.Result.Length - 2);
                    Session["AliceconfirmationMessage"] = "I Recevied Bob's Data";
                }
            }

            return View("Index");
        }
        //=======================================================================================================
        //=======================================Show Receiving Message======================================
        //=======================================================================================================
        [HttpPost]
        public ActionResult ShowReceivedMessage(string mystr)
        {
            Session["EncodedMessage"] = clsCipher.CipherText;
            string DecodedMessage = clsCipher.DecodeTextWithKey();

            string path_encryptedFile = Server.MapPath("~")+"Content\\UploadFiles\\" + "DemoReceived.txt";
            string path_decryptedFile = Server.MapPath("~")+ "Content\\UploadFiles\\" + "Decrypted.txt";

            using (StreamWriter writer = new StreamWriter(path_encryptedFile, false)) //// true to append data to the file
            {
                if (writer.BaseStream != null)
                    writer.BaseStream.Flush();
                writer.WriteLine(clsCipher.CipherText);
            }
            using (StreamWriter writer = new StreamWriter(path_decryptedFile, false)) //// true to append data to the file
            {
                if (writer.BaseStream != null)
                    writer.BaseStream.Flush();
                writer.WriteLine(DecodedMessage);
            }

            Session["DecodedMessage"] = DecodedMessage;
            return View("Index");
        }
        //=======================================================================================================
        //============================================  Show Receiving Data  =====================================
        //=======================================================================================================
        [HttpPost]
        public ActionResult ShowReceivedData()
        {
            //string DecodedMessage = clsCipher.DecodeTextWithKey();
            Session["EncodedMessage"] = clsCipher.CipherText;
            string DecodedMessage = clsCipher.DecodeTextWithKey();
            Session["DecodedMessage"] = DecodedMessage;
            return View("Index");
        }

        //=======================================================================================================
        //=======================================================================================================
        //=======================================================================================================
        public ActionResult About()
        {
            Session["Message"] = "This Website Designed By Zahra Hamani";

            return View();
        }

        //=======================================================================================================
        //=======================================================================================================
        //=======================================================================================================

    }
}