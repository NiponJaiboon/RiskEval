using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace iSabaya
{
    public class OneTimePassword
    {
        private HMACSHA1 generator = new HMACSHA1();
        private int passwordLength = 6;

        public OneTimePassword()
        {
        }

        public OneTimePassword(int passwordLength)
        {
            this.passwordLength = passwordLength;
        }

        /// <summary>
        /// Generate a string of numbers of length 6 or the length passed to the constructor.
        /// </summary>
        /// <param name="message">string used for generating the number</param>
        /// <returns></returns>
        public virtual string Generate(string message)
        {
            byte[] hashBytes = generator.ComputeHash(Encoding.Unicode.GetBytes(message));
            return hashBytes.GetHashCode().ToString().Substring(0, passwordLength);
        }

        /// <summary>
        /// Generate a string of number.
        /// </summary>
        /// <param name="message">string used for generating the number</param>
        /// <param name="length">length of return string must be between 1 and 10</param>
        /// <returns></returns>
        public virtual string Generate(string message, int length)
        {
            byte[] hashBytes = generator.ComputeHash(Encoding.Unicode.GetBytes(message));
            return hashBytes.GetHashCode().ToString().Substring(0, length);
        }
    }
}
