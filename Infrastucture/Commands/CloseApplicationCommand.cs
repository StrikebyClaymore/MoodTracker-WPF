using MoodTracker.Infrastucture.Commands.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MoodTracker.Infrastucture.Commands
{
    internal class CloseApplicationCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => true;
        
        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
