using System;
using System.Data;

namespace DataAccessLayer.Database
{
    public class DatabaseConcurrencyException : Exception
    {
        public DatabaseConcurrencyException(string message, Exception innerException):base(message,innerException)
        {
            
        }
    }
}