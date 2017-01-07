namespace WpfViewModelBasics.Business.Friend.Query
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Entities;
    using Core.Repository.Query;
    using Core.Requests.Requests.BusinessRequest.Friend.Query;
    using MediatR;

    public class GetAllFriendsQueryServiceHandler :
        IAsyncRequestHandler<GetAllFriendsRequest, List<Friend>>
    {
        private readonly IQueryRepository<Friend> _friendQueryRepository;

        public GetAllFriendsQueryServiceHandler(IQueryRepository<Friend> friendQueryRepository)
        {
            _friendQueryRepository = friendQueryRepository;
        }

        public async Task<List<Friend>> Handle(GetAllFriendsRequest message)
        {
            var result = await _friendQueryRepository.GetAllAsync();
            return result;
        }
    }
}