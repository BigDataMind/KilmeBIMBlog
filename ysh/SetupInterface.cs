using Autodesk.Revit.UI;
namespace ysh
{
    using core;
    using ui;

    /// <summary>
    /// Setup whole plugins interface with tabs, panels, buttons,...
    /// </summary>
    public class SetupInterface
    {

        #region public methods

        /// <summary>
        /// Initializes all interface elements on custom created Revit tab.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Initialize(UIControlledApplication app)
        {
            // Create ribbon tab.
            string tabName = "Bimbyte";
            app.CreateRibbonTab(tabName);

            // Create the ribbon panels.
            var annotateCommandsPanel =  app.CreateRibbonPanel(tabName, "Annotation Commands");

            var familymanagerCommandPanel = app.CreateRibbonPanel(tabName, "FamilyManager Commands");


            #region annotate

            // Populate button data model.
            var TagWallButtonData = new RevitPushButtonDataModel
            {
                Label = "Tag Wall\nLayers",
                Panel = annotateCommandsPanel,
                Tooltip = "这是一些示例工具提示文本,\n此处需替代具体提示",
                CommandNamespacePath = TagWallLayersCommand.GetPath(),
                IconImageName = "icon_TagWallLayers_32x32.png",
                TooltipImageName = "tooltip_TagWallLayers_320x320.png"
            };

            // Create button from provided data.
            var TagWallButton = RevitPushButton.Create(TagWallButtonData);

            #endregion


            #region family manager

            var showFamilyManagerButtonData = new RevitPushButtonDataModel
            {
                Label = "Show Family\n Manager",
                Panel = familymanagerCommandPanel,
                Tooltip = "工具使用提示，\n可更换",
                CommandNamespacePath = ShowFamilyManagerCommand.GetPath(),
                IconImageName = "icon_showFamilyManager_32x32.png",
                TooltipImageName = "tooltip_showFamilyManager_320x320.png"
            };

            var showFamilyManagerButton = RevitPushButton.Create(showFamilyManagerButtonData);

            var hideFamilyManagerButtonData = new RevitPushButtonDataModel
            {
                Label = "Hide Family\n Manager",
                Panel = familymanagerCommandPanel,
                Tooltip = "工具使用提示，\n可更换",
                CommandNamespacePath = ShowFamilyManagerCommand.GetPath(),
                IconImageName = "icon_hideFamilyManager_32x32.png",
                TooltipImageName = "tooltip_hideFamilyManager_320x320.png"
            };

            var hideFamilyManagerButton = RevitPushButton.Create(hideFamilyManagerButtonData);
            #endregion
        }

        #endregion
    }
}
