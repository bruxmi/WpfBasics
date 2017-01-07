using System.Threading.Tasks;
using MediatR;
using WpfViewModelBasics.Core.Repository.Command;
using WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend;
using WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend.Command;

namespace WpfViewModelBasics.Business.Friend.Command
{
    public class AddFriendCommandServiceHandler: IAsyncRequestHandler<AddFriendRequest, Core.Entities.Friend>
    {
        private readonly ICommandRepository<Core.Entities.Friend> _friendCommandRepository;
        //private readonly IMediator _mediator;

        public AddFriendCommandServiceHandler(ICommandRepository<Core.Entities.Friend> friendCommandRepository)
        {
            _friendCommandRepository = friendCommandRepository;
        }

        public async Task<Core.Entities.Friend> Handle(AddFriendRequest friend)
        {
            return await this._friendCommandRepository.AddAsync(friend.Friend);
        }
    }
}
