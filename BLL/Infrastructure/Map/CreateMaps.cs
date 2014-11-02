using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

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
                        //typeof ()
                        //    .Assembly
                        //    .GetTypes()
                        //    .Where(x => typeof (Profile).IsAssignableFrom(x))
                        //    .Each(x => Mapper.AddProfile((Profile) Activator.CreateInstance(x)));

                        //_isRegistered = true;
                    }
                }
        }
    }
}