using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.UI.Enums;

namespace WpfViewModelBasics.UI.Interfaces
{
    public interface IMessageDialogService
    {
        Task<MessageDialogResult> ShowYesNoDialog(string title, string text, MessageDialogResult defaultResult = MessageDialogResult.Yes);
    }
}
