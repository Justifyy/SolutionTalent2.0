using AutoMapper;
using System.Collections.Generic;

namespace OnlineTalent.Core.Extensions
{
    public static class MappingExtensions
    {
        public static TOut ToModelMapperDto<TIn, TOut>(this TIn entity)
        {
            // Mapper.Initialize(cfg => cfg.CreateMap<TIn, TOut>());
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TIn, TOut>());
            IMapper mapper = config.CreateMapper();
            TOut dto = mapper.Map<TOut>(entity);
            return dto;
        }

        public static IMapper ToModelMapper<TIn, TOut>()
        {
            // Mapper.Initialize(cfg => cfg.CreateMap<TIn, TOut>());
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TIn, TOut>());
            IMapper mapper = config.CreateMapper();
            // TOut dto = mapper.Map<TOut>(order);
            return mapper;
        }

        /// <summary> The to data model. </summary>
        /// <param name="entity"> The user. </param>
        /// <typeparam name="TIn"> Type to Convert </typeparam>
        /// <typeparam name="TOut"> Type to Return </typeparam>
        /// <returns> The <see cref="TOut"/> TypeReturn  </returns>
        public static TOut ToModel<TIn, TOut>(this TIn entity)
            where TIn : class
            where TOut : class
        {
            return (entity != null) ? Mapper.Map<TIn, TOut>(entity) : default(TOut);
        }

        /// <summary> The to data model. </summary>
        /// <param name="entityList"> The user list. </param>
        /// <typeparam name="TIn"> Type to Convert </typeparam>
        /// <typeparam name="TOut"> Type to Return </typeparam>
        /// <returns> The <see cref="List{T}"/>. </returns>
        public static List<TOut> ToModel<TIn, TOut>(this List<TIn> entityList)
            where TIn : class
            where TOut : class
        {
            // Mapper.Initialize(cfg => cfg.CreateMap<TIn, TOut>());
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TIn, TOut>());
            IMapper mapper = config.CreateMapper();

            return (entityList != null) ? mapper.Map<List<TOut>>(entityList) : default(List<TOut>);
        }
    }
}
