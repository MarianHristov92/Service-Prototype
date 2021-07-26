// ///-----------------------------------------------------------------
// ///   Class:          ACRSettings
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 29.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using Acr.Settings;

namespace LibDataService.SettingsService.Acr.Settings
{
	/// <summary>
	/// This Class represent abstraction of imported plugin Acr.Settings 
	/// see More https://github.com/aritchie/settings
	/// </summary>
	public static class ACRSettings
	{
		static readonly Lazy<ISettings> Instance = new Lazy<ISettings>(() => CreateInstance(), false);

#if __ANDROID__ || __IOS__ || __MAC__
        public static void InitRoaming(string nameSpace) {
            Roaming = CreateInstance(nameSpace);
        }
#endif

		static ISettings _roaming;
		public static ISettings Roaming {
			get {
				if (_roaming == null)
					throw new ArgumentException("You must call InitRoaming");

				return _roaming;
			}
			set { _roaming = value; }
		}



		static ISettings _local;
		public static ISettings Local {
			get { return _local ?? Instance.Value; }
			set { _local = value; }
		}



		public static ISettings CreateInstance(string nameSpace = null)
		{
#if __ANDROID__ || __IOS__ || __MAC__
            return new SettingsImpl(nameSpace);
#elif NET_CORE
            return new AppConfigSettingsImpl();
#else
			throw new ArgumentException("Platform plugin not found.  Did you reference the nuget package in your platform project?");
#endif
		}
	}
}

