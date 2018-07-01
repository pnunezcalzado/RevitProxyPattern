using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyOption2
{
    public class WallProxy : ElementProxy<Wall>
    {
        // Operators
        public static implicit operator WallProxy(Wall wall)
        {
            var wallProxy = new WallProxy();
            wallProxy.RevitEntity = wall;

            return wallProxy;
        }

        public static explicit operator Wall(WallProxy wallProxy)
        {
            return wallProxy.RevitEntity;
        }

        // Example property mapping
        public WallType WallType { get { return RevitEntity.WallType; } }

        // Example calculated property
        public Curve Curve { get { return (RevitEntity.Location as LocationCurve).Curve; } }
    }
}
