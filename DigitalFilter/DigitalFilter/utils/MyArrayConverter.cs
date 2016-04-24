using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFilter.utils
{
    class MyArrayConverter
    {
        public static T[][] CreateRectangularArray<T>(IList<T[]> arrays)
        {
            // TODO: Validation and special-casing for arrays.Count == 0
            int minorLength = arrays[0].Length;
            T[][] ret = new T[arrays.Count][];
            for (int i = 0; i < arrays.Count; i++)
            {
                ret[i] = new T[arrays[i].Length];
            }
            for (int i = 0; i < arrays.Count; i++)
                for (int j = 0; j < arrays[i].Length;j ++)
            {
                    ret[i][j] = arrays[i][j];

            }
            return ret;
        }
    }
}
