using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class Bob
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
                X[i] = CyclicGroup.power(X[i - 1], 2,n);
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
        //================================Decrypting with KES algorithm ================================
        //==============================================================================================
        public static string Decrypt(string CipherText)
        {
            //string CipherText = richCipherText.Text;
            string output = "";
            int totalChars = CipherText.Length;
            int RowCount = (int)Math.Ceiling((double)totalChars / SecretKey.Length);
            //int RowCount = totalChars / key.Length;
            int ColCount = SecretKey.Length;
            char[,] colChars = new char[RowCount, ColCount];
            char[,] unsortedColChars = new char[RowCount, ColCount];
            int currentRow, currentColumn, i, j;
            int[] KeyIndexes = GetKeyIndexes(SecretKey);

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
            output = output.Replace("*", " ");
            return output;
        }
    }
}

