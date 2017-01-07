namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.FriendEmail.Command
{
    using Entities;
    using MediatR;

    public class DeleteFriendEmailRequest : IAsyncRequest<bool>
    {
        public FriendEmail FriendEmail { get; set; }
    }
}