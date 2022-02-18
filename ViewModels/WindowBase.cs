using MoodTracker.Infrastucture.Commands;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace MoodTracker.ViewModels.Base
{
    internal class WindowBase : ViewModelBase
    {
        #region Базовые команды окна

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            MessageBox.Show("SHUTDOWN");
            Application.Current.Shutdown();
        }

        #endregion

        public WindowBase()
        {
            CloseApplicationCommand = new ActionCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
        }
    }
}
