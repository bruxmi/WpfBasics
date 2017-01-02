using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using WpfViewModelBasics.ViewModelMapping.ViewModel;

namespace WpfViewModelBasics.UI.Events
{
    public class SaveFriendEditViewEvent : PubSubEvent<FriendVm>
    {
    }
}
