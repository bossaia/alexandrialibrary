using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public struct Name
    {
        public Name(string name)
        {
            _raw = name;
            _display = GetDisplay(name);
            _search = GetSearch(name);
            _sort = GetSort(name);
        }

        private string _raw;
        private string _display;
        private string _search;
        private string _sort;

        private static string GetDisplay(string name)
        {
            return name;
        }

        private static string GetSearch(string name)
        {
            return name;
        }

        private static string GetSort(string name)
        {
            return name;
        }

        public string Raw
        {
            get { return _raw; }
        }

        public string Display
        {
            get { return _display; }
        }

        public string Search
        {
            get { return _search; }
        }

        public string Sort
        {
            get { return _sort; }
        }

        public override string ToString()
        {
            return _display ?? string.Empty;
        }
    }
}
