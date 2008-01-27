using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Persistence
{
	public interface IRecord : INamedItem
	{
		ISchema Schema { get; }
		FieldCollection Fields { get; }
		ConstraintCollection Constraints { get; }
		FieldCollection GetIdentifierFields();
	}
	
	public interface IRecord<Model> : IRecord
	{
		Model GetModel(Tuple tuple);
		Tuple GetTuple(Model model);
	}
}
