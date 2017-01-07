namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.FriendEmail.Command
{
    using Entities;
    using MediatR;

    public class AddFriendEmailRequest : IAsyncRequest<FriendEmail>
    {
        public FriendEmail FriendEmail { get; internal set; }
    }
}