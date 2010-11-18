using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IJoinable
    {
        IJoin InnerJoin<T>();
        IJoin InnerJoin<T>(string alias);
        IJoin InnerJoin(string table, string alias);
        IJoin LeftOuterJoin<T>();
        IJoin LeftOuterJoin<T>(string alias);
        IJoin LeftOuterJoin(string table, string alias);
    }
}
