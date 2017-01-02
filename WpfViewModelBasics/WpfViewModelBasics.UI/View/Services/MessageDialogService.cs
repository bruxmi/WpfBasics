
namespace WpfViewModelBasics.UI.View.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using WpfViewModelBasics.UI.Interfaces;
    public class MessageDialogService: IMessageDialogService
    {

        public async Task<Enums.MessageDialogResult> ShowYesNoDialog(string header, string message, Enums.MessageDialogResult defaultButtonFocus = Enums.MessageDialogResult.Yes)
        {
            var buttonFocus = defaultButtonFocus == Enums.MessageDialogResult.Yes ? MessageDialogResult.Affirmative : MessageDialogResult.Negative;
            var result = await ShowMessage(Application.Current.MainWindow as MetroWindow, header, message, MessageDialogStyle.AffirmativeAndNegative, buttonFocus);
            return result == MessageDialogResult.Affirmative ? Enums.MessageDialogResult.Yes : Enums.MessageDialogResult.No;
        }   
        private async Task<MessageDialogResult> ShowMessage(MetroWindow window, string header,string message, MessageDialogStyle dialogStyle, MessageDialogResult defaultButtonFocus)
        {
            window.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Theme;
            window.MetroDialogOptions.AffirmativeButtonText = "Yes";
            window.MetroDialogOptions.DefaultButtonFocus = defaultButtonFocus;
            return await window.ShowMessageAsync(header, message, dialogStyle, window.MetroDialogOptions);
        }
    }
}
