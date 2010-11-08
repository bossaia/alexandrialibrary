using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models
{
    public abstract class Named : Model, INamed
    {
        private string _name;
        private string _nameHash;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _nameHash = GetNameHash(_name);
            }
        }

        public string Abbreviation
        {
            get;
            set;
        }

        public string NameHash
        {
            get { return _nameHash; }
        }

        public static string GetNameHash(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            Dictionary<string, string> hashWordsToNormalize = new Dictionary<string, string> { 
                {"!", ""}, {"@", ""}, {"#", ""}, {"$", ""}, {"%", ""}, {"^", ""}, {"&", ""}, {"*", ""}, {"(", ""}, {")", ""}, {"-", ""}, {"_", ""}, {"+", ""}, {"=", ""},
                {"{", ""}, {"}", ""}, {"[", ""}, {"]", ""}, {"|", ""}, {"\\", ""}, {":", ""}, {";", ""}, {"\"", ""}, {"'", ""}, {"<", ""}, {">", ""}, {",", ""}, {".", ""},
                {"?", ""}, {"/", ""}, {"~", ""}, {"`", ""}, 
                {"THE", ""}, {"A", ""}, {"OF", ""}, {"AT", ""}, {"AND", ""}, {"IN", ""}, {"WITH", ""}, {"BUT", ""}, {"OR", ""}, {"FOR", ""}, {"NOR", ""}, {"YET", ""},
                {"1", "ONE"}, {"2", "TWO"}, {"3", "THREE"}, {"4", "FOUR"}, {"5", "FIVE"}, {"6", "SIX"}, {"7", "SEVEN"}, {"8", "EIGHT"}, {"9", "NINE"}, {"10", "TEN"}
            };

            var result = new StringBuilder();
            var wordDelimiters = new string[] { " ", "\t", "\r\n", "\n" };
            var words = name.Split(wordDelimiters, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                var key = word.Trim().ToUpper();
                if (hashWordsToNormalize.ContainsKey(key))
                    result.Append(hashWordsToNormalize[key]);
                else result.Append(key);
            }

            return result.ToString();
        }

    }
}
