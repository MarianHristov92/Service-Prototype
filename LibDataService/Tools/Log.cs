// ///-----------------------------------------------------------------
// ///   Class:          Log
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 27.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LibDataService.Tools
{
	/// <summary>
	/// This Class is partial reproduction of Android Log Class.
	/// </summary>
	public static class Log
	{
		private static Dictionary<String, List<String>> _container = new Dictionary<String, List<String>>();

		/// <summary>
		/// This method is reproduction of Android Log.i Function.
		/// given data will be print in console. Other than in Android with out text coloring
		/// Example output "04.11.2016 10:47:35 Cached Data for Key Assets.Example.exam_int is invalid" 
		/// </summary>
		/// <param name="data">Data to show in console</param>
		public static void Timestamp(String data)
		{
			Debug.WriteLine((DateTime.Now).ToLocalTime().ToString() + " " + data);
            //ForwardLog?.Invoke("", new ForwardEventArgs("LibDataService", data));
		}

		/// <summary>
		/// Given data will be print in console.
		/// Example output "Cached Data for Key Assets.Example.exam_int is invalid" 
		/// </summary>
		/// <param name="data">Data to show in console</param>
		private static void Simple(String data)
		{
			Debug.WriteLine(data);
            //ForwardLog?.Invoke("", new ForwardEventArgs("LibDataService", data));
		}

		/// <summary>
		/// This mehtod collecting Data for given type in List and print given data once with mehtod i.
		/// To print whole collection call funtion show( key ) with same type
		/// </summary>
		/// <param name="data">Data to show in console</param>
		/// <param name="type">Idetifier for collection</param>
		public static void AppendToObjectContainer(Type type, String data)
		{
			String key = type.Name;
			List<String> objectLog;
			if (_container.ContainsKey(key)) {
				_container.TryGetValue(key, out objectLog);
			} else {
				objectLog = new List<string>();
			}
			objectLog.Add(data);
			_container.Remove(key);
			_container.Add(key, objectLog);
			Timestamp(data);
		}

		/// <summary>
		/// print all collected data for given type. If collection exist.
		/// output example if collection exist
		/// ********************************* START CacheManagerDictonary*************************************
		/// 04.11.2016 10:48:04 No Data Cached for Key Assets.Example.exam_int
		/// 04.11.2016 10:48:04  will cache Data for Key Assets.Example.exam_int [SettingsDataInt: Data=27]
		/// ...
		/// ********************************* END CacheManagerDictonary*************************************
		/// 
		/// output example if collection not exist
		/// ********************************* START CacheManagerDictonary*************************************
		/// 04.11.2016 10:48:04 No Log Collection found for CacheManagerDictonary
		/// ********************************* END CacheManagerDictonary*************************************
		/// </summary>
		/// <param name="type">Identifier for collection</param>
		public static void Show(Type type)
		{
			String key = type.Name;
			List<String> objectLog;
			Simple("********************************* START " + key + "*************************************");
			if (_container.ContainsKey(key)) {
				_container.TryGetValue(key, out objectLog);
				foreach (String logItem in objectLog) {
					Timestamp(logItem);
				}
			} else {
				Timestamp("No Log Collection found for " + key);
			}
			Simple("********************************* END " + key + "*************************************");
		}

        public static EventHandler<ForwardEventArgs> ForwardLog;

        public class ForwardEventArgs : EventArgs
        {
            public string Source { get; set; }
            public string Message { get; set; }

            public ForwardEventArgs(string source, string message)
            {
                Source = source;
                Message = message;
            }
        }
	}
}
