using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MapperService.Base
{
   public abstract class ModelDtoMapper<TModel, TDto>:ClassMapper
    {
       public ModelDtoMapper()
       {
       }


        public IEnumerable<TDto> GetDtoList(IEnumerable<TModel> modelList)
        {
            return Mapper.Map<IEnumerable<TModel>, IEnumerable<TDto>>(modelList);
        }

        public IEnumerable<TModel> GetModelList(IEnumerable<TDto> viewModelList)
        {
            return Mapper.Map<IEnumerable<TDto>, IEnumerable<TModel>>(viewModelList);
        }

        public TModel GetModel(TDto viewModel)
        {
            return Mapper.Map<TDto, TModel>(viewModel);
        }

        public TDto GetDto(TModel model)
        {
            return Mapper.Map<TModel, TDto>(model);
        }


    }

}
