using MoodTracker.Infrastucture.Commands.Base;
using System;

namespace MoodTracker.Infrastucture.Commands
{
    internal class ActionCommand : CommandBase
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public ActionCommand(Action<object> execurte, Func<object, bool> canExecute = null)
        {
            _execute = execurte ?? throw new ArgumentNullException(nameof(_execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _execute(parameter);
    }
}
