using System;
using System.Globalization;

namespace OhmCalculator
{
    public enum ColorCodes
    {
        Pink = 0,
        Silver = 1,
        Gold = 2,
        Black = 3,
        Brown = 4,
        Red = 5,
        Orange = 6,
        Yellow = 7,
        Green = 8,
        Blue = 9,
        Violet = 10,
        Gray = 11,
        White = 12
    }         

    public enum Bands
    {
      SigDigA = 1,
      SigDigB = 2,
      MultiplierC = 3,
      ToleranceDPositive = 4,
      ToleranceDNegative = 5,

    }

    public class OhmValueCalculator : IOhmValueCalculator
    {

        /// <summary>
        /// Color Codes: -1 signifies an N/A value which is checked for and an exception is thrown
        /// </summary>                                                                  //Multiplier        //+Tolerance            //-Tolerance      
        public double[,] ColorCodeTable ={
        {(double)ColorCodes.Pink,           3015,          -1,              -1,          0.001,                0,                      0},
        {(double)ColorCodes.Silver,           -1,          -1,              -1,        0.01,                   10,                     -10},
        {(double)ColorCodes.Gold,             -1,          -1,              -1,         0.1,                    5,                      -5},
        {(double)ColorCodes.Black,          9005,           0,               0,            1,                  0,                      0},
        {(double)ColorCodes.Brown,          8003,           1,               1,            10,                  1,                      -1},
        {(double)ColorCodes.Red,            3000,           2,               2,            100,                 2,                      -2},
        {(double)ColorCodes.Orange,         2003,           3,               3,            1000,               0,                      0},
        {(double)ColorCodes.Yellow,         1021,           4,               4,            10000,               5,                      -5},
        {(double)ColorCodes.Green,          6018,           5,               5,            100000,            0.5,                    -0.5},
        {(double)ColorCodes.Blue,           5015,           6,               6,            1000000,          0.25,                   -0.25},
        {(double)ColorCodes.Violet,         4005,           7,               7,            10000000,          0.1,                    -0.1}, 
        {(double)ColorCodes.Gray,           7000,           8,               8,            100000000,        0.05,                   -0.05},
        {(double)ColorCodes.White,          1013,           9,               9,            1000000000,         0,                       0}
        };

        public OhmValueCalculator()
        {
        }


        public int CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        {
            ColorCodes bandA = StringToEnum<ColorCodes>(bandAColor);
            ColorCodes bandB = StringToEnum<ColorCodes>(bandBColor);
            ColorCodes bandC = StringToEnum<ColorCodes>(bandCColor);
            ColorCodes bandD = StringToEnum<ColorCodes>(bandDColor);

            double bandAValue = TableLookup(bandA, Bands.SigDigA);
            double bandBValue = TableLookup(bandB, Bands.SigDigB);
            double bandCValue = TableLookup(bandC, Bands.MultiplierC);
            double bandDValue = TableLookup(bandD, Bands.ToleranceDNegative);

            int ohmValue = (int)((bandAValue * 10 + bandBValue) * bandCValue);
            return ohmValue;
        }

        public double TableLookup(ColorCodes bandColor, Bands bandPosition)
        {
            int column = (int)bandPosition + 1;
            double code = ColorCodeTable[(int)bandColor, column];

            if (code < 0 && (bandPosition == Bands.SigDigA || bandPosition == Bands.SigDigB))
                throw new ArgumentException("Invalid color placement");

            return code;
        }

		public static T StringToEnum<T>(string name)
		{
            name = name?.Replace(" ", "");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(string.Format("String cannot be null or empty when converting to {0}", typeof(T).Name ));

            return (T)Enum.Parse(typeof(T), new CultureInfo("en-US").TextInfo.ToTitleCase(name.ToLower()));
		}

        public double[] FindTolerance(string bandDColor)
        {
            double[] tolerance = new double[2];
            ColorCodes bandD = StringToEnum<ColorCodes>(bandDColor);
            tolerance[0] = TableLookup(bandD, Bands.ToleranceDNegative);
            tolerance[1] = TableLookup(bandD, Bands.ToleranceDNegative);
            return tolerance;
        }
    }
}
