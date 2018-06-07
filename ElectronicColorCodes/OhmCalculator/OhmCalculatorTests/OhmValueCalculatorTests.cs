using NUnit.Framework;
using OhmCalculator;
using System;
using System.Collections;
using System.Linq;

namespace ElectronCalculatorTests
{
    [TestFixture()]
    public abstract class IOhnValueCalculatorTests
    {
        protected abstract IOhmValueCalculator CreateInstance();

        [Test()]
        public virtual void CalculatorTest(string bandA, string bandB, string bandC, string bandD, int ohms)
        {
            IOhmValueCalculator instance = CreateInstance(); 
        }
    }

    //For example, a resistor with bands of yellow, violet, red, 
    //and gold has first digit 4 (yellow in table below), second digit 7 (violet), 
    //followed by 2 (red) zeroes: 4700 ohms.Gold signifies that the tolerance is ±5%, so that the resistance could be between 4465 and 4935 ohms.


    [TestFixture()]
    public class OhnValueCalculatorTest : IOhnValueCalculatorTests
    {
        protected override IOhmValueCalculator CreateInstance()
        {
            return new OhmValueCalculator();
        }

        [TestCase("violet", "black", "pink", "black", 0)]
        [TestCase("white", "gray", "brown", "gold", 980)]
        [TestCase("red", "red", "red", "red", 2200)]
        [TestCase("Green", "Green", "Green", "gold", 5500000)]
        [TestCase("black", "black", "black", "black", 0)]
        [TestCase("yellow", "violet", "red", "gold", 4700)]
        public override void CalculatorTest(string bandA, string bandB, string bandC, string bandD, int ohms)
        {
            IOhmValueCalculator instance = CreateInstance();
            int sutOhms = instance.CalculateOhmValue(bandA, bandB, bandC, bandD);
            Assert.AreEqual(sutOhms, ohms);
        }


        [Test, Sequential]
        public void BandAColorCodeTableLookupTest([ValueSource(typeof(OhnValueCalculatorTestData), "Colors")] ColorCodes x,
                                                   [ValueSource(typeof(OhnValueCalculatorTestData), "BandAColumn")] Bands position,
                                                   [ValueSource(typeof(OhnValueCalculatorTestData), "BandAResults")] double ohm)
        {
            IOhmValueCalculator instance = CreateInstance();

            if (ohm >= 0)
            {
                var sutOhms = instance.TableLookup(x, position);
                Assert.AreEqual(sutOhms, ohm);
            }

            else
            {
                try
                {
                    var sutOhmsInvalid = instance.TableLookup(x, position);
                    Assert.AreEqual(true, false); //ohm < 0 denotes invalid table values, so it should not pass
                }
                catch
                {
                    Assert.AreEqual(true, true);//instead exception should occur
                }
            }
        }

        [Test, Sequential]
        public void BandBColorCodeTableLookupTest([ValueSource(typeof(OhnValueCalculatorTestData), "Colors")] ColorCodes x,
                                                  [ValueSourceAttribute(typeof(OhnValueCalculatorTestData), "BandBColumn")] Bands position,
                                                  [ValueSource(typeof(OhnValueCalculatorTestData), "BandBResults")] double ohm)
        {
            IOhmValueCalculator instance = CreateInstance();

            if (ohm >= 0)
            {
                var sutOhms = instance.TableLookup(x, position);
                Assert.AreEqual(sutOhms, ohm);
            }

            else
            {
                try
                {
                    var sutOhmsInvalid = instance.TableLookup(x, position);
                    Assert.AreEqual(true, false); //ohm < 0 denotes invalid table values, so it should not pass
                }
                catch
                {
                    Assert.AreEqual(true, true);//instead exception should occur
                }
            }
        }

        [Test, Sequential]
        public void BandCColorCodeTableLookupTest([ValueSource(typeof(OhnValueCalculatorTestData), "Colors")] ColorCodes x,
                                                  [ValueSourceAttribute(typeof(OhnValueCalculatorTestData), "BandCColumn")] Bands position,
                                                  [ValueSource(typeof(OhnValueCalculatorTestData), "BandCResults")] double ohm)
        {
            IOhmValueCalculator instance = CreateInstance();
            if (ohm >= 0)
            {
                var sutOhms = instance.TableLookup(x, position);
                Assert.AreEqual(sutOhms, ohm);
            }

            else
            {
                try
                {
                    var sutOhmsInvalid = instance.TableLookup(x, position);
                    Assert.AreEqual(true, false); //ohm < 0 denotes invalid table values, so it should not pass
                }
                catch
                {
                    Assert.AreEqual(true, true);//instead exception should occur
                }
            }
        }

        [Test, Sequential]
        public void BandDColorCodeTableLookupTest([ValueSource(typeof(OhnValueCalculatorTestData), "Colors")] ColorCodes x,
            [ValueSourceAttribute(typeof(OhnValueCalculatorTestData), "BandDPositiveColumn")] Bands position,
            [ValueSource(typeof(OhnValueCalculatorTestData), "BandDResults")] double ohm)
        {
            IOhmValueCalculator instance = CreateInstance();
            if (ohm >= 0)
            {
                var sutOhms = instance.TableLookup(x, position);
                Assert.AreEqual(sutOhms, ohm);
            }

            else
            {
                try
                {
                    var sutOhmsInvalid = instance.TableLookup(x, position);
                    Assert.AreEqual(true, false); //ohm < 0 denotes invalid table values, so it should not pass
                }
                catch
                {
                    Assert.AreEqual(true, true);//instead exception should occur
                }
            }
        }
        ////[TestCase("red", 1, 5)]
        ////[TestCase("red", 2, 100)]
        ////[TestCase("violet", 3, 4)]
        //[Test, Combinatorial]


