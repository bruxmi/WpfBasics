using System.Threading.Tasks;

namespace WpfViewModelBasics.Business.Friend
{
    using System.Collections.Generic;
    using Core.Interfaces.Services.Query;
    using Core.Repository.Query;
    using Core.Entities;
    public class FriendQueryService: IFriendQueryService
    {
        private readonly IQueryRepository<Friend> _friendQueryRepository;

        public FriendQueryService(IQueryRepository<Friend> friendQueryRepository)
        {
            _friendQueryRepository = friendQueryRepository;
        }

        public async Task<List<Friend>> GetAllFriendsAsync()
        {
            var result = await this._friendQueryRepository.GetAllAsync();
            return result;
        }

        public async Task<Friend> GetFriendByIdAsync(int friendId)
        {
            var friend = await this._friendQueryRepository.SingleAsync(a => a.Id == friendId);
            return friend;
        }
    }
}
