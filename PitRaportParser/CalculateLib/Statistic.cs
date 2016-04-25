using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitRaportParser.CalculateLib
{
    public static class Statistic
    {
        public static List<string> Changes(IEnumerable<string> setA, IEnumerable<string> setB)
        {
            List < string > toReturn = new List<string>();
            bool flag01 = false;
            foreach (string As in setA)
            {
                flag01 = false;
                foreach (string Bs in setB)
                {
                    if(Bs==As) flag01 = true;
                }
                if(!flag01)
                    toReturn.Add(As);
            }
            
            return toReturn;
        }

        public static int Added(IEnumerable<string> setA, IEnumerable<string> setB)
        {
            var toreturn = Changes(setB, setA);
            return toreturn.Count;
        }

        public static int Deleted(IEnumerable<string> setA, IEnumerable<string> setB)
        {
            var toreturn = Changes(setA, setB);
            return toreturn.Count;
        }

        public static int Modyfy(IEnumerable<string> setA, IEnumerable<string> setB)
        {
            throw new NotImplementedException();
            return -1;
        }
    }
}
