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
    }
}
