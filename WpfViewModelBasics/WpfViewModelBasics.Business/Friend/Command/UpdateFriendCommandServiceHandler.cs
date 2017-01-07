namespace WpfViewModelBasics.Business.Friend
{
    using WpfViewModelBasics.Core.Repository.Command;
    using WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.Friend;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Entities;
    public class UpdateFriendCommandServiceHandler : IAsyncRequestHandler<UpdateFriendRequest, bool>
    {
        private readonly ICommandRepository<Friend> _friendCommandRepository;

        public UpdateFriendCommandServiceHandler(ICommandRepository<Friend> friendCommandRepository)
        {
            _friendCommandRepository = friendCommandRepository;
        }
        public async Task<bool> Handle(UpdateFriendRequest friend)
        {
            await this._friendCommandRepository.UpdateAsync(friend.Friend);
            return true;
        }
    }
}
