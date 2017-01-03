namespace WpfViewModelBasics.Business.Friend
{
    using System.Threading.Tasks;
    using Core.Interfaces.Services.Command;
    using Core.Repository.Command;
    using Core.Entities;
    public class FriendCommandService: IFriendCommandService
    {
        private readonly ICommandRepository<Friend> _friendCommandRepository;

        public FriendCommandService(ICommandRepository<Friend> friendCommandRepository)
        {
            _friendCommandRepository = friendCommandRepository;
        }

        public async Task AddFriendAsync(Friend friend)
        {
            await this._friendCommandRepository.AddAsync(friend);
        }

        public async Task UpdateFriendAsync(Friend friend)
        {
            await this._friendCommandRepository.UpdateAsync(friend);
        }

        public async Task DeleteFriendAsync(Friend friendEntity)
        {
            await this._friendCommandRepository.DeleteAsync(friendEntity);
        }
    }
}
