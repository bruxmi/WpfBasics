namespace WpfViewModelBasics.Business.FriendEmail.Command
{
    using System;
    using System.Threading.Tasks;
    using Core.Entities;
    using Core.Repository.Command;
    using Core.Requests.Requests.BusinessRequest.FriendEmail.Command;
    using MediatR;

    public class UpdateFriendEmailCommandServiceHandler : IAsyncRequestHandler<UpdateFriendEmailRequest, bool>
    {
        private readonly ICommandRepository<FriendEmail> _friendEmailCommandRepository;

        public UpdateFriendEmailCommandServiceHandler(ICommandRepository<FriendEmail> friendEmailCommandRepository)
        {
            _friendEmailCommandRepository = friendEmailCommandRepository;
        }
        public async Task<bool> Handle(UpdateFriendEmailRequest message)
        {
            await this._friendEmailCommandRepository.UpdateAsync(message.FriendEmail);
            return true;
        }
    }
}