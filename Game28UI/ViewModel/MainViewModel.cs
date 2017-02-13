using System.Windows.Input;

namespace Game28UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public ICommand AnalyzeCommand
        {
            get
            {
                return new GenericCommand()
                {
                    CanExecuteCallback = (obj) => { return true; },
                    ExecuteCallback = (obj) => { new WinAnalyze().Show(); }
                };
            }

        }


        public ICommand CompareCommand
        {
            get
            {
                return new GenericCommand()
                {
                    CanExecuteCallback = (obj) => { return true; },
                    ExecuteCallback = (obj) => { new WinRoundCompare().Show(); }
                };
            }

        }
    }
}
