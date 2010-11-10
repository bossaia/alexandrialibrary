using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Utilities.Interfaces
{
    public interface IStringBuilder
    {
        string ClauseDelimiter { get; set; }
        string TokenDelimiter { get; set; }
        IStringBuilder Append(string value);
        IStringBuilder Append(IStringBuilder builder);
        IStringBuilder AppendClause(params string[] tokens);
        IStringBuilder AppendFormat(string format, params object[] args);
        IStringBuilder AppendLine(string value);

        string ToString();
    }
}
