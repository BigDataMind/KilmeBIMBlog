using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
namespace ysh.core
{
    public class SelectionFilterByCategory : ISelectionFilter
    {
        private string mCategory = "";

        public SelectionFilterByCategory(string category)
        {
            mCategory = category;
        }

        public bool AllowElement(Element elem)
        {
            //检查类别是否匹配
            if (elem.Category.Name == mCategory)
            {
                return true;
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
