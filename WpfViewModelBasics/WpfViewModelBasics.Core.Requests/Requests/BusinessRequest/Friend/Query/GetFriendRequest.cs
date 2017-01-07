namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend.Query
{
    using MediatR;
    using Core.Entities;
    public class GetFriendRequest: IAsyncRequest<Friend>
    {
        public int FriendId { get; set; }
    }
}