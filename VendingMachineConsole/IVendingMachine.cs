using System.Collections.Generic;

namespace VendingMachineConsole
{
    public interface IVendingMachine
    {
        Dictionary<CoinTypes, int> CalculateChange(decimal costOfSelectedItem, decimal totalOfCoinsSupplied);
    }
}