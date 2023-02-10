using Autodesk.Revit.UI;

namespace ysh.core
{
    public class Message
    {
        #region public methods

        /// <summary>
        /// 显示指定的消息。
        /// </summary>
        /// <param name="message">在此窗口中显示的消息。</param>
        /// <param name="type">消息类型</param>
        public static void Display(string message,WindowType type)
        {
            string title = "";

            var icon = TaskDialogIcon.TaskDialogIconNone;

            switch (type)
            {
                case WindowType.Inforamtion:
                    title = "INFORMATION";
                    icon = TaskDialogIcon.TaskDialogIconInformation;
                    break;
                case WindowType.Warning:
                    title = "WARNING";
                    icon = TaskDialogIcon.TaskDialogIconWarning;
                    break;
                case WindowType.Error:
                    title = "ERROR";
                    icon = TaskDialogIcon.TaskDialogIconError;
                    break;
                default:
                    break;
            }

            //构造窗口以显示指定的消息
            var window = new TaskDialog(title)
            {
                MainContent = message,
                MainIcon = icon,
                CommonButtons = TaskDialogCommonButtons.Ok
            };
            window.Show();

        }
        #endregion
    }
}
