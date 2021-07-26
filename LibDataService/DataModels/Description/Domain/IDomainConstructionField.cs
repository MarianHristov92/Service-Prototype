// ///-----------------------------------------------------------------
// ///   Class:          IDataContainerSimpleType
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 26.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using LibDataService.DataModels.Container;

namespace LibDataService.DataModels.Description.Domain
{
	public interface IDomainConstructionField<TType> : IDomainConstructionField where TType : IDataContainer
	{

		string GetDomain(TType parameters);

	}

	public interface IDomainConstructionField : IDomainConstruction 
	{

		string GetDomain(IDataContainer parameters);

	}

}