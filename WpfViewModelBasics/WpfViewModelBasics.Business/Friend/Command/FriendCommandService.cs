
namespace WpfViewModelBasics.Business.Friend.Command
{
    using System.Threading.Tasks;
    using WpfViewModelBasics.Core.Entities;
    using WpfViewModelBasics.Core.Interfaces.Services.Command;
    using WpfViewModelBasics.Core.Repository.Command;

    public class FriendCommandService: IFriendCommandService
    {
        private readonly ICommandRepository<Friend> _friendCommandRepository;

        public FriendCommandService(ICommandRepository<Friend> friendCommandRepository)
        {
            this._friendCommandRepository = friendCommandRepository;
        }

        public async Task<Friend> AddFriend(Friend address)
        {
            return await this._friendCommandRepository.AddAsync(address);
        }

        public async Task DeleteFriend(Friend address)
        {
            await this._friendCommandRepository.UpdateAsync(address);
        }

        public async Task UpdateFriend(Friend address)
        {
            await this._friendCommandRepository.DeleteAsync(address);
        }
    }
}
