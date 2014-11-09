using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoMapper;

namespace BLL.Infrastructure.Map
{
    public static class MapExtensions
    {
        public static TDest MapTo<TDest>(this object source)
        {
            //Mapper.CreateMap(source.GetType(), typeof(TDest));
            return (TDest)Mapper.Map(source, source.GetType(), typeof(TDest));
        }

        public static TDest MapTo<TDest, TSource>(this TSource source, TDest destination)
        {
            //Mapper.CreateMap(source.GetType(), typeof(TDest));
            return Mapper.Map(source, destination);
        }

        //public static void MapTo(this object source, object dest)
        //{
        //    return Mapper.Map()
        //}

        public static IEnumerable<TDest> MapEachTo<TDest>(this IEnumerable<object> source)
        {
            //Mapper.CreateMap(
            return source.MapTo<IEnumerable<TDest>>();
        }
    }
}