using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.UI.Wrapper;

namespace WpfViewModelBasics.UI.Interfaces
{
    public interface IFriendEditViewModel
    {
        Task Load(int? friendId = null);
        FriendWrapper Friend { get; }
    }
}
