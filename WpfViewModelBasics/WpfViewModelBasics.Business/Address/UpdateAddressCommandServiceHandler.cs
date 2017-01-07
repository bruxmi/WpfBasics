namespace WpfViewModelBasics.Business.Address
{
    using System.Threading.Tasks;
    using Core.Repository.Command;
    using Core.Entities;
    using MediatR;
    using Core.Requests.Requests.BusinessRequest.Address;

    public class UpdateAddressCommandServiceHandler : IAsyncRequestHandler<UpdateAddressRequest, bool>
    {
        private readonly ICommandRepository<Address> _addressCommandRepository;
        public UpdateAddressCommandServiceHandler(ICommandRepository<Address> addressCommandRepository)
        {
            _addressCommandRepository = addressCommandRepository;
        }

        public async Task<bool> Handle(UpdateAddressRequest message)
        {
            await this._addressCommandRepository.UpdateAsync(message.Address);
            return true;
        }
    }
}
