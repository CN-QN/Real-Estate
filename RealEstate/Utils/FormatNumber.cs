using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Utils
{
    public static class FormatNumber 
    {
        public static string Num (decimal value)
        {
            if(value %1 == 0)
            {
                return value.ToString("N0");
            }
            else
            {
                return value.ToString("N2");
            }
        }



    }
}