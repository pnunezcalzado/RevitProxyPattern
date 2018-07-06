using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyOption3
{
    public class WallProxy : BaseProxy<Wall>
    {
        // Example property mapping
        public WallType WallType { get { return RevitEntity.WallType; } }

        // Example calculated property
        public Curve Curve { get { return (RevitEntity.Location as LocationCurve).Curve; } }
    }
}
