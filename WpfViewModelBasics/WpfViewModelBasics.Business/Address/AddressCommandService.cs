namespace WpfViewModelBasics.Business.Address
{
    using System.Threading.Tasks;
    using Core.Interfaces.Services.Command;
    using Core.Repository.Command;
    using Core.Entities;

    public class AddressCommandService: IAddressCommandService
    {
        private readonly ICommandRepository<Address> _addressCommandRepository;

        public AddressCommandService(ICommandRepository<Address> addressCommandRepository)
        {
            _addressCommandRepository = addressCommandRepository;
        }

        public async Task UpdateAddressAsync(Address address)
        {
           await this._addressCommandRepository.UpdateAsync(address);
        }

        public async Task<Address> AddAddressAsync(Address address)
        {
            return await this._addressCommandRepository.AddAsync(address);
        }
    }
}
