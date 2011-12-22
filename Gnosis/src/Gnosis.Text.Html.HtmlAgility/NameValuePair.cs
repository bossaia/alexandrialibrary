// HtmlAgilityPack V1.0 - Simon Mourier <simon underscore mourier at hotmail dot com>
namespace Gnosis.Text.Html.HtmlAgility
{
    internal class NameValuePair
    {
        #region Fields

        internal readonly string Name;
        internal string Value;

        #endregion

        #region Constructors

        internal NameValuePair()
        {
        }

        internal NameValuePair(string name)
            :
                this()
        {
            Name = name;
        }

        internal NameValuePair(string name, string value)
            :
                this(name)
        {
            Value = value;
        }

        #endregion
    }
}