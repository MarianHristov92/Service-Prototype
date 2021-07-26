
using System;

namespace LibDataService.DataModels.Description
{
	public interface IDataDescription
	{
        int ID { get; set; }
        Type Source { get; set; }
	}
}