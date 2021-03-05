using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        // Eğer varsa bellekten cache'den ekler. Yoksa database'den alıp ekler ve cache'e ekler.
        public override void Intercept(IInvocation invocation)
        {
            // FullName ile NameSpace+Class İsmi alınır. Method.Name ile metot adı alınır.
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            // Parametreler alınır
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            // eğer cache'de varsa bunu yapar
            if (_cacheManager.IsAdd(key))
            {
                // cacheManager'daki geti kullanır.
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            // Yoksa database'den çekip sürdürür.
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
