namespace WpfViewModelBasics.Core.Interfaces.Services.Command
{
    using WpfViewModelBasics.Core.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFriendEmailCommandService
    {
        Task<FriendEmail> AddFriendEmailAsync(FriendEmail friendEmail);

        Task<List<FriendEmail>> AddFriendEmailListAsync(List<FriendEmail> friendEmails);

        Task UpdateFriendEmailListAsync(List<FriendEmail> friendEmails);

        Task UpdateFriendEmailAsync(FriendEmail friendEmail);

        Task DeleteFriendEmailAsync(FriendEmail friendEmail);
    }
}