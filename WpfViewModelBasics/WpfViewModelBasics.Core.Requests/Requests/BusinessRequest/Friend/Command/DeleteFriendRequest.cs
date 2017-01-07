using MediatR;

namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend.Command
{
    public class DeleteFriendRequest: IAsyncRequest<bool>
    {
        public Entities.Friend Friend { get; set; }
    }
}
