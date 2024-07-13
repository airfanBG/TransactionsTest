namespace TransactionsTest.Services
{
    public static class OrderIdDescriptor
    {
        private static int currentNumber;
        public static string GetOrerNumber()
        {
            currentNumber++;
            return currentNumber.ToString("D6");
        }
    }
}
