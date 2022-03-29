using System;
public static class IntEx
    {
		#region Methods (3) 

		// Public Methods (3) 

        /// <summary>
        /// tests if an integer is EVEN
        /// </summary>
        /// <param name="garbage"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool IsEven(this int garbage, int amount)
        {
            return amount % 2 == 0;
        }

		public static bool IsNullOrNotPositive(this int? item)
				{
					bool skip = false;

					if (item == null)
						skip = true;
					else
						if (item < 1)
							skip = true;

					return skip;
				}
        public static int EscapeNullToZero(this int? item)
        {
            return item.HasValue ? item.Value : 0;
        }

        public static int EscapeNullToOne(this int? item)
        {
            return item.HasValue ? item.Value : 1;
        }

        public static long EscapeNullToZero(this long? item)
        {
            return item.HasValue ? item.Value : 0;
        }

        public static long EscapeNullToOne(this long? item)
        {
            return item.HasValue ? item.Value : 1;
        }

        public static int DowncastToInt32(this long item)
        {
            return Convert.ToInt32(item);
        }

        public static short DowncastToInt16(this long item)
        {
            return Convert.ToInt16(item);
        }

        public static long UpcastToInt64(this int item)
        {
            return Convert.ToInt64(item);
        }
        /// <summary>
        /// tests if the given integer is ODD
        /// </summary>
        /// <param name="garbage"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool IsOdd(this int garbage, int amount)
        {
            return !(amount % 2 == 0);
        }

		public static bool IsPositive(this int integer)
		{
			return integer > 0;
		}
		public static bool IsZero(this int integer)
		{
			return integer == 0;
		}
		public static bool IsNegative(this int integer)
		{
			return integer < 0;
		}
        public static int GetRandomNumber(this int minimum, int maximum)
        {
            var random = new Random();
            return random.Next(minimum, maximum + 1);
        }
		#endregion Methods 

        public static string Ordinal(this Int32 number)
        {
            if (number < 0) return number.ToString();
            switch(number % 100)
            {
                case 11:
                case 12:
                case 13:
                    return number + "th";

            }
            switch(number % 10)
            {
                case 1:
                    return number + "st";
                case 2:
                    return number + "nd";
                case 3:
                    return number + "rd";
                default:
                    return number + "th";
            }
        }
    }

