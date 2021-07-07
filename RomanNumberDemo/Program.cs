using System;
using NumberConversion;

namespace RomanNumberDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            // Initialize a roman number based on the numerical value
            RomanNumber fromNumber = new RomanNumber(3482);

            // Output the number converted into a roman numeral string
            Console.WriteLine(String.Format("Roman numeral string : {0}", fromNumber.toString()));

            // Initialize a roman number based on the roman numeral string
            RomanNumber fromString = new RomanNumber("MMDCCXCVI");

            // Output the number converted into a roman numeral string
            Console.WriteLine(String.Format("Number value of roman numeral : {0}", fromString.toNumber()));

            // Initialize an empty roman number class
            RomanNumber romanNumber = new RomanNumber();

            // Define a custom configuration of roman characters (no longer roman though ;-) ) please note it should contain an even number of characters
            romanNumber.setConfigs(customMap : "ABCDEFG");

            // Add a value to the roman number object
            romanNumber.fromNumber(1248);

            // Output the custom string with no a no longer roman numeral string
            Console.WriteLine(String.Format("Not a roman numeral string : {0}", romanNumber.toString()));

            // Re-use the existing roman number object
            romanNumber.fromString("AABCCECFG");

            // Output the number converted into a no longer roman numeral string | Fun fact: it is the same number as the first one above
            Console.WriteLine(String.Format("Number value of non roman numeral : {0}", romanNumber.toNumber()));

            // Try using a very long number | note: the default configuration allows for numbers up to 3999999999
            romanNumber.setConfigs(customMap: "MDCLXVI"); // Reverting to default mapping
            romanNumber.fromNumber(3917852462);

            // Output the custom string with no a no longer roman numeral string | note : the lines indicate a multiplication of values according to roman notation and can also be configured
            Console.WriteLine(String.Format("Large roman numeral string : {0}", romanNumber.toString()));

            // Changing default factor map to a custom one | note : for the expected effect use the unicode value of cobining diacritical marks (https://unicode-table.com/en/blocks/combining-diacritical-marks/)
            romanNumber.setConfigs(customFactors: "\u0302\u0303"); // these represent a ^ and ~ characters

            // Output the same big string with custom factors
            Console.WriteLine(String.Format("Large roman numeral string with custom factors : {0}", romanNumber.toString()));

            // Extending the max value by adding aditional factors | note : every new factor adds tree aditonal numbers to the max value
            //  - if you need a formula it would be this: newMaxValue = (maxValue * 1000) + 999
            romanNumber.setConfigs(customFactors: "\u0305\u033F\u0302\u0303"); // this will add two extra factors to with a new max value of 3999999999999999

            // Using a huge number allowed by the extra custom factors
            romanNumber.fromNumber(2148523144912182);

            // Output the huge string with custom factors
            Console.WriteLine(String.Format("Huge roman numeral string with custom factors : {0}", romanNumber.toString()));

            /*
                Expected output for the Console Lines

                Roman numeral string : MMMCDLXXXII
                Number value of roman numeral : 2796
                Not a roman numeral string : ACCEDFGGG
                Number value of non roman numeral : 2796
                Large roman numeral string : M̿M̿M̿C̿M̿X̿V̿M̅M̅D̅C̅C̅C̅L̅MMCDLXII
                Large roman numeral string with custom factors : M̃M̃M̃C̃M̃X̃ṼM̂M̂D̂ĈĈĈL̂MMCDLXII
                Huge roman numeral string with custom factors : M̃M̃C̃X̃L̃ṼM̂M̂M̂D̂X̂X̂M̿M̿M̿C̿X̿L̿M̅V̿C̅M̅X̅MMCLXXXII
            */
        }
    }
}
