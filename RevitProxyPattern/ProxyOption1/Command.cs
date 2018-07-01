using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;

namespace ProxyOption1
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        UIApplication uiapp;
        UIDocument uidoc;
        Application app;
        Document doc;

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;

            // Select walls
            var allWalls = new FilteredElementCollector(doc, doc.ActiveView.Id)
                .OfClass(typeof(Wall))
                .Cast<Wall>();

            if (allWalls.Count() < 3)
            {
                TaskDialog.Show("Error", "There aren't at least three visible walls on this view.");
                return Result.Cancelled;
            }

            var wallsOrigin = allWalls.Take(3);
            var wallOrigin = wallsOrigin.First();

            // Convert to proxy
            var wallProxy = WallProxy.Cast(wallOrigin);
            var wallsProxy = WallProxy.Cast(wallsOrigin);

            // Convert back to revit entity
            var wall = WallProxy.Cast(wallProxy);
            var walls = WallProxy.Cast(wallsProxy);

            // Show properties
            ShowProperties(wallsProxy, walls);

            return Result.Succeeded;
        }

        public void ShowProperties(IEnumerable<WallProxy> wallsProxy, IEnumerable<Wall> wallsBack)
        {
            var msg = string.Empty;

            msg += "----------------------------------------------------------";
            msg += "\nWallsProxy:\n";

            foreach (var wallP in wallsProxy)
            {
                msg += "\n" + wallP.Category.Name;
                msg += "\n" + wallP.Level.Name;
                msg += "\n" + wallP.WallType.Name;
                msg += "\n" + wallP.Curve.Length;
                msg += "\n";
            }

            msg += "----------------------------------------------------------";
            msg += "\nWalls:\n";

            foreach (var wallB in wallsBack)
            {
                msg += "\n" + wallB.Category.Name;
                msg += "\n" + wallB.WallType.Name;
                msg += "\n";
            }

            TaskDialog.Show("WallProxy", msg);
        }
    }
}
