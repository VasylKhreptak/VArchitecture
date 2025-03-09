using Infrastructure.Data.Models.Static.Data;
using Infrastructure.Data.SaveLoad.Core;

namespace Infrastructure.Data.Models.Static.Core
{
    public interface IStaticDataModel : ILoadHandler
    {
        public Config Config { get; }
        public Balance Balance { get; }
    }
}