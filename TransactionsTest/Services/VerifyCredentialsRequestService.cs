using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;
using TransactionsTest.Models;

namespace TransactionsTest.Services
{
    public class VerifyCredentialsRequestService
    {
        public string _gate = "https://3dsgate-dev.borica.bg/cgi-bin/cgi_link";
        private readonly AuthorizationPaymentModel model;
        private string _pass = "VTU1963";
       
        public VerifyCredentialsRequestService(AuthorizationPaymentModel model)
        {
            this.model = model;
        }
        
        public byte[] SignMessage()
        {
            string sign =
                model.TERMINAL.Length.ToString() + model.TERMINAL +
                model.TRTYPE.Length.ToString() + model.TRTYPE +
                model.AMOUNT.Length.ToString() + model.AMOUNT +
                model.CURRENCY.Length.ToString() + model.CURRENCY +
                model.ORDER.Length.ToString() + model.ORDER +
                model.TIMESTAMP.Length.ToString() + model.TIMESTAMP +
                model.NONCE.Length.ToString() + model.NONCE +
                model.RFU.Length.ToString() + model.RFU;
            AsymmetricKeyParameter privateKey;
            using (StreamReader reader = File.OpenText(model.PrivateKeyPath))
            {
                PemReader pemReader = new PemReader(reader, new Password(_pass.ToCharArray()));
                privateKey = (AsymmetricKeyParameter)pemReader.ReadObject();
            } 
            RSA rsa = DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters)privateKey);
             
            byte[] messageBytes = Encoding.UTF8.GetBytes(sign);
             
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(messageBytes);
                return rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }
       

    }
    class Password : IPasswordFinder
    {
        private readonly char[] password;

        public Password(char[] password)
        {
            this.password = (char[])password.Clone();
        }

        public char[] GetPassword()
        {
            return (char[])password.Clone();
        }
    }
}
