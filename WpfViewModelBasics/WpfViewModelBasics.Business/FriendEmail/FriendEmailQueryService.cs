using System.Threading.Tasks;

namespace WpfViewModelBasics.Business.FriendEmail
{
    using System.Collections.Generic;
    using Core.Entities;
    using Core.Interfaces.Services.Command;
    using Core.Repository.Query;

    public class FriendEmailQueryService: IFriendEmailQueryService
    {
        private readonly IQueryRepository<FriendEmail> _friendEmailQueryRepository;

        public FriendEmailQueryService(IQueryRepository<FriendEmail> friendEmailQueryRepository)
        {
            _friendEmailQueryRepository = friendEmailQueryRepository;
        }
        public async Task<List<FriendEmail>> GetFriendEmailByFriendIdAsync(int friendId)
        {
            var result = await this._friendEmailQueryRepository.FindAsync(a => a.FriendId == friendId);
            return result;
        }
    }
}
