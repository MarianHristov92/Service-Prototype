using System;
using System.Collections.Generic;
using static DataServicePrototype.Root;
namespace DataServicePrototype
{
	public static class Root
	{
		public static class Asset
		{
			public static class Example
			{
				public static Domain FirstValueExample = new Domain().add("Asset").add("Example").add("FirstValueExample");
				public static Domain SecondValueExample = new Domain().add("Asset").add("Example").add("SecondValueExample");
			}
		}
	}


	public class Domain
	{
		private const Char SEPERATOR = '.';
		private Dictionary<int, String> domains;
		private bool done;
		public Domain()
		{
			domains = new Dictionary<int, string>();
			done = false;
		}

		public Domain add(String domain)
		{
			if (!done)
			{
				int domainLevel = domains.Count;
				domains.Add(domainLevel, domain);
			}
			else
			{
				throw new Exception();
			}
			return this;
		}

		public void finish()
		{
			done = true;
		}

		public String getFullDomain()
		{
			String domain = "";
			foreach (KeyValuePair<int, string> part in domains)
			{
				if (!domain.Equals("")) domain += SEPERATOR;
				domain += part.Value;
			}
			return domain;
		}

		public String getDomainPart(int part = 0)
		{
			String returnValue = "";
			if (part <= domains.Count && part > 0)
			{
				domains.TryGetValue(part, out returnValue);;
			}
			return returnValue;
		}

		public int getDomainDepth()
		{
			return domains.Count;
		}

		public Boolean isPartOf(String domain)
		{
			String thisDomain = this.getFullDomain();
			return (thisDomain.IndexOf(domain, StringComparison.OrdinalIgnoreCase) != -1);
		}

		public Boolean equals(Domain otherDomain)
		{
			Boolean returnValue = false;
			if (domains.Count == otherDomain.getDomainDepth())
			{
				foreach (KeyValuePair<int, String>part in domains)
				{
					String thisValue = part.Value;
					if (otherDomain.getDomainPart(part.Key).Equals(part.Value)) returnValue = true;
					else returnValue = false;
				}
			}
			else
			{
				returnValue = false;
			}
			return returnValue;
		}
	}

	public class Domains
	{
		public void domains()
		{
			Domain domainOne = Asset.Example.FirstValueExample;
			Domain domainTwo = Asset.Example.SecondValueExample;
			Domain domainOneAgain = Asset.Example.FirstValueExample;

			Boolean isEqual = domainOne.equals(domainOneAgain);
			Boolean isNotEqual = domainOne.equals(domainTwo);

			String domainName = domainOne.getFullDomain();
			String domainPart = domainOne.getDomainPart(2);

			Boolean isPartOf = domainOne.isPartOf("Asset.Example");
			Boolean isPartOfPartial = domainOne.isPartOf("Example");
			Boolean isNotPartOf = domainOne.isPartOf("abc");

			DomainAssociation dA = new DomainAssociation(domainOne, new Func<String, Boolean>(Connector.connectorOne));
			dA.getDataFromWebservice("http://www.google.de");
		}

		public DomainAssociation getDa(Domain domain)
		{
			DomainAssociation dA = null;
			switch (domain.getFullDomain())
			{
				case "Asset.Example.FirstValueExample":
					dA = new DomainAssociation(domain, new Func<String, Boolean>(Connector.connectorOne));
					break;
				case "Asset.Example.SecondValueExample":
					dA = new DomainAssociation(domain, new Func<String, Boolean>(Connector.connectorTwo));
					break;
				default:
					throw new Exception();
			}
			return dA;
		}
	}

	public class DomainAssociation
	{
		Domain domain;
		public Func<String, Boolean> getDataFromWebservice;

		public DomainAssociation(Domain pDomain, Func<String, Boolean> pFunc)
		{
			this.domain = pDomain;
			this.getDataFromWebservice = pFunc;
		}
	}

	class Connector
	{
		public static Boolean connectorOne(String a)
		{
			//do Something
			return true;
		}
		public static Boolean connectorTwo(String a)
		{
			return false;
		}
	}
}
