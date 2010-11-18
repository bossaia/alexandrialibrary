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
                {'È', "E"}, {'Ê', "E"}, {'Ë', "E"}, {'Û', "U"}, {'Ù', "U"}, {'Ï', "I"}, {'Î', "I"}, {'À', "A"}, {'Â', "A"}, {'Ô', "O"}, {'Ö', "O"},
                {'è', "E"}, {'ê', "E"}, {'ë', "E"}, {'û', "U"}, {'ù', "U"}, {'ï', "I"}, {'î', "I"}, {'à', "A"}, {'â', "A"}, {'ô', "O"}, {'ö', "O"}, 
                //German
                {'ø', "O"},
                //Baltic
                {'ā', "A"}, {'ē', "E"}, {'ī', "I"}, {'ū', "U"}, {'ŗ', "R"}, {'ļ', "L"}, {'ķ', "K"}, {'ņ', "N"}, {'ģ', "G"}, {'š', "S"}, {'ž', "Z"}, {'č', "C"},
                //Spanish
                {'á', "A"}, {'é', "E"}, {'í', "I"}, {'ó', "O"}, {'ú', "U"}, {'ñ', "N"},
                {'Á', "A"}, {'É', "E"}, {'Í', "I"}, {'Ó', "O"}, {'Ú', "U"}, {'Ñ', "N"},
                //Estonian
                {'õ', "O"},
                //Greek
                {'æ', "AE"}, {'Æ', "AE"},
                //Icelandic
                {'ð', "D"}, {'Ð', "D"},
                //Turkic
                {'Ç', "C"}, {'Ğ', "G"}, {'İ', "I"}, {'Ş', "S"}, {'Ü', "U"},
                //Belarusian
                {'ў', "U"}, {'й', "I"}, {'ѝ', "I"},
                //Macedonian
                {'ќ', "K"}, {'ѓ', "G"},
                //Lakota
                {'č', "C"}, {'ȟ', "H"}, {'ǧ', "G"}, {'š', "S"}, {'ž', "Z"}, {'ŋ', "NG"},
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
