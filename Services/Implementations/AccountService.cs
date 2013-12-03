using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataAccessLayer.Repositories.Base;
using Mappers;
using Mappers.Factory;
using Model;
using Services.Base;
using Services.Definitions;
using ViewModels;
using ViewModels.Selectors.Implementations;

namespace Services.Implementations
{
    public class AccountService : BaseService, IAccountService
    {
        #region Implementation of IAccountService

        public AccountService(IRepositoryFactory repositoryFactory, IMapperFactory mapperFactory) : base(repositoryFactory, mapperFactory)
        {
        }

        public void CreateUserProfile(UserProfile userProfile)
        {
            Repository.UserProfile.Insert(userProfile);
        }

        public UserProfile GetByUserName(string toLower)
        {
            return Repository.UserProfile.GetByUserName(toLower);
        }

        #endregion

        public IEnumerable<UserProfile> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public UserProfile New()
        {
            throw new System.NotImplementedException();
        }

        public UserProfile Get(UserProfile model)
        {
            var modelLoaded = Repository.UserProfile.Get(model.UserId);       
            return modelLoaded;
        }

        public UserProfile Create(UserProfile model)
        {
            throw new System.NotImplementedException();
        }

        public int Update(UserProfile model)
        {
            return Repository.UserProfile.Update(model);
        }

        public int Update(UserProfile model, params Expression<Func<UserProfile, object>>[] properties)
        {
            return Repository.UserProfile.Update(model);
        }

        public int Delete(UserProfile model)
        {
            throw new System.NotImplementedException();
        }
    }
}