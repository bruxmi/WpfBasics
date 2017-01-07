namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Address
{
    using MediatR;
    using Core.Entities;

    public class DeleteAddressRequest: IAsyncRequest<bool>
    {
        public Address Address { get; set; }
    }
}
