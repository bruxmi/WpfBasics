using System.Threading.Tasks;
using MediatR;
using WpfViewModelBasics.Core.Repository.Command;
using WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend;
using WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend.Command;

namespace WpfViewModelBasics.Business.Friend.Command
{
    public class UpdateFriendCommandServiceHandler : IAsyncRequestHandler<UpdateFriendRequest, bool>
    {
        private readonly ICommandRepository<Core.Entities.Friend> _friendCommandRepository;

        public UpdateFriendCommandServiceHandler(ICommandRepository<Core.Entities.Friend> friendCommandRepository)
        {
            _friendCommandRepository = friendCommandRepository;
        }
        public async Task<bool> Handle(UpdateFriendRequest friend)
        {
            await this._friendCommandRepository.UpdateAsync(friend.Friend);
            return true;
        }
    }
}
