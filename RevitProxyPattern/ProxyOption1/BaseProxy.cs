using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyOption1
{
    public abstract class BaseProxy<TRevitEntity, TProxy>
        where TRevitEntity : Element
        where TProxy : BaseProxy<TRevitEntity, TProxy>, new()
    {
        public TRevitEntity RevitEntity { get; set; }

        // Conversion methods
        public static TProxy Cast(TRevitEntity revitEntity)
        {
            var proxy = new TProxy();
            proxy.RevitEntity = revitEntity;

            return proxy;
        }

        public static IEnumerable<TProxy> Cast(IEnumerable<TRevitEntity> revitEntities)
        {
            return revitEntities.Select(re => Cast(re));
        }

        public static TRevitEntity Cast(TProxy proxy)
        {
            return proxy.RevitEntity;
        }

        public static IEnumerable<TRevitEntity> Cast(IEnumerable<TProxy> proxies)
        {
            return proxies.Select(p => Cast(p));
        }

        // Example property mapping
        public Category Category { get { return RevitEntity.Category; } }

        // Example calculated property
        public Level Level { get { return RevitEntity.Document.GetElement(RevitEntity.LevelId) as Level; } }
    }
}
