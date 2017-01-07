namespace WpfViewModelBasics.Business.FriendEmail.Command
{
    using System.Threading.Tasks;
    using Core.Entities;
    using Core.Repository.Command;
    using Core.Requests.Requests.BusinessRequest.FriendEmail.Command;
    using MediatR;

    public class UpdateFriendEmailListCommandServiceHandler : IAsyncRequestHandler<UpdateFriendEmailListRequest, bool>
    {
        private readonly ICommandRepository<FriendEmail> _friendEmailCommandRepository;

        public UpdateFriendEmailListCommandServiceHandler(ICommandRepository<FriendEmail> friendEmailCommandRepository)
        {
            _friendEmailCommandRepository = friendEmailCommandRepository;
        }

        public async Task<bool> Handle(UpdateFriendEmailListRequest message)
        {
            await _friendEmailCommandRepository.UpdateListAsync(message.FriendEmails);
            return true;
        }
    }
}