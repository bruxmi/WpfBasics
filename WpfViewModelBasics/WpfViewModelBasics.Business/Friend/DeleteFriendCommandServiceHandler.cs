
using WpfViewModelBasics.Core.Repository.Command;
using WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend;

namespace WpfViewModelBasics.Business.Friend
{
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Entities;
    public class DeleteFriendCommandServiceHandler : IAsyncRequestHandler<DeleteFriendRequest, bool>
    {
        private readonly ICommandRepository<Friend> _friendCommandRepository;
        //private readonly IMediator _mediator;

        public DeleteFriendCommandServiceHandler(ICommandRepository<Friend> friendCommandRepository)
        {
            _friendCommandRepository = friendCommandRepository;
        }
        public async Task<bool> Handle(DeleteFriendRequest friend)
        {
            await this._friendCommandRepository.DeleteAsync(friend.Friend);
            return true;
        }
    }
}
