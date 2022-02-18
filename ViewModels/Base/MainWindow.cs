using System;
using System.Collections.Generic;
using System.Text;

namespace MoodTracker.ViewModels.Base
{
    internal class MainWindow : ViewModelBase
    {
        private string _Title = "Title";

        /// <summary> Заголовок окна </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
    }
}
