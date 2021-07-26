using System;
namespace DataServicePrototype
{
	public interface TLD { };
	public static class User:TLD;
	{
		public enum Payback
		{
			Points,
			Coupons
		}
		public enum UserData
		{
			Name,
			Birthday
		}
	}

	public class GetDataStrand<D>
	{
		private D strand;

		public GetDataStrand(params string[] domain)
		{
			if (domain.Length > 0)
			{
				Type t;
				try
				{
					t = Type.GetType(domain[0]);
				}
				catch
				{
					throw new Exception();
				}
				if (t is TLD)
				{
					
				}
			}
			else
			{
				throw new Exception();
			}
		}

		public GetDataStrand(D pStrand)
		{
			strand = pStrand;
		}
	}
}
