using SnackTrackBLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnackTrackBLL.BLL
{
    class Class1
    {
        public Class1()
        {
            // Just a place to test that my models are working correctly.
            GroceryItem testGroceryItem = new GroceryItem();
            string getSPName = testGroceryItem.GetStoredProcedureName(StoredProcedureType.Get);
        }
    }
}
