using MediatR;

namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend.Command
{
    public class AddFriendRequest: IAsyncRequest<Entities.Friend>
    {
        public Entities.Friend Friend { get; set; }
    }
}
