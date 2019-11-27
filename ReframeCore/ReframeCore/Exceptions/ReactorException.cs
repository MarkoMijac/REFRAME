﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Exceptions
{
    public class ReactorException : ReframeException
    {
        public ReactorException()
            :base("Generic reactor exception")
        {

        }

        public ReactorException(string message)
            :base(message)
        {

        }
    }
}
