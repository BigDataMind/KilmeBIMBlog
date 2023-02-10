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

            //��鵱ǰ���ļ�����Ϊ��Ŀ��������
            if (doc.IsFamilyDocument)
            {
                Message.Display("���������ĵ�ʹ�õ�ǰ����", WindowType.Warning);
                return Result.Cancelled;
            }

            var activeView = uidoc.ActiveView;

            //����Ƿ�����ڵ�ǰ�����Ŀ��ͼ�д����ı�ע��
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

            //�������ṩ�����ݵ��ռ�����
            var userInfo = new TagWallLayersCommandData();
            if (!canCreateTextNoteInView)
            {
                Message.Display("�޷��ڵ�ǰ��ͼ�д����ı�ע��", WindowType.Error);
                return Result.Cancelled;
            }

            //�Ӵ��ں���ʾ�Ի����л�ȡ�û��ṩ����Ϣ
            using (var window = new TagWallLayersForm(uidoc))
            {
                window.ShowDialog();

                if (window.DialogResult == DialogResult.Cancel)
                    return Result.Cancelled;

                userInfo = window.GetInformation();
            }

            //(1)Ҫ���û�ѡ��һ�����ǽ ����д��������ϵͳ��Ҫ��
            var selectionReference = uidoc.Selection.PickObject(ObjectType.Element, new SelectionFilterByCategory("ǽ"), "ѡ��һ�����ǽ.");
            var selectionElement = doc.GetElement(selectionReference);

            //(2) 

            var wall = selectionElement as Wall;

            if (wall.IsStackedWall)
            {
                Message.Display("��ѡ���ǽ�Ƕѻ�ǽ�����\n���֧������", WindowType.Warning);
                return Result.Cancelled;
            }

            //����ǽ���б�
            var layers = wall.WallType.GetCompoundStructure().GetLayers();

            //���ı�ע�ͻ�ȡͼ����Ϣ
            var msg = new StringBuilder();

            foreach (var layer in layers)
            {
                var material = doc.GetElement(layer.MaterialId) as Material;

                msg.AppendLine();

                if (userInfo.Function)
                {
                    msg.Append(layer.Function.ToString());
                }

                //�������Ƿ�����ǽ�帴�Ͻṹ�еĲ��ϡ�
                if (userInfo.Name)
                {
                    if (material != null)
                        msg.Append(" " + material.Name);
                    else
                        msg.Append("  <by category>");
                }

                //����λת��Ϊ���ơ�
                //Ĭ������£�Revit ʹ��Ӣ�Ƶ�λ
                if (userInfo.Thickness)
                {
                    msg.Append(" " + LengthUnitConverter.ConvertToMetric(layer.Width, userInfo.UnitType, userInfo.Decimals).ToString());
                }
            }

            //�����ı�ע��
            var textNoteOptions = new TextNoteOptions
            {
                HorizontalAlignment = HorizontalTextAlignment.Left,
                TypeId = userInfo.TextTypeId
            };

            //�� Revit �ĵ������Դ����µ��ı�ע��ͼԪ��
            using (var transaction = new Transaction(doc))
            {
                transaction.Start("���ǽͼ������");

                var pt = new XYZ();

                //������ͼƽ�棬���û��������������ͼ��ѡȡ�㡣
                if (activeView.ViewType == ViewType.Elevation || activeView.ViewType == ViewType.Section)
                {
                    var plane = Plane.CreateByNormalAndOrigin(activeView.ViewDirection, activeView.Origin);
                    var sketchPlane = SketchPlane.Create(doc, plane);

                    //Ҫ���û�ѡȡ�ı�ע��Ԫ�ص�λ�õ�
                    pt = uidoc.Selection.PickPoint("ѡȡ�ı�ע��λ�õ�");
                }
                else
                {
                    pt = uidoc.Selection.PickPoint("ѡȡ�ı�ע��λ�õ�");
                }

                //ʹ�õ�ǰ���ͼ���û�ָ�����ϵ�ǽͼ����Ϣ�����ı�ע�͡�
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
