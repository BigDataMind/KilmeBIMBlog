using System.Windows.Input;

namespace ysh.core
{
    /// <summary>
    /// A view model for the main application page 
    /// </summary>
    public class FamilyManagerMainViewModel 
    {
        /// <summary>
        /// Initialize  application main page as family page
        /// </summary>
        public ApplicationPageType CurrentPage { get; set; } = ApplicationPageType.Family;

        #region Default constructor

        /// <summary>
        /// Define buttonCommand instance and execute delegate method
        /// </summary>
        public FamilyManagerMainViewModel()
        {
            FamilyBtnCommand = new RouteCommands(FamilyExecute);
            PreferencesBtnCommand = new RouteCommands(PreferencesExecute);
        }
        #endregion

        #region Commands

        /// <summary>
        /// Get or set the current page as family page
        /// </summary>
        public ICommand FamilyBtnCommand { get; set; }

        /// <summary>
        /// Get or set the current page as preferences page
        /// </summary>
        public ICommand PreferencesBtnCommand { get; set; }

        #endregion


        #region private method as delegate execute.

        /// <summary>
        /// 
        /// </summary>
        private void FamilyExecute()
        {
            Message.Display("Family Button Clicked", WindowType.Inforamtion);
        }

        private void PreferencesExecute()
        {
            Message.Display("Preferences Button Clicked", WindowType.Inforamtion);
        }

        #endregion
    }
}
