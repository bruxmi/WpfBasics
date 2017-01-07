using System.Collections.Generic;
using MediatR;

namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend.Query
{
    public class GetAllFriendsRequest: IAsyncRequest<List<Entities.Friend>>
    {
    }
}