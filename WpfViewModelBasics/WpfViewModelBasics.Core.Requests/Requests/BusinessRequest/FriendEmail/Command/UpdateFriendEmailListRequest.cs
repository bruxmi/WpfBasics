namespace WpfViewModelBasics.Core.Requests.Requests.BusinessRequest.FriendEmail.Command
{
    using System.Collections.Generic;
    using Entities;
    using MediatR;

    public class UpdateFriendEmailListRequest: IAsyncRequest<bool>
    {
        public List<FriendEmail> FriendEmails { get; set; }
    }
}