#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using ysh.ui;

#endregion

namespace ysh
{
    public class Main : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            //��ʼ������������û����档
            var ui = new SetupInterface();
            ui.Initialize(a);

            //��ʼ������ʱ����ͣ������
            a.ControlledApplication.ApplicationInitialized += DockablePaneRegister;
            

            return Result.Succeeded;
        }

        private void DockablePaneRegister(object sender, Autodesk.Revit.DB.Events.ApplicationInitializedEventArgs e)
        {
            var familyManagerRegisterCommand = new RegisterFamilyManagerDockableCommand();

            familyManagerRegisterCommand.Execute(new UIApplication(sender as Application));
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
