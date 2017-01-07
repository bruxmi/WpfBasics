using System.Threading.Tasks;
using MediatR;
using WpfViewModelBasics.Core.Repository.Command;
using WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend;
using WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend.Command;

namespace WpfViewModelBasics.Business.Friend.Command
{
    public class DeleteFriendCommandServiceHandler : IAsyncRequestHandler<DeleteFriendRequest, bool>
    {
        private readonly ICommandRepository<Core.Entities.Friend> _friendCommandRepository;

        public DeleteFriendCommandServiceHandler(ICommandRepository<Core.Entities.Friend> friendCommandRepository)
        {
            _friendCommandRepository = friendCommandRepository;
        }
        public async Task<bool> Handle(DeleteFriendRequest friend)
        {
            await this._friendCommandRepository.DeleteAsync(friend.Friend);
            return true;
        }
    }
}
