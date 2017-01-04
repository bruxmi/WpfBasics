namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend
{
    using MediatR;
    public class AddFriendRequest: IAsyncRequest<Entities.Friend>
    {
        public Entities.Friend Friend { get; set; }
    }
}
