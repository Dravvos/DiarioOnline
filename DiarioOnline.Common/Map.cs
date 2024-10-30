using Mapster;

namespace DiarioOnline.Common
{
    public static class Map<D>
    {
        public static D Convert<O>(O o)
        {
            return o.Adapt<D>();
        }
        public static D Convert<O>(O o, TypeAdapterConfig config)
        {
            return o.Adapt<D>(config);
        }
    }
}
