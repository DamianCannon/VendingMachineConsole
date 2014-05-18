using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up the initial load of coins to use for change
            var initialCoinLoad = new Dictionary<CoinTypes, int>
                {
                    {CoinTypes.OnePound, 1},
                    {CoinTypes.FiftyPence, 2},
                    {CoinTypes.TwentyPence, 2},
                    {CoinTypes.TenPence, 2},
                    {CoinTypes.FivePence, 1},
                    {CoinTypes.TwoPence, 1},
                    {CoinTypes.OnePence, 2}
                };

            // Initialise the vending machine with these coins
            var machine = new VendingMachine(initialCoinLoad);

            // Buy a can of drink for 75p
            var itemAmount = 0.75M;
            var moneyInserted = 1.00M;
            var changeAmount = ConvertChangeToString(machine.CalculateChange(itemAmount, moneyInserted));
            Console.WriteLine("I've bought a drink for {0} and got {1} in change from my {2}.", itemAmount.ToString("c"), changeAmount, moneyInserted.ToString("c"));

            // Buy another can of drink for £1.22
            itemAmount = 1.22M;
            moneyInserted = 1.35M;
            changeAmount = ConvertChangeToString(machine.CalculateChange(itemAmount, moneyInserted));
            Console.WriteLine("I've bought another drink for {0} and got {1} in change from my {2}.", itemAmount.ToString("c"), changeAmount, moneyInserted.ToString("c"));

            // Buy a cornish pasty for £2.39
            itemAmount = 2.39M;
            moneyInserted = 3.00M;
            changeAmount = ConvertChangeToString(machine.CalculateChange(itemAmount, moneyInserted));
            Console.WriteLine("I've bought a pasty for {0} and got {1} in change from my {2}.", itemAmount.ToString("c"), changeAmount, moneyInserted.ToString("c"));
            
            Console.Read();
        }

        static string ConvertChangeToString(Dictionary<CoinTypes, int> change)
        {
            return change.Sum(amount => amount.Key.Value()*amount.Value).ToString("c");
        }
    }
}
