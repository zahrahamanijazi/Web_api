using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Part2.Models;


namespace Part2.Crypto
{
    public static class clsCipher
    {
        public static string CipherText;
        public static string PlainText;
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
        // KES Encryption Algorithm
        public static string EncodeTextWithKey()
        {
            string Received_Message = clsPublic.Message;
            string FinalCipher = "";
            Received_Message = (Received_Message.Length % clsPublic.SecretKeyAlice.Length == 0) ? Received_Message : Received_Message.PadRight(Received_Message.Length - (Received_Message.Length % clsPublic.SecretKeyAlice.Length) + clsPublic.SecretKeyAlice.Length, '*');
            string output = "";
            int totalChars = Received_Message.Length;
            int ColCount = clsPublic.SecretKeyAlice.Length;
            int RowCount = (int)Math.Ceiling((double)totalChars / ColCount);
            char[,] colChars = new char[ColCount, RowCount];
            char[,] sortedColChars = new char[ColCount, RowCount];
            int currentRow, currentColumn, i, j;
            int[] KeyIndexes = GetKeyIndexes(clsPublic.SecretKeyAlice);

            for (i = 0; i < totalChars; ++i)
            {
                currentRow = i / ColCount;
                currentColumn = i % ColCount;
                colChars[currentColumn, currentRow] = Received_Message[i];
            }

            for (i = 0; i < ColCount; ++i)
                for (j = 0; j < RowCount; ++j)
                    sortedColChars[KeyIndexes[i], j] = colChars[i, j];

            for (i = 0; i < ColCount; ++i)
            {
                if (i % 2 == 0)
                {
                    for (j = 0; j < RowCount; ++j)
                        output += sortedColChars[i, j];
                }
                else
                {
                    for (j = RowCount - 1; j >= 0; --j)
                        output += sortedColChars[i, j];
                }
            }
            if (Received_Message.Length <= output.Length)
            {
                FinalCipher = output;
            }
            return FinalCipher;

            


        }
        // KES Decryption Algorithm
        public static string DecodeTextWithKey()
        {
            int[] secretkey = clsPublic.SecretKeyAlice;
            //string CipherText = richCipherText.Text;
            string output = "";
            int totalChars = CipherText.Length;
            int RowCount = (int)Math.Ceiling((double)totalChars / clsPublic.SecretKeyAlice.Length);
            int ColCount = clsPublic.SecretKeyAlice.Length;
            char[,] colChars = new char[RowCount, ColCount];
            char[,] unsortedColChars = new char[RowCount, ColCount];
            int currentRow, currentColumn, i, j;
            int[] KeyIndexes = GetKeyIndexes(clsPublic.SecretKeyAlice);

            for (i = 0; i < totalChars; ++i)
            {
                currentRow = i / RowCount;
                currentColumn = i % RowCount;
                colChars[currentColumn, currentRow] = CipherText[i];
            }

            for (i = 0; i < RowCount; ++i)
                for (j = 0; j < ColCount; ++j)
                {
                    int finalj = KeyIndexes[j];
                    if (finalj % 2 == 0)
                        unsortedColChars[i, j] = colChars[i, finalj];
                    else
                        unsortedColChars[i, j] = colChars[RowCount - i - 1, finalj];
                }

            for (i = 0; i < totalChars; ++i)
            {
                currentRow = i / ColCount;
                currentColumn = i % ColCount;
                output += unsortedColChars[currentRow, currentColumn];
            }
            output=output.Replace("*", " ");
            return output;

        }
    }

}