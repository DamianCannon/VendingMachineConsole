using System.Collections.Generic;
using NUnit.Framework;
using VendingMachineConsole;

namespace VendingMachineConsoleTests
{
    [TestFixture]
    public class VendingMachineTests
    {
        private IVendingMachine _vendingMachine;

        [Test]
        public void WhenExactAmountProvidedThenNoChangeIsGiven()
        {
            _vendingMachine = GenerateIntialVendingMachineCalculator(); 
            var changeAmount =_vendingMachine.CalculateChange(1.00M, 1.00M);
            Assert.AreEqual(0, changeAmount.Count);
        }

        [Test]
        public void WhenDiffIsOnePound_AndOnePoundAvailable_ReturnOnePoundChange()
        {
            _vendingMachine = GenerateIntialVendingMachineCalculator();
            var changeAmount = _vendingMachine.CalculateChange(1.00M, 2.00M);
            Assert.AreEqual(1, changeAmount[CoinTypes.OnePound]);
        }

        [Test]
        public void WhenDiffIsFiftyPence_AndFiftyPenceIsAvailable_ReturnOneFiftyPenceChange()
        {
            _vendingMachine = GenerateIntialVendingMachineCalculator();
            var changeAmount = _vendingMachine.CalculateChange(1.00M, 1.50M);
            Assert.AreEqual(1, changeAmount[CoinTypes.FiftyPence]);
        }

        [Test]
        public void WhenDiffIsOnePound_AndOnePoundIsNotAvailable_ReturnTwoFiftyPenceChange()
        {
            _vendingMachine = GenerateIntialVendingMachineCalculator(onePounds: 0, fiftyPences: 2);
            var changeAmount = _vendingMachine.CalculateChange(1.00M, 2.00M);
            Assert.AreEqual(2, changeAmount[CoinTypes.FiftyPence]);
        }

        [Test]
        public void WhenDiffIsOnePound_AndOnePoundAndFiftyPencesAreNotAvailable_ReturnFiveTwentyPenceChange()
        {
            _vendingMachine = GenerateIntialVendingMachineCalculator(onePounds: 0, fiftyPences: 0, twentyPences: 5);
            var changeAmount = _vendingMachine.CalculateChange(1.00M, 2.00M);
            Assert.AreEqual(5, changeAmount[CoinTypes.TwentyPence]);
        }

        [Test]
        public void WhenDiffIsFiftyPence_AndFiftyPencesAndTwentyPencesAreNotAvailable_ReturnFiveTenPenceChange()
        {
            _vendingMachine = GenerateIntialVendingMachineCalculator(fiftyPences: 0, twentyPences: 0, tenPences: 5);
            var changeAmount = _vendingMachine.CalculateChange(1.00M, 1.50M);
            Assert.AreEqual(5, changeAmount[CoinTypes.TenPence]);
        }

        [Test]
        public void WhenDiffIsTwentyPence_AndTwentyPencesAndTenPencesAreNotAvailable_ReturnFourFivePenceChange()
        {
            _vendingMachine = GenerateIntialVendingMachineCalculator(twentyPences: 0, tenPences: 0, fivePences: 4);
            var changeAmount = _vendingMachine.CalculateChange(1.00M, 1.20M);
            Assert.AreEqual(4, changeAmount[CoinTypes.FivePence]);
        }

        [Test]
        public void WhenDiffIsTenPence_AndTenPencesAndFivePencesAreNotAvailable_ReturnFiveTwoPenceChange()
        {
            _vendingMachine = GenerateIntialVendingMachineCalculator(tenPences: 0, fivePences: 0, twoPences: 5);
            var changeAmount = _vendingMachine.CalculateChange(1.00M, 1.10M);
            Assert.AreEqual(5, changeAmount[CoinTypes.TwoPence]);
        }

        [Test]
        public void WhenDiffIsFourPence_AndTwoPencesAreNotAvailable_ReturnFourOnePenceChange()
        {
            _vendingMachine = GenerateIntialVendingMachineCalculator(twoPences: 0, onePences: 4);
            var changeAmount = _vendingMachine.CalculateChange(1.00M, 1.04M);
            Assert.AreEqual(4, changeAmount[CoinTypes.OnePence]);
        }

        [Test]
        public void WhenDiffIsSeventyThreePence_AndAllCoinsAreAvailable_ReturnOneFiftyOneTwentyOneTwoAndOneOneChange()
        {
            _vendingMachine = GenerateIntialVendingMachineCalculator();
            var changeAmount = _vendingMachine.CalculateChange(1.00M, 1.73M);
            Assert.AreEqual(1, changeAmount[CoinTypes.FiftyPence]);
            Assert.AreEqual(1, changeAmount[CoinTypes.TwentyPence]);
            Assert.AreEqual(1, changeAmount[CoinTypes.TwoPence]);
            Assert.AreEqual(1, changeAmount[CoinTypes.OnePence]);
        }

        public VendingMachine GenerateIntialVendingMachineCalculator(int onePounds = 1, int fiftyPences = 1, int twentyPences = 1, int tenPences = 1, int fivePences = 1, int twoPences = 1, int onePences = 1)
        {
            var initialCoins = new Dictionary<CoinTypes, int>
                {
                    {CoinTypes.OnePound, onePounds},
                    {CoinTypes.FiftyPence, fiftyPences},
                    {CoinTypes.TwentyPence, twentyPences},
                    {CoinTypes.TenPence, tenPences},
                    {CoinTypes.FivePence, fivePences},
                    {CoinTypes.TwoPence, twoPences},
                    {CoinTypes.OnePence, onePences}
                };
            return new VendingMachine(initialCoins);
        }
    }
}