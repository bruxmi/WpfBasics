namespace WpfViewModelBasics.Business.Friend.Query
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WpfViewModelBasics.Core.Entities;
    using WpfViewModelBasics.Core.Interfaces.Services.Query;
    using WpfViewModelBasics.Core.Repository.Query;

    public class FriendQueryService: IFriendQueryService
    {
        private readonly IQueryRepository<Friend> _friendQueryRepository;

        public FriendQueryService(IQueryRepository<Friend> friendQueryRepository)
        {
            this._friendQueryRepository = friendQueryRepository;
        }

        public async Task<List<Friend>> GetAllFriend()
        {
            return await this._friendQueryRepository.GetAllAsync();
        }

        public async Task<Friend> GetFriendById(int id)
        {
            return await this._friendQueryRepository.SingleOrDefaultAsync(a => a.Id == id);
        }
    }
}
