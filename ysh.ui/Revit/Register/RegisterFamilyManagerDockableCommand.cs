using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using ysh.core;


namespace ysh.ui
{
    /// <summary>
    /// 
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class RegisterFamilyManagerDockableCommand : IExternalCommand
    {
        #region public method
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            return Execute(commandData.Application);
        }


        public Result Execute(UIApplication uiApp)
        {
            //
            var dpid = new DockablePaneId(PaneIdentifiers.GetManagerPaneIdentifier());

            //
            var data = new DockablePaneProviderData();

            var familyMangerMainPage = new FamilyManagerMainPage();

            //

            data.FrameworkElement = familyMangerMainPage;

            //
            uiApp.RegisterDockablePane(dpid, "Family Manager", familyMangerMainPage );




            return Result.Succeeded;
        }

        #endregion

    }
}
