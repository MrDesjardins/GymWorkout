using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Database
{
    public class RemoveValidation:IRemoveValidation
    {
        private readonly DbContextConfiguration _configuration;

        public RemoveValidation(DbContextConfiguration configuration)
        {
            _configuration = configuration;
            _configuration.ValidateOnSaveEnabled = false;
        }

        public void Dispose()
        {
            _configuration.ValidateOnSaveEnabled = true;
        }
    }

    public interface IRemoveValidation:IDisposable
    {
    }
}
