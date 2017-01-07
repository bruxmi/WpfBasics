namespace WpfViewModelBasics.Business.Friend.Command
{
    using System.Threading.Tasks;
    using Core.Entities;
    using Core.Repository.Command;
    using Core.Requests.Requests.BusinessRequest.Friend.Command;
    using MediatR;

    public class DeleteFriendCommandServiceHandler : IAsyncRequestHandler<DeleteFriendRequest, bool>
    {
        private readonly ICommandRepository<Friend> _friendCommandRepository;

        public DeleteFriendCommandServiceHandler(ICommandRepository<Friend> friendCommandRepository)
        {
            _friendCommandRepository = friendCommandRepository;
        }

        public async Task<bool> Handle(DeleteFriendRequest friend)
        {
            await _friendCommandRepository.DeleteAsync(friend.Friend);
            return true;
        }
    }
}