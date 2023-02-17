using Autodesk.Revit.UI;
using System.Windows.Controls;
using System;


namespace ysh.ui
{
    /// <summary>
    /// FamilyManagerMainPage.xaml 的交互逻辑
    /// </summary>
    public partial class FamilyManagerMainPage : Page ,IDisposable,IDockablePaneProvider
    {
        #region constructor
        public FamilyManagerMainPage()
        {
            InitializeComponent();
        }
        #endregion



        #region public method

        /// <summary>
        /// 非托管资源释放
        /// </summary>
        public void Dispose()
        {
            this.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this;

            data.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Right
            };


        }

        #endregion

    }
}
