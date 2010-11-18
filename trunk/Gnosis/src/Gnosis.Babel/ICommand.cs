using System.Collections.Generic;

namespace Gnosis.Babel
{
    public interface ICommand
    {
        string Text { get; set; }
        IEnumerable<KeyValuePair<string, object>> Parameters { get; }
        void AddParameter(string name, object value);
        void AddParameters(IEnumerable<KeyValuePair<string, object>> parameters);
        void CallbackWithResult(object value);
    }
}
