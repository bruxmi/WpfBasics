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
        public List<FriendEmail> GetFriendEmailByFriendId(int friendId)
        {
            var result = this._friendEmailQueryRepository.Find(a => a.FriendId == friendId);
            return result;
        }
    }
}
