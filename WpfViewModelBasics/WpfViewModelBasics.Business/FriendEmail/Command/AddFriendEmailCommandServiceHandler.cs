namespace WpfViewModelBasics.Business.FriendEmail.Command
{
    using System.Threading.Tasks;
    using Core.Entities;
    using Core.Repository.Command;
    using Core.Requests.Requests.BusinessRequest.FriendEmail.Command;
    using MediatR;

    public class AddFriendEmailCommandServiceHandler : IAsyncRequestHandler<AddFriendEmailRequest, FriendEmail>
    {
        private readonly ICommandRepository<FriendEmail> _friendEmailCommandRepository;

        public AddFriendEmailCommandServiceHandler(ICommandRepository<FriendEmail> friendEmailCommandRepository)
        {
            _friendEmailCommandRepository = friendEmailCommandRepository;
        }

        public async Task<FriendEmail> Handle(AddFriendEmailRequest message)
        {
            var result = await _friendEmailCommandRepository.AddAsync(message.FriendEmail);
            return result;
        }
    }
}