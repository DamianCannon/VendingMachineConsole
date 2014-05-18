namespace VendingMachineConsole
{
    /// <summary>
    /// Each coin type has a specific value and fixing the value avoids adding constants into the vending machine logic.
    /// </summary>
    public static class CoinTypeExtensions
    {
        public static decimal Value(this CoinTypes myType)
        {
            switch (myType)
            {
                case CoinTypes.OnePound:
                    return 1.00M;
                case CoinTypes.FiftyPence:
                    return 0.50M;
                case CoinTypes.TwentyPence:
                    return 0.20M;
                case CoinTypes.TenPence:
                    return 0.10M;
                case CoinTypes.FivePence:
                    return 0.05M;
                case CoinTypes.TwoPence:
                    return 0.02M;
                case CoinTypes.OnePence:
                    return 0.01M;
                default:
                    return 0.00M;
            }
        }
    }
}