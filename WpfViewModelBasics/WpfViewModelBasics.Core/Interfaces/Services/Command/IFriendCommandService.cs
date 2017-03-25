namespace WpfViewModelBasics.Core.Interfaces.Services.Command
{
    using System.Threading.Tasks;
    using WpfViewModelBasics.Core.Entities;

    public interface IFriendCommandService
    {
        Task<Friend> AddFriend(Friend address);


        Task UpdateFriend(Friend address);


        Task DeleteFriend(Friend address);
    }
}
