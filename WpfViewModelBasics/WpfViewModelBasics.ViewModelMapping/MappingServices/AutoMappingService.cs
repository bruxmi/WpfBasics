using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace WpfViewModelBasics.ViewModelMapping.MappingServices
{
    public class AutoMappingService: IAutoMapperService
    {
        private readonly IMapper _mapper;

        public AutoMappingService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TTo MapTo<TFrom, TTo>(TFrom from)
        {
            var result = this._mapper.Map<TFrom, TTo>(from);
            return result;
        }
    }
}
