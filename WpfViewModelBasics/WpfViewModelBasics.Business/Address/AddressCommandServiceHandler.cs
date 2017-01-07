namespace WpfViewModelBasics.Business.Address
{
    using System.Threading.Tasks;
    using Core.Repository.Command;
    using Core.Entities;
    using System;
    using MediatR;
    using Core.Requests.Requests.BusinessRequest.Address;

    public class AddAddressCommandServiceHandler: IAsyncRequestHandler<AddAddressRequest, Address>
    {
        private readonly ICommandRepository<Address> _addressCommandRepository;

        public AddAddressCommandServiceHandler(ICommandRepository<Address> addressCommandRepository)
        {
            _addressCommandRepository = addressCommandRepository;
        }

        public async Task<Address> Handle(AddAddressRequest message)
        {
            return await this._addressCommandRepository.AddAsync(message.Address);
        }
    }
}
