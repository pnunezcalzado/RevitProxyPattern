using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyOption3
{
    public abstract class ElementProxy<TRevitEntity>
    where TRevitEntity : Element
    {
        public TRevitEntity RevitEntity { get; set; }

        // Example property mapping
        public Category Category { get { return RevitEntity.Category; } }

        // Example calculated property
        public Level Level { get { return RevitEntity.Document.GetElement(RevitEntity.LevelId) as Level; } }
    }

}
