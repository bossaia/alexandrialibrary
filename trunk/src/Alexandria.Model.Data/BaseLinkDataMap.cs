#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Telesophy.Alexandria.Model;
using Telesophy.Alexandria.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public abstract class BaseLinkDataMap<Parent, Child> : BaseDataMap
		where Parent: IModel
		where Child: IModel
	{
		#region Constructors
		public BaseLinkDataMap(string tableName, BaseSimpleDataMap<Parent> parentMap, string parentIdColumn, BaseSimpleDataMap<Child> childMap, string childIdColumn) : this(null, tableName, parentMap, parentIdColumn, childMap, childIdColumn)
		{
		}
		
		public BaseLinkDataMap(IPersistenceEngine engine, string tableName, BaseSimpleDataMap<Parent> parentMap, string parentIdColumn, BaseSimpleDataMap<Child> childMap, string childIdColumn) : base(engine)
		{
			Table = new DataTable(tableName);
			Table.Columns.Add(parentIdColumn, typeof(Guid));
			Table.Columns.Add(childIdColumn, typeof(Guid));
			Table.Constraints.Add(new UniqueConstraint(new DataColumn[]{Table.Columns[parentIdColumn], Table.Columns[childIdColumn]}, true));
			
			this.tableName = tableName;
			this.parentMap = parentMap;
			this.parentIdColumn = parentIdColumn;
			this.childMap = childMap;
			this.childIdColumn = childIdColumn;
		}
		#endregion
		
		#region Private Fields
		private string tableName;
		private BaseSimpleDataMap<Parent> parentMap;
		private string parentIdColumn;
		private BaseSimpleDataMap<Child> childMap;
		private string childIdColumn;
		#endregion
		
		#region Protected Properties
		protected BaseSimpleDataMap<Parent> ParentMap
		{
			get { return parentMap; }
		}
		
		protected BaseSimpleDataMap<Child> ChildMap
		{
			get { return childMap; }
		}
		#endregion
		
		#region Protected Methods
		protected virtual void LoadChildren(Parent parent)
		{
			IList<Child> children = ListChildren(parent);
			ParentMap.LoadChildren<Child>(parent, children);
		}
		
		protected virtual void LoadChildren(IList<Parent> parents)
		{
			foreach (Parent parent in parents)
			{
				LoadChildren(parent);
			}
		}
		
		protected virtual Parent GetParentFromRow(DataRow row)
		{
			if (row != null)
			{
				Guid parentId = GetValue<Guid>(row[parentIdColumn]);
				if (parentId != default(Guid))
				{
					return ParentMap.LookupModel(parentId);
				}
			}

			return default(Parent);
		}

		protected virtual Child GetChildFromRow(DataRow row)
		{
			if (row != null)
			{
				Guid childId = GetValue<Guid>(row[childIdColumn]);
				if (childId != default(Guid))
				{
					return ChildMap.LookupModel(childId);
				}
			}

			return default(Child);
		}

		protected virtual void FillRowForParent(DataRow row, Parent parent)
		{
			if (row != null && parent != null)
			{
				row[parentIdColumn] = parent.Id;
			}
		}

		protected virtual void FillRowForChild(DataRow row, Child child)
		{
			if (row != null && child != null)
			{
				row[childIdColumn] = child.Id;
			}
		}
		
		protected virtual void SaveLinks(Parent parent)
		{
			IList<Child> children = GetCurrentChildren(parent);
		
			if (parent != null && children != null && children.Count > 0)
			{
				foreach(Child child in children)
				{
					DataRow row = Table.NewRow();
					
					FillRowForParent(row, parent);
					FillRowForChild(row, child);
					
					Engine.SaveRow(row);
					
					row.Delete();
				}
			}
		}

		protected virtual void DeleteLinks(Parent parent)
		{
			if (parent != null)
			{
				Engine.FillTable(Table, parent.Id);
				Engine.DeleteRows(Table);
			}
		}
		
		protected abstract IList<Child> GetCurrentChildren(Parent parent);
		#endregion
		
		#region Public Methods
		public virtual Parent LookupParentAndChildren(Guid id, bool cascade)
		{
			Parent parent = ParentMap.LookupModel(id);
			
			if (parent != null && cascade)
			{
				LoadChildren(parent);
			}
			
			return parent;
		}
		
		public virtual IList<Parent> ListParents()
		{
			IList<Parent> parents = ParentMap.ListModels();
			
			LoadChildren(parents);
			
			return parents;
		}
		
		public virtual IList<Parent> ListParents(string filter)
		{
			IList<Parent> parents = ParentMap.ListModels(filter);
			
			LoadChildren(parents);
			
			return parents;
		}
		
		public virtual IList<Child> ListChildren(Parent parent)
		{
			IList<Child> list = new List<Child>();

			if (parent != null)
			{
				Engine.FillTable(Table, parent.Id);
				foreach (DataRow row in Table.Rows)
				{
					Guid childId = GetValue<Guid>(row[childIdColumn]);
					if (childId != default(Guid))
					{
						Child child = ChildMap.LookupModel(childId);
						if (child != null)
						{
							list.Add(child);
						}
					}
				}
			}

			return list;
		}
		
		public void SaveParentAndChildren(Parent parent, bool cascade)
		{
			if (parent != null)
			{
				ParentMap.SaveModel(parent);
				SaveLinks(parent);
				SaveChildren(parent, cascade);
			}
		}
		
		public void SaveChildren(Parent parent, bool cascade)
		{
			if (parent != null)
			{
				IList<DataRow> childRows = null;
				if (cascade)
				{
					IList<Child> children = ListChildren(parent);
					foreach(Child child in children)
					{
						DataRow childRow = ChildMap.GetRowFromModel(child);
						childRows.Add(childRow);
					}	
				}
			
				Engine.FillTable(Table, parent.Id);
				Engine.SaveRows(Table, parent.Id, childRows);
			}
		}
		
		public void DeleteParentAndChildren(Parent parent, bool cascade)
		{
			if (parent != null)
			{
				ParentMap.DeleteModel(parent);
				DeleteLinks(parent);
				DeleteChildren(parent, cascade);
			}
		}
		
		public void DeleteChildren(Parent parent, bool cascade)
		{
			if (parent != null)
			{
				IList<DataRow> childRows = null;
				if (cascade)
				{
					IList<Child> children = ListChildren(parent);
					foreach (Child child in children)
					{
						DataRow childRow = ChildMap.GetRowFromModel(child);
						childRows.Add(childRow);
					}				
				}
			
				Engine.DeleteRows(Table, childRows);
			}
		}
		#endregion
	}
}
