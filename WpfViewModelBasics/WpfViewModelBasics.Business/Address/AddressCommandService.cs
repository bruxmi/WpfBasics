
namespace WpfViewModelBasics.Business.Address
{
    using System;
    using System.Threading.Tasks;
    using Core.Interfaces.Services.Command;
    using Core.Repository.Command;

    public class AddressCommandService: IAddressCommandService
    {
        private readonly ICommandRepository<Core.Entities.Address> _addressCommandRepository;

        public AddressCommandService(ICommandRepository<Core.Entities.Address> addressCommandRepository)
        {
            _addressCommandRepository = addressCommandRepository;
        }

        public async Task UpdateAddress(Core.Entities.Address address)
        {
           await this._addressCommandRepository.UpdateAsync(address);
        }
    }
}
