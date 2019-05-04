using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class Content
    {
        int m_Index = -1;
        int m_Length = -1;
        string m_Value = "";

        internal Content(ushort index, ushort length, string value)
        {
            m_Index = index;
            m_Length = length;
            m_Value = value;
        }

        public int Index => m_Index;
        public int Length => m_Length;
        public string Value => m_Value;

        public override string ToString() => Value;

        internal void Adjust(ushort pos, ushort offset)
        {
            if (pos < m_Index)
                m_Index += offset;

            else if (pos < m_Index + m_Length)
                m_Length += offset;
        }

        internal void UpdateValue(string input)
            => m_Value = input.Substring(m_Index, m_Length);
    }
}
