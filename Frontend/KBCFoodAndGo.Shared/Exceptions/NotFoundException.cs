using System;
using System.Collections.Generic;
using System.Text;

namespace KBCFoodAndGo.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Content { get; set; }

        public NotFoundException(string content)
        {
            Content = content;
        }

        public NotFoundException() { }
    }
}
