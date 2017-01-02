using System.Collections.Generic;
using WpfViewModelBasics.Core.Interfaces.Services.Query;
using WpfViewModelBasics.Core.Repository.Query;

namespace WpfViewModelBasics.Business.Friend
{
    public class FriendQueryService: IFriendQueryService
    {
        private readonly IQueryRepository<Core.Entities.Friend> _friendQueryRepository;

        public FriendQueryService(IQueryRepository<Core.Entities.Friend> friendQueryRepository)
        {
            _friendQueryRepository = friendQueryRepository;
        }

        public List<Core.Entities.Friend> GetAllFriends()
        {
            var result = this._friendQueryRepository.GetAll();
            return result;
        }

        public Core.Entities.Friend GetFriendById(int friendId)
        {
            var friend = this._friendQueryRepository.Single(a => a.Id == friendId);
            return friend;
        }
    }
}
