using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace ysh.core
{
    /// <summary>
    /// 
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class HideFamilyManagerCommand : IExternalCommand
    {
        #region public method

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            TaskDialog.Show("info", "test show...");
            return Result.Succeeded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetPath()
        {
            return typeof(HideFamilyManagerCommand).Namespace + "." + nameof(HideFamilyManagerCommand);
        }
        #endregion

    }
}
