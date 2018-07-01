using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyOption1
{
    public class WallProxy : ElementProxy<Wall, WallProxy>
    {
        // Example property mapping
        public WallType WallType { get { return RevitEntity.WallType; } }

        // Example calculated property
        public Curve Curve { get { return (RevitEntity.Location as LocationCurve).Curve;} }
    }
}
