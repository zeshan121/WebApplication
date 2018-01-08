using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebApp.Core.Entities.Result
{
    public class ResultError
    {        
        /// <summary>
        /// Gets or sets the code for this error.
        /// </summary>        
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the message for this error.
        /// </summary>        
        public string Message { get; set; }
    }
}
