using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.Core.Entities;

namespace WpfViewModelBasics.Core.Interfaces.Services.Query
{
    public interface IFriendQueryService
    {
        List<Friend> GetAllFriends();
        Friend GetFriendById(int friendId);
    }
}
