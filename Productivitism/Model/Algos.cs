using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Productivitism.Model
{
    public class Algos
    {
        static public int CalculateTcoins(TimeSpan T, bool GM, bool MM, bool BM)
        {
            double points = T.TotalMinutes;
            
            if (GM) 
            {
                return (int)(points / 2.0);
            }

            if (MM)
            {
                return (int)(points / 1.5);
            }

            if (BM)
            {
                return (int)(points / 1.3);
            }

            return (int)(points / 2);

        }

    }
}
