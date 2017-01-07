
namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Address
{
    using Core.Entities;
    using MediatR;

    public class AddAddressRequest: IAsyncRequest<Address>
    {
        public Address Address { get; set; }
    }
}
