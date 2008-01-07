using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Telesophy.Alexandria.Model;
using Telesophy.Alexandria.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public class MediaSetContentDataMap : BaseLinkDataMap<IMediaSet, IMediaItem>
	{
		#region Constructors
		public MediaSetContentDataMap()
		{
			Table = new DataTable("MediaSetContent");
			Table.Columns.Add("MediaSetId", typeof(Guid));
			Table.Columns.Add("MediaItemId", typeof(Guid));
			Table.Constraints.Add(new UniqueConstraint(Table.Columns["MediaSetId"], true));
			Table.Constraints.Add(new UniqueConstraint(Table.Columns["MediaItemId"], true));
		}
		#endregion
		
		#region Protected Methods
		protected override IMediaSet GetParentFromRow(DataRow row)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		protected override IMediaItem GetChildFromRow(DataRow row)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		protected override void FillRowForParent(DataRow row, IMediaSet parent)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		protected override void FillRowForChild(DataRow row, IMediaItem child)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
