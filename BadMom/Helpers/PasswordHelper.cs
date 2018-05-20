using BadMom.Models.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BadMom.Helpers
{
    public class PasswordHelper
    {
        private int SaltSize;
        HashAlgorithm hashAlg;

        public PasswordHelper(int saltSize)
        {
            SaltSize = saltSize;
            hashAlg = new SHA256CryptoServiceProvider();
        }

        public UserPasswordData CryptPassword(string password)
        {
            string salt = CreateSalt();

            string stringDataToHash = password + salt;
            // Convert the data to hash to an array of Bytes.
            byte[] bytValue = Encoding.UTF8.GetBytes(stringDataToHash);
            // Compute the Hash. This returns an array of Bytes.
            byte[] bytHash = hashAlg.ComputeHash(bytValue);
            // Optionally, represent the hash value as a base64-encoded string, 
            // For example, if you need to display the value or transmit it over a network.
            string base64 = Convert.ToBase64String(bytHash);

            return new UserPasswordData() { passwordHash = base64, salt = salt };
        }

        public bool CheckPassword(string password, UserPasswordData passData)
        {
            password = password + passData.salt;
            var bytValue = Encoding.UTF8.GetBytes(password);
            var bytHash = hashAlg.ComputeHash(bytValue);
            string base64 = Convert.ToBase64String(bytHash);

            return base64.Equals(passData.passwordHash);
        }

        private string CreateSalt()
        {
            // Generate a cryptographic random number using the cryptographic 
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[SaltSize];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

    }
}