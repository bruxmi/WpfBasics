namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Address
{
    using MediatR;
    using Core.Entities;

    public class UpdateAddressRequest : IAsyncRequest<bool>
    {
        public Address Address { get; set; }
    }
}
