namespace WpfViewModelBasics.Business.Friend.Query
{
    using System.Threading.Tasks;
    using Core.Entities;
    using Core.Repository.Query;
    using Core.Requests.Requests.BusinessRequest.Friend.Query;
    using MediatR;

    public class GetFriendQueryServiceHandler : IAsyncRequestHandler<GetFriendRequest, Friend>
    {
        private readonly IQueryRepository<Friend> _friendQueryRepository;

        public GetFriendQueryServiceHandler(IQueryRepository<Friend> friendQueryRepository)
        {
            _friendQueryRepository = friendQueryRepository;
        }

        public async Task<Friend> Handle(GetFriendRequest message)
        {
            var result = await this._friendQueryRepository.FirstAsync(a => a.Id == message.FriendId);
            return result;
        }
    }
}