using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Interfaces.Services.Command;
using WpfViewModelBasics.Core.Repository.Command;

namespace WpfViewModelBasics.Business.Friend
{
    public class FriendCommandService: IFriendCommandService
    {
        private readonly ICommandRepository<Core.Entities.Friend> _friendCommandRepository;

        public FriendCommandService(ICommandRepository<Core.Entities.Friend> friendCommandRepository)
        {
            _friendCommandRepository = friendCommandRepository;
        }

        public async Task AddFriend(Core.Entities.Friend friend)
        {
            await this._friendCommandRepository.AddAsync(friend);
        }

        public async Task UpdateFriend(Core.Entities.Friend friend)
        {
            await this._friendCommandRepository.UpdateAsync(friend);
        }
    }
}
