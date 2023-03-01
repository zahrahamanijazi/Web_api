using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        private int BlockSize = 128;
        public byte[] encrypted;
        public byte[] decrypted;
        public double[] key;
        public static List<int> CyclicGroupNumbers;
        public static int g=-1;
        public static int Prime;
        public static int q;

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    CyclicGroup cyclicG = new CyclicGroup();
        //    //cyclicG.BBShub_GenerateKey(100);
        //    //cyclicG.Deff_Hellman();
        //    //txtgenerator.AppendText(cyclicG.FindGenerator().ToString());
        //}

        //static byte[] GetBytes(double[] values)
        //{
        //    return values.SelectMany(value => BitConverter.GetBytes(value)).ToArray();
        //}
        //=======================================================================================================
        //==========================================Get index of each element of secret key =====================
        //=======================================================================================================
        private static int[] GetKeyIndexes(double[] key)
        {
            int keyLength = key.Length;
            int[] key_indexes = new int[keyLength];
            for (int k = 0; k < key.Length; k++)
            {
                int cnt = 0;
                for (int l = 0; l < key.Length; l++)
                {
                    if (key[l] < key[k]) cnt++;
                }

                key_indexes[k] = cnt;
            }

            return key_indexes;
        }
        //=======================================================================================================
        //==========================================Call Encryption method by Alice======== =====================
        //=======================================================================================================

        private void Encrypt_Click_1(object sender, EventArgs e)
        {
            string Ciphertext = "";
            Ciphertext = Alice.Encrypt(richTextBox1.Text);
            txtEncrypted.Text = Ciphertext;
        }

        private void Dycript_Click(object sender, EventArgs e)
        {
            string CipherText = richCipherText.Text;
            string output = "";
            int totalChars = CipherText.Length;
            int RowCount = (int)Math.Ceiling((double)totalChars / key.Length);
            //int RowCount = totalChars / key.Length;
            int ColCount = key.Length;
            char[,] colChars = new char[RowCount, ColCount];
            char[,] unsortedColChars = new char[RowCount, ColCount];
            int currentRow, currentColumn, i, j;
            int[] KeyIndexes = GetKeyIndexes(key);
            for (i = 0; i < totalChars; ++i)
            {
                currentRow = i / RowCount;
                currentColumn = i % RowCount;
                colChars[currentColumn, currentRow] = CipherText[i];
            }

            for (i = 0; i < RowCount; ++i)
                for (j = 0; j < ColCount; ++j)
                    unsortedColChars[i, j] = colChars[i, KeyIndexes[j]];

            for (i = 0; i < totalChars; ++i)
            {
                currentRow = i / ColCount;
                currentColumn = i % ColCount;
                output += unsortedColChars[currentRow, currentColumn];
            }

            richTextBox3.Text = output;
            return;

        }
        //==============================================================================================
        //================================ Generating Prime number, g, q  ==============================
        //==============================================================================================
        private void button1_Click_1(object sender, EventArgs e)
        {
            CyclicGroup cyclicGroup = new CyclicGroup();
            while (g == -1)
            {
                Prime = cyclicGroup.FindP();
                g = cyclicGroup.findPrimitiveRoot(Prime);
            }
           
            CyclicGroupNumbers = cyclicGroup.FormCyclicGroup(Prime);
            q = (Prime - 1) / 2;
            // int j = CyclicGroup.g;
            List<string> cyclic = CyclicGroupNumbers.ConvertAll<string>(x => x.ToString());

            CyclicGruopNum.Text = String.Join(", ", cyclic);

            txtgenerator.Text = g.ToString();
            Primetxt.Text = Prime.ToString();
            qTxt.Text = q.ToString();
            //qTxt.Text=CyclicGroup.q


        }
        //==============================================================================================
        //================================Alice and Bob generates their own Key=========================
        //==============================================================================================
        private void btn_keys_Click(object sender, EventArgs e)
        {
            Random R = new Random();
            
            // Alice will choose the private key a
            int random = R.Next(3, Prime - 1);
            Alice.PrivateKey = CyclicGroupNumbers[random]; //Alice Private Key
            Alice.PublicKey = CyclicGroup.power(g, Alice.PrivateKey, Prime);// Alice Public Key
            if (Alice.PublicKey < 0) Alice.PublicKey = Alice.PublicKey + Prime;

            // Bob will choose the private key a
            random = R.Next(3, Prime - 1);
            Bob.PrivateKey = CyclicGroupNumbers[random]; //Bob Private Key
            Bob.PublicKey = CyclicGroup.power(g, Bob.PrivateKey, Prime);// Bob Public Key
            if (Bob.PublicKey < 0) Bob.PublicKey = Bob.PublicKey + Prime;

            //x = Math.Pow(g, a) % P; 

            alicePrivate.Text = Alice.PrivateKey.ToString();
            alicePublic.Text = Alice.PublicKey.ToString();

            bobPrivate.Text = Bob.PrivateKey.ToString();
            bobPublic.Text = Bob.PublicKey.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void DiffieHellman_Click(object sender, EventArgs e)
        {
            Alice.SharedKey =CyclicGroup.power(Bob.Send_PublicKey(), Alice.PrivateKey,Prime);
           
            Bob.SharedKey = CyclicGroup.power(Alice.Send_PublicKey(), Bob.PrivateKey,Prime);

            Alicesharedkey.Text = Alice.SharedKey.ToString();
            bobShared.Text = Bob.SharedKey.ToString();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
        //================================================================================================
        //============== Alice and BoB call BBShub_GenerateKey to generate their Shared key ==============
        //================================================================================================
        private void BBS_Click(object sender, EventArgs e)
        {
            string alice_secret = "";
            string bob_secret = "";
            string alice_secret_key = "[";
            string bob_secret_key = "[";
            int[] AliceSecretKey;
            int[] BobSecretKey;
            AliceSecretKey = Alice.BBShub_GenerateKey(Bob.SharedKey,Prime);
            BobSecretKey = Bob.BBShub_GenerateKey(Alice.SharedKey,Prime);

            Alice.SecretKey = AliceSecretKey;
            Bob.SecretKey = BobSecretKey;

            for (int i = 0; i < AliceSecretKey.Length; i++)
            {
                alice_secret_key += AliceSecretKey[i] + ",";
                bob_secret_key += BobSecretKey[i] + ",";
                if (i == AliceSecretKey.Length - 1)
                {
                    alice_secret_key += AliceSecretKey[i] + "]";
                    bob_secret_key += BobSecretKey[i] + "]";
                }
            }
            secretKeyAlice.Text = alice_secret_key;
            secretkeyBob.Text = bob_secret_key;




        }
        //================================================================================================
        //===============================Call Decryption method by Bob====================================
        //================================================================================================
        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string Plaintext = "";
            Plaintext = Bob.Decrypt(richCipherText.Text);
            richTextBox3.Text = Plaintext;
        }
    }
}
