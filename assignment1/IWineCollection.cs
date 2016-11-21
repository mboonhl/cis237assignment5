using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    interface IWineCollection
    {
        bool AddNewItem(string id, string description, string pack, decimal price, bool active);

        string[] GetPrintStringsForAllItems();

        string FindById(string id);
    }
}
