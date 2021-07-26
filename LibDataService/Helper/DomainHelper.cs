// ///-----------------------------------------------------------------
// ///   Class:          DomainHelper
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 07.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using System.Reflection;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.DataModels.Description.Domain;

namespace LibDataService.Helper
{
	public class DomainHelper
	{
		/// <summary>
		/// This method verify do dataDescriptionInstance implement IDomainConstructionField or IDomainConstruction interfaces
		/// to perform getDomain(IDataContainer data) or getDomain().
		/// In case that no one interface are not implemented, this method throw NotImplementedException
		/// </summary>
		/// <returns>The settings key. Or throw NotImplementedException </returns>
		/// <param name="dataDescriptionInstance">Data description instance. This Object have to implement 
		/// IDomainConstruction or IDomainConstructionField</param>
		/// <param name="data">Instance of generic typeparameter Value. Contains addational identifier</param>
		/// <typeparam name="TParameter">Generic typeparameter.</typeparam>
		public static string GetDomainAsKey<TParameter>(IDataDescription dataDescriptionInstance, TParameter data)
			where TParameter : IDataContainer
		{
			string settingKey = null;
			if (dataDescriptionInstance is IDomainConstructionField) {
				settingKey = (dataDescriptionInstance as IDomainConstructionField).GetDomain(data);
			} else if (dataDescriptionInstance is IDomainConstruction) {
				settingKey = GetDomainAsKey(dataDescriptionInstance);
			} else {
				throw new NotImplementedException("Given instance don't implement IDomainConstruction / IDomainConstructionField ");
			}

			return settingKey;
		}

		/// <summary>
		/// This method verify do dataDescriptionInstance implement IDomainConstruction interface
		/// to perform getDomain().
		/// In case that IDomainConstruction interface is not implemented this method throw NotImplementedException
		/// </summary>
		/// <returns>The settings key. Or throw NotImplementedException </returns>
		/// <param name="dataDescriptionInstance">Data description instance. This Object have to implement 
		/// IDomainConstruction</param>
		public static string GetDomainAsKey(IDataDescription dataDescriptionInstance)
		{
			string settingKey = null;
			if (dataDescriptionInstance is IDomainConstruction) {
				settingKey = (dataDescriptionInstance as IDomainConstruction).GetDomain();
			} else {
				throw new NotImplementedException("Given instance don't implement IDomainConstruction / IDomainConstructionField ");
			}

			return settingKey;
		}

		/// <summary>
		/// This method check is given Type a simple Type
		/// </summary>
		/// <returns><c>true</c>, if simple type, <c>false</c> otherwise.</returns>
		/// <param name="type">Type to check Type</param>
		public static bool IsSimpleType(Type type)
		{
			return type.GetTypeInfo().IsPrimitive || type.GetTypeInfo().IsValueType || (type == typeof(string));
		}

	}
}
