using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyOption3
{
    public static class ProxyExtensions
    {
        public static TProxy ProxyCast<TRevitEntity, TProxy>(this TRevitEntity revitEntity)
            where TRevitEntity : Element
            where TProxy : BaseProxy<TRevitEntity>, new()
        {
            var proxy = new TProxy();
            proxy.RevitEntity = revitEntity;

            return proxy;
        }

        public static IEnumerable<TProxy> ProxyCast<TRevitEntity, TProxy>(this IEnumerable<TRevitEntity> revitEntities)
            where TRevitEntity : Element
            where TProxy : BaseProxy<TRevitEntity>, new()
        {
            return revitEntities.Select(re => re.ProxyCast<TRevitEntity, TProxy>());
        }

        public static TRevitEntity ProxyCast<TRevitEntity, TProxy>(this TProxy proxy)
            where TRevitEntity : Element
            where TProxy : BaseProxy<TRevitEntity>, new()
        {
            return proxy.RevitEntity;
        }

        public static IEnumerable<TRevitEntity> ProxyCast<TRevitEntity, TProxy>(this IEnumerable<TProxy> proxies)
            where TRevitEntity : Element
            where TProxy : BaseProxy<TRevitEntity>, new()
        {
            return proxies.Select(p => p.ProxyCast<TRevitEntity, TProxy>());
        }
    }
}
