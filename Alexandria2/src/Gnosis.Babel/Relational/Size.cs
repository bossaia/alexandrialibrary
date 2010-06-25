using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public struct Size
    {
        public Size(int length)
        {
            _length = length;
            _precision = 0;
            _scale = 0;
            _isVariableLength = true;
        }

        public Size(int length, bool isVariableLength)
        {
            _length = length;
            _precision = 0;
            _scale = 0;
            _isVariableLength = isVariableLength;
        }

        public Size(int precision, int scale)
        {
            _length = 0;
            _precision = precision;
            _scale = scale;
            _isVariableLength = false;
        }

        private int _length;
        private int _precision;
        private int _scale;
        private bool _isVariableLength;

        public int Length { get { return _length; } }
        public int Precision { get { return _precision; } }
        public int Scale { get { return _scale; } }
        public bool IsVariableLength { get { return _isVariableLength; } }

        public static readonly Size Zero = new Size(0, false);
    }
}
