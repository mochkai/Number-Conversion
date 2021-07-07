using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NumberConversion
{
    public class RomanNumber
    {
        private LinkedList<char> factorMap;
        private Dictionary<string, long> map;
        private string mapChars = "MDCLXVI";
        private Regex validRoman;

        private long currentNumber;

        public RomanNumber()
        {
            this.setConfigs(customFactors:"\u0305\u033F"); // Setting default configs
        }

        public RomanNumber(long number) : this() => this.fromNumber(number);
        public RomanNumber(string roman) : this() => this.fromString(roman);

        public void setConfigs(string customMap = null, string customFactors = null)
        {
            if (customFactors != null)
            {
                this.factorMap = new LinkedList<char>();

                foreach (char f in customFactors)
                    this.factorMap.AddLast(f);
            }

            if (customMap != null)
            {
                this.mapChars = customMap;

                if (!this.setMap(customMap))
                    this.setConfigs(customMap:"MDCLXVI");
            }
            else
            {
                this.setMap(this.mapChars);
            }

            this.refreshRegex();
        }

        public void fromString(string romanNumber)
        {
            this.currentNumber = this.stringToNumber(romanNumber);
        }
        public void fromNumber(long number)
        {
            this.currentNumber = number;
        }

        public bool setMap(string newMap)
        {
            int valMult = (newMap.Length - 1) * (factorMap.Count + 1);
            char[] charMap = newMap.ToCharArray();

            if (valMult % 2 != 0)
                return false;

            Dictionary<string, long> map = new Dictionary<string, long>();

            LinkedListNode<char> factor = factorMap.Last;

            for (int i = 0; i < charMap.Length; i++)
            {
                string overChar = (factor != null) ? factor.Value.ToString() : "";

                map.Add(charMap[i].ToString() + overChar, (long)(Math.Pow(5, (valMult / 2 + valMult % 2)) * Math.Pow(2, valMult / 2)));

                valMult--;

                if (i == charMap.Length - 2 && factor != null)
                {
                    factor = factor.Previous;
                    i = -1;
                }
            }

            this.map = map;

            return true;
        }

        private void refreshRegex()
        {
            LinkedList<string> romanChars = new LinkedList<string>(map.Keys);
            LinkedListNode<string> rChar = romanChars.Last;

            string newRegex = @"$";

            while (rChar != null)
            {
                newRegex = @"(" + rChar.Value + @"){0,3})" + newRegex;
                string _rChar = (rChar.Previous != null) ? rChar.Previous.Value : "";
                string __rChar = (_rChar != "" && rChar.Previous.Previous != null) ? rChar.Previous.Previous.Value : "";

                if (_rChar != "")
                    newRegex = rChar.Value + @"(" + __rChar + @"|" + _rChar + @")|(" + _rChar + @")?" + newRegex;

                newRegex = @"(" + newRegex;

                rChar = rChar.Previous;
                rChar = (rChar != null) ? rChar.Previous : null;
            }

            newRegex = @"^(?=[" + string.Join("", map.Keys) + "])" + newRegex;

            validRoman = new Regex(newRegex);
        }

        private bool validateRomanNumber(string rNum)
        {
            return !validRoman.IsMatch(rNum);
        }

        private bool validateRange(long num)
        {
            long maxVal = map.Values.Max() * 4;

            if (num >= maxVal || num <= 0)
                return true;
            return false;
        }

        public string toString()
        {
            long num = this.currentNumber;

            if (validateRange(num))
                return "error";

            string romanNum = "";
            LinkedList<string> romanChars = new LinkedList<string>(map.Keys);
            LinkedListNode<string> rChar = romanChars.First;
            long maxVal = map.Values.Max() * 4;

            while (rChar != null)
            {
                long n = num / map[rChar.Value];

                //Console.WriteLine("{0} / {1} = {2}",num,factor,n);

                if (n == 9)
                {
                    romanNum += rChar.Value + rChar.Previous.Previous.Value;
                }
                else if (n >= 5)
                {
                    n = n % 5;
                    romanNum += rChar.Previous.Value + repeatChar(rChar.Value, n);
                }
                else if (n == 4)
                {
                    romanNum += rChar.Value + rChar.Previous.Value;
                }
                else if (n > 0)
                {
                    romanNum += repeatChar(rChar.Value, n);
                }

                num = num % map[rChar.Value];

                rChar = rChar.Next;
                rChar = (rChar != null) ? rChar.Next : null;

                if (rChar == null && num > 0)
                    rChar = romanChars.First;
            }

            return romanNum;
        }

        private string repeatChar(string ch, long n)
        {
            string repeatString = "";

            while (n-- > 0)
                repeatString += ch;

            return repeatString;
        }

        private long stringToNumber(string rNum)
        {
            if (validateRomanNumber(rNum)) return -1;

            long intValue = 0;
            char[] romanChars = rNum.ToCharArray();
            List<string> romanStrings = new List<string>();

            for (int i = 0; i < romanChars.Length; i++)
            {
                string rString = romanChars[i].ToString();
                if (i < romanChars.Length - 1 && factorMap.Contains(romanChars[i + 1]))
                {
                    i++;
                    rString += romanChars[i].ToString();
                }

                romanStrings.Add(rString);
            }

            for (int i = romanStrings.Count - 1; i >= 0; i--)
            {
                long curVal = map[romanStrings[i]];
                long nextVal = (i > 0) ? map[romanStrings[i - 1]] : 0;

                if (curVal > nextVal)
                {
                    intValue += curVal - nextVal;
                    i--;
                }
                else
                    intValue += curVal;
            }

            return intValue;
        }

        public long toNumber()
        {
            return this.currentNumber;
        }
    }
}
