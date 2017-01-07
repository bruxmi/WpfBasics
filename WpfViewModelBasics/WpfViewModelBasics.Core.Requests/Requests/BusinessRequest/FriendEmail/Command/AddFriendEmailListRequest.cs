namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.FriendEmail.Command
{
    using System.Collections.Generic;
    using Entities;
    using MediatR;

    public class AddFriendEmailListRequest: IAsyncRequest<List<FriendEmail>>
    {
        public ICollection<FriendEmail> FriendEmails { get; set; }
    }
}