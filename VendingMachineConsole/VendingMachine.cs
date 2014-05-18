using System.Collections.Generic;

namespace VendingMachineConsole
{
    /// <summary>
    /// This is a prototype model of a vending machine that allows for initial loading of coins, to provide change, and a calculation of the coins that can be provided in change.
    /// </summary>
    /// <remarks>
    /// This prototype does not model the items for sale; it is solely concerned with the change calculation based on the coins available.
    /// This prototype also does not model transactions which would be required to capture users entering coins but not completing a purchase.
    /// There is no allowance made for re-use of coins supplied in payment.
    /// There is no validation of the coins supplied in payment but this would be necessary were we to re-use these coins.
    /// </remarks>
    public class VendingMachine : IVendingMachine
    {
        private readonly Dictionary<CoinTypes, int> _coinsAvailableForChange;

        public VendingMachine(Dictionary<CoinTypes, int> initialCoinsAvailableForChange)
        {
            _coinsAvailableForChange = initialCoinsAvailableForChange;
        }

        /// <summary>
        /// Calculates the change that can be returned from the available coins in the vending machine. Returns the least number of coins by working from the largest
        /// coin (one pound) to the smallest (one pence).
        /// </summary>
        /// <remarks>
        /// 1) CalculateChange does not check if we have enough coins in total to cover the change required. If there aren't enough coins then it will return all of the 
        /// available coins and there will be a shortfall. If no coins are initially supplied then a change value of zero will be returned (rather than an exception).
        /// 2) To allow for cancellation of the transaction CalculateChange does not remove the change coins from the class-level collection of available coins.
        /// 3) If less money is supplied than the cost of the item then there is no change but we don't raise an exception.
        /// </remarks>
        /// <param name="costOfSelectedItem"></param>
        /// <param name="totalOfCoinsSupplied"></param>
        /// <returns></returns>
        public Dictionary<CoinTypes, int> CalculateChange(decimal costOfSelectedItem, decimal totalOfCoinsSupplied)
        {
            var changeAvailableToReturn = new Dictionary<CoinTypes, int>();
            var coinsAvailableForChange = new Dictionary<CoinTypes, int>(_coinsAvailableForChange);
            var amountRequiredInChange = totalOfCoinsSupplied - costOfSelectedItem;

            // If no change is required then we don't need to calculate anything
            if (amountRequiredInChange > 0)
            {
                CalculateChangeCoins(amountRequiredInChange, coinsAvailableForChange, changeAvailableToReturn, CoinTypes.OnePound);
            }

            return changeAvailableToReturn;
        }

        /// <summary>
        /// A recursive series is used to exhaust the largest coins first before moving to the next smallest coin when:
        /// 1) The amount left to return in change is less than the current coin size, or
        /// 2) There are no more coins of the current size and so the change will have to consist of smaller coins
        /// </summary>
        /// <param name="remainingAmountRequiredInChange"></param>
        /// <param name="coinsAvailable"></param>
        /// <param name="coinsToReturnInChange"></param>
        /// <param name="currentCoinType"></param>
        private static void CalculateChangeCoins(decimal remainingAmountRequiredInChange, IDictionary<CoinTypes, int> coinsAvailable, IDictionary<CoinTypes, int> coinsToReturnInChange, CoinTypes currentCoinType)
        {
            if (remainingAmountRequiredInChange > 0)
            {
                if ((remainingAmountRequiredInChange >= currentCoinType.Value()) && (coinsAvailable[currentCoinType] > 0))
                {
                    if (coinsToReturnInChange.ContainsKey(currentCoinType))
                    {
                        coinsToReturnInChange[currentCoinType] += 1;
                    }
                    else
                    {
                        coinsToReturnInChange.Add(currentCoinType, 1);
                    }
                    coinsAvailable[currentCoinType] -= 1;
                    remainingAmountRequiredInChange -= currentCoinType.Value();
                    CalculateChangeCoins(remainingAmountRequiredInChange, coinsAvailable, coinsToReturnInChange, currentCoinType);
                }
                else
                {
                    switch (currentCoinType)
                    {
                        case CoinTypes.OnePound:
                            CalculateChangeCoins(remainingAmountRequiredInChange, coinsAvailable, coinsToReturnInChange, CoinTypes.FiftyPence);
                            break;
                        case CoinTypes.FiftyPence:
                            CalculateChangeCoins(remainingAmountRequiredInChange, coinsAvailable, coinsToReturnInChange, CoinTypes.TwentyPence);
                            break;
                        case CoinTypes.TwentyPence:
                            CalculateChangeCoins(remainingAmountRequiredInChange, coinsAvailable, coinsToReturnInChange, CoinTypes.TenPence);
                            break;
                        case CoinTypes.TenPence:
                            CalculateChangeCoins(remainingAmountRequiredInChange, coinsAvailable, coinsToReturnInChange, CoinTypes.FivePence);
                            break;
                        case CoinTypes.FivePence:
                            CalculateChangeCoins(remainingAmountRequiredInChange, coinsAvailable, coinsToReturnInChange, CoinTypes.TwoPence);
                            break;
                        case CoinTypes.TwoPence:
                            CalculateChangeCoins(remainingAmountRequiredInChange, coinsAvailable, coinsToReturnInChange, CoinTypes.OnePence);
                            break;
                    }
                }
            }
        }
    }
}