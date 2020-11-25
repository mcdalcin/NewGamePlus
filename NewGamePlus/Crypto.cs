using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Security.Cryptography;
using System.Text;

namespace NewGamePlus {
    public static class Crypto {
        public static byte[] EncryptAndDigest(string aad, byte[] data) {
            if (data == null) {
                return null;
            }
            byte[] nonce = Utilities.RandomBytes(12);
            byte[] aadBytes = Encoding.UTF8.GetBytes(aad);
            byte[] aadHash = new SHA256Managed().ComputeHash(aadBytes);
            var cipher = new GcmBlockCipher(new AesEngine());
            var cParams = new AeadParameters(new KeyParameter(aadHash, 0, 16), 128, nonce, aadBytes);
            cipher.Init(true, cParams);
            byte[] ciphertext = new byte[cipher.GetOutputSize(data.Length)];
            int retLen = cipher.ProcessBytes(data, 0, data.Length, ciphertext, 0);
            cipher.DoFinal(ciphertext, retLen);

            byte[] output = new byte[nonce.Length + ciphertext.Length];
            Buffer.BlockCopy(nonce, 0, output, 0, nonce.Length);
            Buffer.BlockCopy(ciphertext, 0, output, nonce.Length, ciphertext.Length);
            return output;
        }
    }
}