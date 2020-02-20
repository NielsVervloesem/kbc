using System;
using System.Collections.Generic;
using System.Text;

namespace KBCFoodAndGo.Shared.Exceptions
{
    public class BadRequestException : Exception
    {
        public string Content { get; set; }

        public BadRequestException(string content)
        {
            Content = content;
        }

        public BadRequestException() { }
    }
}
