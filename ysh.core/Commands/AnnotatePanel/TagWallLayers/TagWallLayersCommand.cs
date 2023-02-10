#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

#endregion

namespace ysh.core
{
    [Transaction(TransactionMode.Manual)]
    public class TagWallLayersCommand : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;

            //检查当前打开文件类型为项目，而非族
            if (doc.IsFamilyDocument)
            {
                Message.Display("不能在族文档使用当前命令", WindowType.Warning);
                return Result.Cancelled;
            }

            var activeView = uidoc.ActiveView;

            //检查是否可以在当前活动的项目视图中创建文本注释
            bool canCreateTextNoteInView = false;
            switch (activeView.ViewType)
            {
                case ViewType.FloorPlan:
                    canCreateTextNoteInView = true;
                    break;
                case ViewType.CeilingPlan:
                    canCreateTextNoteInView = true;
                    break;
                case ViewType.Elevation:
                    canCreateTextNoteInView = true;
                    break;
                case ViewType.Section:
                    canCreateTextNoteInView = true;
                    break;
                case ViewType.Detail:
                    canCreateTextNoteInView = true;
                    break;
                
            }

            //窗口中提供的数据的收集器。
            var userInfo = new TagWallLayersCommandData();
            if (!canCreateTextNoteInView)
            {
                Message.Display("无法在当前视图中创建文本注释", WindowType.Error);
                return Result.Cancelled;
            }

            //从窗口和显示对话框中获取用户提供的信息
            using (var window = new TagWallLayersForm(uidoc))
            {
                window.ShowDialog();

                if (window.DialogResult == DialogResult.Cancel)
                    return Result.Cancelled;

                userInfo = window.GetInformation();
            }

            //(1)要求用户选择一面基本墙 这种写法对语言系统有要求
            var selectionReference = uidoc.Selection.PickObject(ObjectType.Element, new SelectionFilterByCategory("墙"), "选择一面基本墙.");
            var selectionElement = doc.GetElement(selectionReference);

            //(2) 

            var wall = selectionElement as Wall;

            if (wall.IsStackedWall)
            {
                Message.Display("您选择的墙是堆积墙的类别。\n命令不支持它。", WindowType.Warning);
                return Result.Cancelled;
            }

            //访问墙层列表
            var layers = wall.WallType.GetCompoundStructure().GetLayers();

            //以文本注释获取图层信息
            var msg = new StringBuilder();

            foreach (var layer in layers)
            {
                var material = doc.GetElement(layer.MaterialId) as Material;

                msg.AppendLine();

                if (userInfo.Function)
                {
                    msg.Append(layer.Function.ToString());
                }

                //检查材料是否附着在墙体复合结构中的层上。
                if (userInfo.Name)
                {
                    if (material != null)
                        msg.Append(" " + material.Name);
                    else
                        msg.Append("  <by category>");
                }

                //将单位转换为公制。
                //默认情况下，Revit 使用英制单位
                if (userInfo.Thickness)
                {
                    msg.Append(" " + LengthUnitConverter.ConvertToMetric(layer.Width, userInfo.UnitType, userInfo.Decimals).ToString());
                }
            }

            //创建文本注释
            var textNoteOptions = new TextNoteOptions
            {
                HorizontalAlignment = HorizontalTextAlignment.Left,
                TypeId = userInfo.TextTypeId
            };

            //打开 Revit 文档事务以创建新的文本注释图元。
            using (var transaction = new Transaction(doc))
            {
                transaction.Start("标记墙图层命令");

                var pt = new XYZ();

                //构建草图平面，供用户在立面或剖面视图中选取点。
                if (activeView.ViewType == ViewType.Elevation || activeView.ViewType == ViewType.Section)
                {
                    var plane = Plane.CreateByNormalAndOrigin(activeView.ViewDirection, activeView.Origin);
                    var sketchPlane = SketchPlane.Create(doc, plane);

                    //要求用户选取文本注释元素的位置点
                    pt = uidoc.Selection.PickPoint("选取文本注释位置点");
                }
                else
                {
                    pt = uidoc.Selection.PickPoint("选取文本注释位置点");
                }

                //使用当前活动视图中用户指定点上的墙图层信息创建文本注释。
                var textNote = TextNote.Create(doc, activeView.Id, pt, msg.ToString(), textNoteOptions);

                transaction.Commit();
            }

            return Result.Succeeded;
        }

        /// <summary>
        /// Gets the full namespace path to this command.
        /// </summary>
        /// <returns></returns>
        public static string GetPath()
        {
            // Return constructed namespace path.
            return typeof(TagWallLayersCommand).Namespace + "." + nameof(TagWallLayersCommand);
        }
    }
}
