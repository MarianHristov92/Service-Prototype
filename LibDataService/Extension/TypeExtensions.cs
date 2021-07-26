// ///-----------------------------------------------------------------
// ///   Class:          Extent
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 06.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using System.Reflection;

namespace System.Extension
{
	public static class TypeExtensions
	{
		public static T Default<T>(this Type type)
		{
			T output = default(T);

			if (type.GetTypeInfo().IsValueType) {
				output = (T)Activator.CreateInstance(type);
			}

			return output;
		}
	}
}
