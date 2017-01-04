namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend
{
    using MediatR;
    using Core.Entities;

    public class AddFriendRequest: IAsyncRequest<Friend>
    {
        public Friend Friend { get; set; }
    }
}
