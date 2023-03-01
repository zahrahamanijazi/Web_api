using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class Alice
    {
        public static int PrivateKey;
        public static int PublicKey;
        public static int SharedKey;
        public static int[] SecretKey;
        public static int[] Y = new int[5];

        public static int Send_PublicKey()
        {
            return PublicKey;
        }
        public void SetKeys()
        {
            
        }
        //==============================================================================================
        //========================Implementing BBS pseudo-random number generator ======================
        //==============================================================================================
        public static int[] BBShub_GenerateKey(int seed, int p)
        {
            int q = (p-1)/2;
            int n = p * q;

            int x0 = seed;
            int[] key = new int[5];
            int[] X = new int[5];
            

            X[0] = seed;
            key[0] = seed % 2;

            for (int i = 1; i < key.Length; i++)
            {
                
                X[i] = CyclicGroup.power((X[i - 1]), 2, n);
                if (X[i] < 0) X[i] = X[i] + n;


                key[i] = X[i] % 2;
            }
            return X;
        }
        //==============================================================================================
        //========================Returns the secret key indexes in an array in order===================
        //==============================================================================================
        private static int[] GetKeyIndexes(int[] key)
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
        //==============================================================================================
        //================================Encrypting with KES algorithm ================================
        //==============================================================================================
        public static string Encrypt(string plainText)
        {
            while (true)
            {
                //CyclicGroup CG = new CyclicGroup();
                //key = CG.Deff_Hellman();
                //string plainText = richTextBox1.Text;
                plainText = (plainText.Length % SecretKey.Length == 0) ? plainText : plainText.PadRight(plainText.Length - (plainText.Length % SecretKey.Length) + SecretKey.Length, '*');
                string output = "";
                string FinalCipher = "";
                int totalChars = plainText.Length;
                int ColCount = SecretKey.Length;
                int RowCount = (int)Math.Ceiling((double)totalChars / ColCount);
                char[,] colChars = new char[ColCount, RowCount];
                char[,] sortedColChars = new char[ColCount, RowCount];
                int currentRow, currentColumn, i, j;
                int[] KeyIndexes = GetKeyIndexes(SecretKey);

                for (i = 0; i < totalChars; ++i)
                {
                    currentRow = i / ColCount;
                    currentColumn = i % ColCount;
                    colChars[currentColumn, currentRow] = plainText[i];
                }

                for (i = 0; i < ColCount; ++i)
                    for (j = 0; j < RowCount; ++j)
                        sortedColChars[KeyIndexes[i], j] = colChars[i, j];

                for (i = 0; i < totalChars; ++i)
                {
                    currentRow = i / RowCount;
                    currentColumn = i % RowCount;
                    output += sortedColChars[currentRow, currentColumn];
                }
                output = output.Replace("\0", string.Empty);

                if (plainText.Length <= output.Length)
                {
                    return output;
                }
            }
        }


    }

}

