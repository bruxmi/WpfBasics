using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Entities;
using WpfViewModelBasics.Core.Interfaces.Services.Query;
using WpfViewModelBasics.Core.Repository.Query;

namespace WpfViewModelBasics.Business.Query
{
    public class FriendQueryService: IFriendQueryService
    {
        private readonly IQueryRepository<Friend> _friendQueryRepository;

        public FriendQueryService(IQueryRepository<Friend> friendQueryRepository)
        {
            _friendQueryRepository = friendQueryRepository;
        }

        public List<Friend> GetAllFriends()
        {
            var result = this._friendQueryRepository.GetAll();
            return result;
        }

        public Friend GetFriendById(int friendId)
        {
            var friend = this._friendQueryRepository.Single(a => a.Id == friendId);
            return friend;
        }
    }
}
