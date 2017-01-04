using MediatR;

namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend
{
    public class UpdateFriendRequest: IAsyncRequest<bool>
    {
        public UpdateFriendRequest()
        {
            
        }
        public Entities.Friend Friend { get; set; }
    }
}
