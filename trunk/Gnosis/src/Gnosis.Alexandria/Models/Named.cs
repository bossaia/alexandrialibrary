using System;
using System.Collections.Generic;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;

namespace Gnosis.Alexandria.Models
{
    public abstract class Named : Model, INamed
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NameHash = GetNameHash(_name);
            }
        }

        public string Abbreviation
        {
            get;
            set;
        }

        public string NameHash { get; private set; }

        public static string GetNameHash(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            var punctuation = new Dictionary<char, string> {
                {'!', string.Empty}, {'@', string.Empty}, {'#', string.Empty}, {'$', string.Empty}, {'%', string.Empty}, {'^', string.Empty}, {'&', string.Empty}, {'*', string.Empty}, {'(', string.Empty}, {')', string.Empty}, {'-', string.Empty}, {'_', string.Empty}, {'+', string.Empty}, {'=', string.Empty},
                {'{', string.Empty}, {'}', string.Empty}, {'[', string.Empty}, {']', string.Empty}, {'|', string.Empty}, {'\\', string.Empty}, {':', string.Empty}, {';', string.Empty}, {'\'', string.Empty}, {'"', string.Empty}, {'<', string.Empty}, {'>', string.Empty}, {',', string.Empty}, {'.', string.Empty},
                {'?', string.Empty}, {'/', string.Empty}, {'~', string.Empty}, {'`', string.Empty}
            };

            var charactersToNormalize = new Dictionary<char, string> {
                {'À', "A"}, {'Â', "A"}, {'Á', "A"},{'Æ', "AE"},
                {'à', "A"}, {'â', "A"}, {'ā', "A"},{'æ', "AE"}, {'á', "A"},

                {'Ç', "C"},
                {'č', "C"},

                {'Ð', "D"},
                {'ð', "D"}, 

                {'È', "E"}, {'Ê', "E"}, {'Ë', "E"}, {'É', "E"},
                {'è', "E"}, {'ê', "E"}, {'ë', "E"}, {'ē', "E"}, {'é', "E"},

                {'Ğ', "G"}, {'ѓ', "G"},
                {'ģ', "G"}, {'ǧ', "G"},

                {'ȟ', "H"},

                {'Ï', "I"}, {'Î', "I"}, {'Í', "I"}, {'İ', "I"}, {'й', "I"}, {'ѝ', "I"},
                {'ï', "I"}, {'î', "I"}, {'ī', "I"}, {'í', "I"},
                
                {'ќ', "K"},
                {'ķ', "K"},

                {'ļ', "L"},

                {'Ñ', "N"},
                {'ñ', "N"}, {'ņ', "N"}, {'ŋ', "N"},

                {'Ô', "O"}, {'Ö', "O"}, {'Ó', "O"},
                {'ô', "O"}, {'ö', "O"}, {'ø', "O"}, {'ó', "O"}, {'õ', "O"},
                
                {'ŗ', "R"},

                {'Ş', "S"},
                {'š', "S"},

                {'Û', "U"}, {'Ù', "U"}, {'Ú', "U"}, {'Ü', "U"},
                {'û', "U"}, {'ù', "U"}, {'ū', "U"}, {'ú', "U"},
                
                {'ў', "U"},
                {'ž', "Z"}
            };

            var wordsToNormalize = new Dictionary<string, string> {
                {"THE", string.Empty}, {"A", string.Empty}, {"OF", string.Empty}, {"AT", string.Empty}, {"AND", string.Empty}, {"IN", string.Empty}, {"WITH", string.Empty}, {"BUT", string.Empty}, {"OR", string.Empty}, {"FOR", string.Empty}, {"NOR", string.Empty}, {"YET", string.Empty},
                {"ONE", "1"}, {"TWO", "2"}, {"THREE", "3"}, {"FOUR", "4"}, {"FIVE", "5"}, {"SIX", "6"}, {"SEVEN", "7"}, {"EIGHT", "8"}, {"NINE", "9"}, {"TEN", "10"},
                {"ELEVEN", "11"}, {"TWELVE", "12"}, {"THIRTEEN", "13"}, {"FOURTEEN", "14"}, {"FIFTEEN", "15"}, {"SIXTEEN", "16"}, {"SEVENTEEN", "17"}, {"EIGHTEEN", "18"}, {"NINETEEN", "19"}, {"TWENTY", "20"},
                {"HUNDRED", "00"}, {"THOUSAND", "000"}, {"MILLION", "000000"}, {"BILLION", "000000000"}, {"TRILLION", "000000000000"}
            };

            var result = new StringBuilder();
            var wordDelimiters = new string[] { " ", "\t", "\r\n", "\n" };
            var words = name.Split(wordDelimiters, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                var key = word.Trim().ToUpper();
                var noPunctuation = new StringBuilder();
                foreach (var character in key.ToCharArray())
                {
                    if (punctuation.ContainsKey(character))
                        noPunctuation.Append(punctuation[character]);
                    else
                        noPunctuation.Append(character);
                }

                key = noPunctuation.ToString();
                if (wordsToNormalize.ContainsKey(key))
                    key = wordsToNormalize[key];

                var normalized = new StringBuilder();
                foreach (var character in key.ToCharArray())
                {
                    if (charactersToNormalize.ContainsKey(character))
                        normalized.Append(charactersToNormalize[character]);
                    else
                        normalized.Append(character);
                }

                result.Append(normalized.ToString());
            }

            return result.ToString();
        }

    }
}
