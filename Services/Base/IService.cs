using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Services.Base
{
    public interface IService<TModel>
    {
        IEnumerable<TModel> GetAll();
        TModel New();
        TModel Get(TModel model);
        TModel Create(TModel model);
        int Update(TModel model);
        int Update(TModel model, params Expression<Func<TModel, object>>[] properties);
        int Delete(TModel model);
    }
}