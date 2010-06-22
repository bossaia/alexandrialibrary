using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Persistence
{
    public class Table
        : ITable
    {
        public Table(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            _name = name;
            _primaryKey = new Column("Id", typeof(long), null);
        }

        public Table(string name, IEnumerable<IColumn> columns)
            : this(name)
        {
            if (columns == null)
                throw new ArgumentNullException("columns");

            foreach (IColumn column in columns)
                _columns.Add(column);
        }

        private string _name;
        private IColumn _primaryKey;
        private IList<IColumn> _columns = new List<IColumn>();

        private static object GetDefault(Type type)
        {
            switch (type.Name)
            {
                case "Boolean":
                case "Byte":
                case "Int16":
                case "Int32":
                case "Int64":
                    return 0;
                case "Float":
                case "Double":
                case "Decimal":
                    return 0.0;
                case "DateTime":
                    return DateTime.MinValue;
                default:
                    return string.Empty;
            }
        }

        #region ITable Members

        public string Name
        {
            get { return _name; }
        }

        public IColumn PrimaryKey
        {
            get { return _primaryKey; }
        }

        public IEnumerable<IColumn> Columns
        {
            get { return _columns; }
        }

        public ITable AddColumn(string name, Type type)
        {
            return AddColumn(name, type, GetDefault(type));
        }

        public ITable AddColumn(string name, Type type, object @default)
        {
            var array = new IColumn[_columns.Count];
            _columns.CopyTo(array, 0);
            var columns = new List<IColumn>(array);
            columns.Add(new Column(name, type, @default));

            return new Table(_name, columns);
        }

        #endregion
    }
}
