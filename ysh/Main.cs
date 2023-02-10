#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

#endregion

namespace ysh
{
    public class Main : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            //初始化整个插件的用户界面。
            var ui = new SetupInterface();
            ui.Initialize(a);

            

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
