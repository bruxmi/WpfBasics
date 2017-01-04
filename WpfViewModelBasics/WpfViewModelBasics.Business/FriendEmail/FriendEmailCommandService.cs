using System.Collections.Generic;
using System.Linq;

namespace WpfViewModelBasics.Business.FriendEmail
{
    using Core.Repository.Command;
    using System.Threading.Tasks;
    using Core.Interfaces.Services.Command;
    using Core.Entities;

    public class FriendEmailCommandService: IFriendEmailCommandService
    {
        private readonly ICommandRepository<FriendEmail> _friendEmailCommandRepository;

        public FriendEmailCommandService(ICommandRepository<FriendEmail> friendEmailCommandRepository)
        {
            _friendEmailCommandRepository = friendEmailCommandRepository;
        }

        public async Task UpdateEmail(FriendEmail friendEmail)
        {
            await this._friendEmailCommandRepository.UpdateAsync(friendEmail);
        }

        public async Task<FriendEmail> AddEmailAsync(FriendEmail friendEmail)
        {
            return await this._friendEmailCommandRepository.AddAsync(friendEmail);
        }

        public async Task<List<FriendEmail>> AddEmailListAsync(List<FriendEmail> friendEmails)
        {
            return (await this._friendEmailCommandRepository.AddListAsync(friendEmails)).ToList();
        }

        public async Task UpdateEmailListAsync(List<FriendEmail> friendEmails)
        {
            await this._friendEmailCommandRepository.UpdateListAsync(friendEmails);
        }
    }
}
