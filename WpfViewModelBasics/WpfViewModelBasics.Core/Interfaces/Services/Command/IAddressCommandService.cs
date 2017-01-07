using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Entities;

namespace WpfViewModelBasics.Core.Interfaces.Services.Command
{
    public interface IAddressCommandService
    {
        Task UpdateAddressAsync(Address address);
        Task<Address> AddAddressAsync(Address addressEntity);
        Task DeleteAddressAsync(Address addressEntity);
    }
}
