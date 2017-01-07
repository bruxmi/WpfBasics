namespace WpfViewModelBasics.Business.Friend.Command
{
    using System.Threading.Tasks;
    using Core.Entities;
    using Core.Repository.Command;
    using Core.Requests.Requests.BusinessRequest.Friend.Command;
    using MediatR;

    public class AddFriendCommandServiceHandler : IAsyncRequestHandler<AddFriendRequest, Friend>
    {
        private readonly ICommandRepository<Friend> _friendCommandRepository;
        //private readonly IMediator _mediator;

        public AddFriendCommandServiceHandler(ICommandRepository<Friend> friendCommandRepository)
        {
            _friendCommandRepository = friendCommandRepository;
        }

        public async Task<Friend> Handle(AddFriendRequest friend)
        {
            return await _friendCommandRepository.AddAsync(friend.Friend);
        }
    }
}