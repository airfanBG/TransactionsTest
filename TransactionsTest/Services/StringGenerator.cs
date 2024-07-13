using System.Security.Cryptography;

namespace TransactionsTest.Services
{
    public class StringGenerator
    {
        public static string GenerateNonce()
        {
            byte[] nonceBytes = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(nonceBytes);
            }
            string nonceHex = BitConverter.ToString(nonceBytes).Replace("-", "").ToUpper();
            return nonceHex;
        }
    }
}
