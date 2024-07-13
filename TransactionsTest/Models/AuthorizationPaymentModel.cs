using TransactionsTest.Services;

namespace TransactionsTest.Models
{
    public class AuthorizationPaymentModel
    {
        private string _order;

        public AuthorizationPaymentModel(string amount,string mInfo, string privateKeypath)
        {
            AMOUNT = amount;
            M_INFO = mInfo;
            PrivateKeyPath = privateKeypath;
            ORDER= OrderIdDescriptor.GetOrerNumber();
        }
        public AuthorizationPaymentModel(string amount, string privateKeypath)
        {
            AMOUNT = amount;
            PrivateKeyPath = privateKeypath;
            ORDER = OrderIdDescriptor.GetOrerNumber();
        }
        public string PrivateKeyPath { get; }
        public string TERMINAL { get; set; } = "V5402167";
        public string TRTYPE { get; set; } = "1";
        public string AMOUNT { get; set; }
        public string CURRENCY { get; set; } = "BGN";
        public string ORDER
        {
            get { return _order; }
            private set { _order = value; }
        }
        public string DESC { get; set; } = "Такса";
        public string MERCHANT { get; set; } = "V5402167";
        public string MERCH_NAME { get; set; } = "ВТУ";
        public string ADDENDUM { get; set; } = "AD,TD";
        public string AD_CUST_BOR_ORDER_ID
        {
            get
            {
                return string.Format($"{_order}@{_order}");
            }
        }
        public string TIMESTAMP { get; set; } = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        public string M_INFO { get; set; }
        public string NONCE { get; set; } = StringGenerator.GenerateNonce();
        public string P_SIGN { get; set; }
        public string RFU { get; set; } = "-";
    }
}
