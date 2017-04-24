namespace WpfViewModelBasics.Business.Address
{
    using System.Threading.Tasks;
    using Core.Entities;
    using WpfViewModelBasics.Core.Repository.Command;
    using WpfViewModelBasics.Core.Interfaces.Services.Command;

    public class AddressCommandService: IAddressCommandService
    {
        private readonly ICommandRepository<Address> _addressCommandRepository;

        public AddressCommandService(ICommandRepository<Address> addressCommandRepository)
        {
            this._addressCommandRepository = addressCommandRepository;
        }

        public async Task<Address> AddAddress(Address address)
        {
            return await this._addressCommandRepository.AddAsync(address);
        }

        public async Task UpdateAddress(Address address)
        {
            await this._addressCommandRepository.UpdateAsync(address);
        }

        public async Task DeleteAddress(Address address)
        {
            await this._addressCommandRepository.DeleteAsync(address);
        }
    }
}
