using System;

namespace TimeLogger.Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public string[] Errors { get; set; }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string[] errors) : base("Errors occurred. See error details.")
        {
            Errors = errors;
        }
    }
}