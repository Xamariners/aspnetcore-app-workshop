using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Infrastructure
{
    public static class StringExtensions
    {
        public static string Truncate(this string text, int length, string ellipsis, bool keepFullWordAtEnd)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
    
            if (text.Length < length) return text;
    
            text = text.Substring(0, length);
    
            if (keepFullWordAtEnd)
            {
                text = text.Substring(0, text.LastIndexOf(' '));
            }
    
            return text + ellipsis;
        }
    }
}
