namespace WpfViewModelBasics.Business.Address
{
    using System.Threading.Tasks;
    using Core.Repository.Command;
    using Core.Entities;
    using MediatR;
    using Core.Requests.Requests.BusinessRequest.Address;

    public class DeleteAddressCommandServiceHandler : IAsyncRequestHandler<DeleteAddressRequest, bool>
    {
        private readonly ICommandRepository<Address> _addressCommandRepository;
        public DeleteAddressCommandServiceHandler(ICommandRepository<Address> addressCommandRepository)
        {
            _addressCommandRepository = addressCommandRepository;
        }

        public async Task<bool> Handle(DeleteAddressRequest message)
        {
            await this._addressCommandRepository.DeleteAsync(message.Address);
            return true;
        }
    }
}
