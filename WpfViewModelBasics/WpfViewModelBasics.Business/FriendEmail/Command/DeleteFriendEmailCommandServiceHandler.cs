namespace WpfViewModelBasics.Business.FriendEmail.Command
{
    using System.Threading.Tasks;
    using Core.Entities;
    using Core.Repository.Command;
    using Core.Requests.Requests.BusinessRequest.FriendEmail.Command;
    using MediatR;

    public class DeleteFriendEmailCommandServiceHandler : IAsyncRequestHandler<DeleteFriendEmailRequest, bool>
    {
        private readonly ICommandRepository<FriendEmail> _friendEmailCommandRepository;

        public DeleteFriendEmailCommandServiceHandler(ICommandRepository<FriendEmail> friendEmailCommandRepository)
        {
            _friendEmailCommandRepository = friendEmailCommandRepository;
        }

        public async Task<bool> Handle(DeleteFriendEmailRequest message)
        {
             await _friendEmailCommandRepository.DeleteAsync(message.FriendEmail);
            return true;
        }
    }
}