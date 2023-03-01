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

        //private byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        //private int BlockSize = 128;
        public byte[] encrypted;
        public byte[] decrypted;
        public double[] key;
 
        //=======================================================================================================
        //=======================================================================================================
        //=======================================================================================================
        public ActionResult Index()
        {
            CyclicGroup cyclicGroup = new CyclicGroup();
            //Person Alice = new Person();
            if (clsPublic.Prime == -1)
            {
                while (clsPublic.g == -1)
                {
                    clsPublic.Prime = CyclicGroup.FindP();
                    //clsPublic.Prime = 29569;
                    clsPublic.g = CyclicGroup.findPrimitiveRoot(clsPublic.Prime);
                }

                //-----------------------------------------------------------------
                //--------------------- Send Prime To Bob -------------------------
                //-----------------------------------------------------------------
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44300/");
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

            // int j = CyclicGroup.g;
            List<string> cyclic = clsPublic.CyclicGroupNumbers.ConvertAll<string>(x => x.ToString());

            //CyclicGruopNum.Text = String.Join(", ", cyclic);

            //txtgenerator.Text = g.ToString();
            //Primetxt.Text = Prime.ToString();
            //qTxt.Text=CyclicGroup.q
            Random R = new Random();

            // Alice will choose the private key a
            int random = R.Next(3, clsPublic.Prime - 1);
            clsPublic.PrivateKeyAlice = clsPublic.CyclicGroupNumbers[random]; //Alice Private Key
            //clsPublic.PrivateKeyAlice = clsPublic.PrivateKeyAlice;
            //clsPublic.PublicKeyAlice = Convert.ToInt32(Math.Pow(clsPublic.g, clsPublic.PrivateKeyAlice)) % clsPublic.Prime;
            //if (clsPublic.PublicKeyAlice < 0) clsPublic.PublicKeyAlice = clsPublic.PublicKeyAlice + clsPublic.Prime;
            clsPublic.PublicKeyAlice = CyclicGroup.power(clsPublic.g, clsPublic.PrivateKeyAlice, clsPublic.Prime);// Alice Public Key
            //clsPublic.PublicKeyAlice= clsPublic.PublicKeyAlice;
            //Session["AlicePublicKey"] = clsPublic.PublicKeyAlice.ToString();
            return View();
        }

        //=======================================================================================================
        //===================================Exchanging public keys with Api=====================================
        //=======================================================================================================
        [HttpPost]
        public ActionResult ExchangeKey(string mystr)
        {
            //string AlicePublicKey = "123";
            string BobKey = "";
            int BobPublicKey;
            string alice_secret_key = "[";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/");
                var T = client.GetAsync("api/GetPublicKey?id=" + clsPublic.PublicKeyAlice);
                T.Wait();
                if (T.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var TData = T.Result.Content.ReadAsStringAsync();
                    TData.Wait();
                    BobKey = TData.Result.Substring(1, TData.Result.Length - 2);
                    clsPublic.PublicKeyBob = Convert.ToInt32(BobKey);
                    Session["BobPublicKey"] = BobKey;
                }
            }





            //==================== Apply Diffe Hellman Algorithm================
            clsPublic.SharedKeyAlice = CyclicGroup.power(clsPublic.PublicKeyBob, clsPublic.PrivateKeyAlice, clsPublic.Prime);
            //clsPublic.SharedKeyAlice = Convert.ToInt32(Math.Pow(clsPublic.PublicKeyBob, clsPublic.PrivateKeyAlice)) % clsPublic.Prime;
            //if (clsPublic.SharedKeyAlice < 0) clsPublic.SharedKeyAlice = clsPublic.SharedKeyAlice + clsPublic.Prime;


            //=================== Apply BBSHub==================================
            int p = clsPublic.Prime;
            int q = (p - 1) / 2;
            int n = p * q;
            string alice_secret_key_binary;

            int[] key = new int[5];
            int[] X = new int[5];
            X[0] = clsPublic.SharedKeyAlice;
            key[0] = clsPublic.SharedKeyAlice % 2;

            for (int i = 1; i < key.Length; i++)
            {
                X[i] = Convert.ToInt32(Math.Pow(X[i - 1], 2) % n);
                //X[i] = CyclicGroup.power(X[i - 1], 2, n);
                if (X[i] < 0) X[i] = X[i] + n;

                //key[i] = X[i] % 2;
                
            }


            clsPublic.SecretKeyAlice = X;


            for (int i = 0; i < clsPublic.SecretKeyAlice.Length; i++)
            {
                alice_secret_key += clsPublic.SecretKeyAlice[i] + ",";
             

                if (i == clsPublic.SecretKeyAlice.Length - 1)
                {
                    alice_secret_key += clsPublic.SecretKeyAlice[i] + "]";
                }
            }
            alice_secret_key_binary = "[";
            for (int len=0; len<key.Length;len++)
            {
                alice_secret_key_binary = alice_secret_key_binary+ key[len].ToString() + ",";
                if (len == key.Length - 1)
                {
                    alice_secret_key_binary += alice_secret_key_binary + "]";
                }
            }
            

            Session["SharedKey"] = clsPublic.SharedKeyAlice.ToString();
            Session["SecretKey"] = alice_secret_key;
           // Session["SecretKey"] = alice_secret_key_binary;
            Session["AlicePublicKey"] = clsPublic.PublicKeyAlice.ToString();
            Session["AlicePrivateKey"] = clsPublic.PrivateKeyAlice;
            Session["BobPublicKey"] = BobKey;
            Session["g"] = clsPublic.g;
            Session["Prime"] = clsPublic.Prime;
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
        //============================================  Encrypting Data  =====================================
        //=======================================================================================================
        [HttpPost]
        public ActionResult Encryptfile()
        {
            string FinalCipher = clsCipher.EncodeTextWithKey();
            Session["FinalCipher"] = FinalCipher;


            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/");
                //sending Encrypted data to Bob
                var T = client.GetAsync("api/UploadEncodeText?encodetext=" + FinalCipher);
                T.Wait();
                if (T.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var TData = T.Result.Content.ReadAsStringAsync();
                    TData.Wait();
                    string BobDecodeText = TData.Result.Substring(1, TData.Result.Length - 2);
                    Session["BobDecodeText"] = "I recieved Alice's File";// confirmation from Bob
                }
            }

            return View("Index");
        }
        //=======================================================================================================
        //============================================  Encrypting Message  =====================================
        //=======================================================================================================
        [HttpPost]
        public ActionResult EncryptMessage(string mystr)
        {
            if (mystr != "")
            {
                clsPublic.Message = mystr;
            }
            string FinalCipher = clsCipher.EncodeTextWithKey();
            Session["FinalCipher"] = FinalCipher;


            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/");
                var T = client.GetAsync("api/UploadEncodeText?encodetext=" + FinalCipher);
                T.Wait();
                if (T.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var TData = T.Result.Content.ReadAsStringAsync();
                    TData.Wait();
                    string BobDecodeText = TData.Result.Substring(1, TData.Result.Length - 2);
                    Session["BobDecodeText"] = "I recieved Alice's File";
                }
            }

            return View("Index");
        }
        //=======================================================================================================
        //=======================================================================================================
        //=======================================================================================================
        [HttpPost]
        public ActionResult ShowReceivedMessage(string mystr)
        {
            //string DecodedMessage = clsCipher.DecodeTextWithKey();
            Session["EncodedMessage"] = clsCipher.CipherText;
            string DecodedMessage = clsCipher.DecodeTextWithKey();
            //string path = "C:\\Users\\Sharif\\Desktop\\security\\Assignment1\\Security_Part\\Part2\\UploadedFiles\\DemoReceived.txt";
            //string path_encryptedFile = "C:\\Users\\Sharif\\Desktop\\security\\Assignment1\\Security_Part2\\Part2\\Content\\UploadFiles\\DemoReceived.txt";
            string path_encryptedFile = Server.MapPath("~")+"Content\\UploadFiles\\"+"DemoReceived.txt";
            string path_decryptedFile = Server.MapPath("~")+ "Content\\UploadFiles\\" + "Decrypted.txt";

            using (StreamWriter writer = new StreamWriter(path_encryptedFile, false)) //// true to append data to the file
            {
                //System.IO.File.WriteAllText(path_encryptedFile, String.Empty);
                if (writer.BaseStream != null)
                    writer.BaseStream.Flush();
                writer.WriteLine(clsCipher.CipherText);
            }
            using (StreamWriter writer = new StreamWriter(path_decryptedFile, false)) //// true to append data to the file
            {
                //System.IO.File.WriteAllText(path_decryptedFile, String.Empty);
                if (writer.BaseStream != null)
                    writer.BaseStream.Flush();
                writer.WriteLine(DecodedMessage);
            }

            Session["DecodedMessage"] = DecodedMessage;
            return View("Index");
        }

        //=======================================================================================================
        //=======================================================================================================
        //=======================================================================================================
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