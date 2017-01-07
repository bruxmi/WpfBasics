namespace WpfViewModelBasics.Business.Friend.Command
{
    using System.Threading.Tasks;
    using Core.Entities;
    using Core.Repository.Command;
    using Core.Requests.Requests.BusinessRequest.Friend.Command;
    using MediatR;

    public class UpdateFriendCommandServiceHandler : IAsyncRequestHandler<UpdateFriendRequest, bool>
    {
        private readonly ICommandRepository<Friend> _friendCommandRepository;

        public UpdateFriendCommandServiceHandler(ICommandRepository<Friend> friendCommandRepository)
        {
            _friendCommandRepository = friendCommandRepository;
        }

        public async Task<bool> Handle(UpdateFriendRequest friend)
        {
            await _friendCommandRepository.UpdateAsync(friend.Friend);
            return true;
        }
    }
}