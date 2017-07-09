using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAngle.Models
{
   public struct IndexPair
    {
        public int FirstIndex { get; set; }
        public int SecondIndex { get; set; }

        public IndexPair(int firstIndex, int secondIndex)
        {
            this.FirstIndex = firstIndex;
            this.SecondIndex = secondIndex;
        }
    }
}
