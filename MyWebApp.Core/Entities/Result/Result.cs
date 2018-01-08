using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MyWebApp.Core.Entities.Result
{
    public class Result
    {
        public Result()
        {
        }

        public Result(bool succeeded)
        {
            Succeeded = succeeded;
        }

        private static Result _success = new Result(true);
        
        /// <summary>
        /// Returns a Result indicating a successful operation.
        /// </summary>        
        public static Result Success => _success;

        /// <summary>
        /// Flag indicating whether if the operation succeeded or not.
        /// </summary>        
        public bool Succeeded { get; protected set; }

        private List<ResultError> _errors = new List<ResultError>();

        /// <summary>
        /// System.Collections.Generic.IEnumerable of ResultError
        /// containing an errors that occurred during the operation.
        /// </summary>        
        public IEnumerable<ResultError> Errors => _errors;

        /// <summary>
        /// Creates a Result indicating a failed operation, with a list of errors if applicable.
        /// Parameters:
        ///     errors: An optional array of ResultError which caused the operation to fail.
        /// Returns:  Result indicating a failed operation, with a list of errors if applicable.
        /// </summary>        
        public static Result Failed(params ResultError[] errors)
        {
            var result = new Result();

            foreach (var error in errors)
                result._errors.Add(error);

            return result;
        }
        
        /// <summary>
        /// Converts the value of the Result object to its equivalent string representation.        
        /// </summary>
        /// <returns>
        /// If the operation was successful the ToString() will return "Succeeded" otherwise
        /// it returned "Failed : " followed by a comma delimited list of error codes and message from
        /// its Result.Errors collection, if any.
        /// </returns>
        /// <remarks>
        /// A string representation of the current Result object.
        /// </remarks>
        public override string ToString()
        {
            if (Succeeded)
                return "Succeeded";
            else
            {
                var sbResult = new StringBuilder();
                sbResult.Append("Failed : ");

                sbResult.Append(String.Join(",", _errors.Select(e => e.Code + "-" + e.Message).ToArray()));
                return sbResult.ToString();
            }
        }
    }
}
