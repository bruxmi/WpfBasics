namespace WpfViewModelBasics.Business.FriendEmail.Command
{
    using WpfViewModelBasics.Core.Repository.Command;
    using WpfViewModelBasics.Core.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using WpfViewModelBasics.Core.Interfaces.Services.Command;

    public class FriendEmailCommandService: IFriendEmailCommandService
    {
        private readonly ICommandRepository<FriendEmail> _friendEmailCommandRepository;

        public FriendEmailCommandService(ICommandRepository<FriendEmail> friendEmailCommandRepository)
        {
            this._friendEmailCommandRepository = friendEmailCommandRepository;
        }

        public async Task<FriendEmail> AddFriendEmailAsync(FriendEmail friendEmail)
        {
            var result = await this._friendEmailCommandRepository.AddAsync(friendEmail);
            return result;
        }

        public async Task<List<FriendEmail>> AddFriendEmailListAsync(List<FriendEmail> friendEmails)
        {
            var result = await this._friendEmailCommandRepository.AddListAsync(friendEmails);
            return result.ToList();
        }

        public async Task UpdateFriendEmailListAsync(List<FriendEmail> friendEmails)
        {
            await this._friendEmailCommandRepository.UpdateListAsync(friendEmails);
        }

        public async Task UpdateFriendEmailAsync(FriendEmail friendEmail)
        {
            await this._friendEmailCommandRepository.UpdateAsync(friendEmail);
        }

        public async Task DeleteFriendEmailAsync(FriendEmail friendEmail)
        {
            await this._friendEmailCommandRepository.DeleteAsync(friendEmail);
        }

    }
}
