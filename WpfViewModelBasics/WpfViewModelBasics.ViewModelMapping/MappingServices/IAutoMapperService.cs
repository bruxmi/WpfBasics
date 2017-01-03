using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfViewModelBasics.ViewModelMapping.MappingServices
{
    public interface IAutoMapperService
    {
        TTo MapTo<TFrom, TTo>(TFrom from);
    }
}
