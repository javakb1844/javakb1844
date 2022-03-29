public static class DecimalX
	{
		public static bool IsPositive(this decimal numValue)
		{
			return numValue > 0;
		}

        public static decimal EscapeNullToZero(this decimal? item)
        {
            return item.HasValue ? item.Value : 0;
        }
	}
