using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Internal;

namespace BLL.Infrastructure.Map
{
    public static class CreateMaps
    {
        private static readonly object LockObject = new object();
        private static bool _isRegistered;

        public static void Register()
        {
            if (_isRegistered == false)
                lock (LockObject)
                {
                    if (_isRegistered == false)
                    {
                        var types = typeof (SomeClass)
                            .Assembly
                            .GetTypes()
                            .Where(x => typeof (Profile).IsAssignableFrom(x))
                            .OrderBy(x => x.Name)
                            .ToList();
                        types.Each(x => Mapper.AddProfile((Profile) Activator.CreateInstance(x)));
                        _isRegistered = true;
                    }
                }
        }
    }
}