using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataAccessLayer.Repositories.Base;
using Mappers;
using Mappers.Factory;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Model;
using Services.Base;
using Services.Definitions;
using ViewModels;
using ViewModels.Selectors.Implementations;

namespace Services.Implementations
{
    
    public class ApplicationUserService : BaseService, IAccountService
    {
        private UserManager<ApplicationUser> _manager;
        public ApplicationUserService(IRepositoryFactory repositoryFactory, IMapperFactory mapperFactory) : base(repositoryFactory, mapperFactory, null)
        {
            _manager = new UserManager<ApplicationUser>(base.Repository.ApplicationUser.GetUserStore());
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser New()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Get(ApplicationUser model)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Create(ApplicationUser model)
        {
            throw new NotImplementedException();
        }

        public int Update(ApplicationUser model)
        {
            throw new NotImplementedException();
        }

        public int Update(ApplicationUser model, params Expression<Func<ApplicationUser, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public int Delete(ApplicationUser model)
        {
            throw new NotImplementedException();
        }

        public void CreateUserProfile(ApplicationUser userProfile)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetByUserName(string toLower)
        {
           
            var currentUser = _manager.FindByName(toLower);
            return currentUser;
        }

        public ApplicationUser GetById(string id)// User.Identity.GetUserId();
        {
           
            var currentUser = _manager.FindById(id);
            return currentUser;
        }

        public UserManager<ApplicationUser> GetUserManager()
        {
            return _manager;
        }



    }
}