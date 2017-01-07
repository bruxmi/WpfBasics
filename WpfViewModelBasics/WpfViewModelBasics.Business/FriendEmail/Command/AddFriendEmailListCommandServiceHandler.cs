namespace WpfViewModelBasics.Business.FriendEmail.Command
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Entities;
    using Core.Repository.Command;
    using Core.Requests.Requests.BusinessRequest.FriendEmail.Command;
    using MediatR;

    public class AddFriendEmailListCommandServiceHandler :
        IAsyncRequestHandler<AddFriendEmailListRequest, List<FriendEmail>>
    {
        private readonly ICommandRepository<FriendEmail> _friendEmailCommandRepository;

        public AddFriendEmailListCommandServiceHandler(ICommandRepository<FriendEmail> friendEmailCommandRepository)
        {
            _friendEmailCommandRepository = friendEmailCommandRepository;
        }

        public async Task<List<FriendEmail>> Handle(AddFriendEmailListRequest message)
        {
            var result = await _friendEmailCommandRepository.AddListAsync(message.FriendEmails);
            return result.ToList();
        }
    }
}