
namespace WpfViewModelBasics.Core.Interfaces.Services.Command
{
    using System.Threading.Tasks;
    using WpfViewModelBasics.Core.Entities;
    public interface IAddressCommandService
    {
        Task<Address> AddAddress(Address address);


        Task UpdateAddress(Address address);


        Task DeleteAddress(Address address);
    }
}
