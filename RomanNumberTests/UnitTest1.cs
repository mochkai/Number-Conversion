using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberConversion;

namespace RomanNumberTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestNumberToRomanConversion()
        {

            (long, string)[] romanNumbers = {
                (896, "DCCCXCVI"),
                (221, "CCXXI"),
                (39, "XXXIX"),
                (769, "DCCLXIX"),
                (982, "CMLXXXII"),
                (1121, "MCXXI"),
                (1434, "MCDXXXIV"),
                (1055, "MLV"),
                (1203, "MCCIII"),
                (1906, "MCMVI"),
                (2293, "MMCCXCIII"),
                (3117, "MMMCXVII"),
                (2144, "MMCXLIV"),
                (3384, "MMMCCCLXXXIV"),
                (2137, "MMCXXXVII")
            };

            foreach (var romanN in romanNumbers)
            {
                RomanNumber romanObj = new RomanNumber(romanN.Item2);
                string message = String.Format("Expected value for {0} : {1}; Actual Value: {2}", romanN.Item2, romanN.Item1, romanObj.toNumber());

                Assert.AreEqual<long>(romanN.Item1, romanObj.toNumber(), message);
            }
        }

        [TestMethod]
        public void TestRomanToNumberConversion()
        {

            (string, long)[] romanNumbers = {
                ("CCCXLI", 341),
                ("DLXVIII", 568),
                ("DXVII", 517),
                ("DLXXXI", 581),
                ("CDXXXIX", 439),
                ("MCMXII", 1912),
                ("MDCCCXXXIV", 1834),
                ("MCIII", 1103),
                ("MCCCXXVIII", 1328),
                ("MD", 1500),
                ("MMDXXVIII", 2528),
                ("MMXLV", 2045),
                ("MMDCCCIII", 2803),
                ("MMXI", 2011),
                ("MMCMLXXII", 2972)
            };

            foreach (var romanN in romanNumbers)
            {
                RomanNumber romanObj = new RomanNumber(romanN.Item2);
                string message = String.Format("Expected value for {0} : {1}; Actual Value: {2}", romanN.Item2, romanN.Item1, romanObj.toString());

                Assert.AreEqual<string>(romanN.Item1, romanObj.toString(), message);
            }
        }
    }
}
