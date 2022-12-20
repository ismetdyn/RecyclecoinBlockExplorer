namespace RecycleCoinBlockExplorer.Utility
{
	public static class Converter
	{
		public static DateTime ToDateTime(long unixTime)
		{
			DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTime).ToLocalTime();
			return dtDateTime;
		}
	}
}