        public void StringToEnumTest(string color, ColorCodes expectedCC)
        {
            ColorCodes sutColorCode = OhmValueCalculator.StringToEnum<ColorCodes>(color);
            Assert.AreEqual(sutColorCode, expectedCC);
        }

        [Test, TestCaseSource(typeof(OhnValueCalculatorTestData), "TestCases")]
        public ColorCodes StringToEnumTest1(string color)
        {
            ColorCodes sutColorCode = OhmValueCalculator.StringToEnum<ColorCodes>(color);
            return sutColorCode;
        }

        [Ignore("Not working")]
        [Test, Sequential]
        public void FindToleranceTest([ValueSource(typeof(OhnValueCalculatorTestData), "Colors")] ColorCodes x,
            [ValueSource(typeof(OhnValueCalculatorTestData), "BandDPositiveResults")] double positive,
            [ValueSource(typeof(OhnValueCalculatorTestData), "BandDResults")] double ohm)

        {
            //IOhmValueCalculator instance = CreateInstance();
            //double[] tolerance = instance.FindTolerance(x.ToString());
            //Assert.AreEqual(tolerance[0], positive);
            //Assert.AreEqual(tolerance[0], negative);
        }
    }

    public class OhnValueCalculatorTestData
    {
        public const int TableRowCount = 13;

        public static Bands[] BandAColumn()
        {
            return Enumerable.Repeat(Bands.SigDigA, TableRowCount).ToArray();
        }

        public static Bands[] BandBColumn()
        {
            return Enumerable.Repeat(Bands.SigDigB, TableRowCount).ToArray();
        }

        public static Bands[] BandCColumn()
        {
            return Enumerable.Repeat(Bands.MultiplierC, TableRowCount).ToArray();
        }

        public static Bands[] BandDPositiveColumn()
        {
            return Enumerable.Repeat(Bands.ToleranceDPositive, TableRowCount).ToArray();
        }

        public static Bands[] BandDNegativeColumn()
        {
            return Enumerable.Repeat(Bands.ToleranceDNegative, TableRowCount).ToArray();
        }

        public static ColorCodes[] Colors()
        {
            return new ColorCodes[]
            {
                ColorCodes.Pink,
                ColorCodes.Silver,
                ColorCodes.Gold,
                ColorCodes.Black,
                ColorCodes.Brown,
                ColorCodes.Red,
                ColorCodes.Orange,
                ColorCodes.Yellow,
                ColorCodes.Green,
                ColorCodes.Blue,
                ColorCodes.Violet,
                ColorCodes.Gray,
                ColorCodes.White
            };
        }

        public static double[] BandAResults()
        {
            return new double[]
            {
                -1,
                -1,
                -1,
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9
            };
        }

        public static double[] BandBResults()
        {
            return new double[]
            {
                -1,
                -1,
                -1,
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9
            };
        }

        public static double[] BandCResults()
        {
            return new double[]
            {
                0.001,
                0.01,
                0.1,
                1,
                10,
                100,
                1000,
                10000,
                100000,
                1000000,
                10000000,
                100000000,
                1000000000
            };
        }

        public static double[] BandDResults()
        {
            return new double[]
            {
                0,
                10,
                5,
                0,
                1,
                2,
                0,
                5,
                0.5,
                0.25,
                0.1,
                0.05,
                0
            };
        }

        public static double[] BandDNegativeResults()
        {
            return new double[]
            {
                0,
                -10,
                -5,
                0,
                -1,
                -2,
                0,
                -5,
                -0.5,
                -0.25,
                -0.1,
                -0.05,
                0
            };
        }
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData("Green").Returns(ColorCodes.Green);
                yield return new TestCaseData("red").Returns(ColorCodes.Red);
                yield return new TestCaseData("Red").Returns(ColorCodes.Red);
                yield return new TestCaseData("violet").Returns(ColorCodes.Violet);
                yield return new TestCaseData("yellow").Returns(ColorCodes.Yellow);
                yield return new TestCaseData("Yellow").Returns(ColorCodes.Yellow);
                yield return new TestCaseData("yelloW").Returns(ColorCodes.Yellow);
                yield return new TestCaseData("blacK").Returns(ColorCodes.Black);
                yield return new TestCaseData("Pink").Returns(ColorCodes.Pink);
                yield return new TestCaseData("blue").Returns(ColorCodes.Blue);
                yield return new TestCaseData("brown").Returns(ColorCodes.Brown);
                yield return new TestCaseData("gold").Returns(ColorCodes.Gold);
                yield return new TestCaseData("white").Returns(ColorCodes.White);
                yield return new TestCaseData("GREEN").Returns(ColorCodes.Green);
                yield return new TestCaseData("oRAnge").Returns(ColorCodes.Orange);
                yield return new TestCaseData("GraY").Returns(ColorCodes.Gray);
            }
        }
    }
}
