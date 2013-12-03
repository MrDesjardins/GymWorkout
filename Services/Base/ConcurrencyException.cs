using System;
using DataAccessLayer.Database;

namespace Services.Implementations
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(DatabaseConcurrencyException databaseConcurrencyException):base(databaseConcurrencyException.Message,databaseConcurrencyException)
        {
            
        }
    }
}