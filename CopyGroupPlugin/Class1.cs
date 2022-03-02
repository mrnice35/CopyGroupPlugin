using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyGroupPlugin
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CopuGroup : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;//Доступ к документу
            Document doc = uiDoc.Document;
           Reference reference = uiDoc.Selection.PickObject(ObjectType.Element, "Выберите группу объектов");//Выбор группы
            Element element = doc.GetElement(reference);
            Group group = element as Group;//преобразовали к типу група

            XYZ point = uiDoc.Selection.PickPoint("Выберите точку");// выбор точки

            Transaction transaction = new Transaction(doc);
            transaction.Start("Копирование группы объектов");
            doc.Create.PlaceGroup(point, group.GroupType);
            transaction.Commit();

            return Result.Succeeded;
        }
    }
}
 