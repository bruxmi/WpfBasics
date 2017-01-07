using MediatR;

namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend.Command
{
    public class UpdateFriendRequest: IAsyncRequest<bool>
    {
        public UpdateFriendRequest()
        {
            
        }
        public Entities.Friend Friend { get; set; }
    }
}
