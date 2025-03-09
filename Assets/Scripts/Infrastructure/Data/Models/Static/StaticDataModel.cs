using Infrastructure.Data.Models.Static.Core;
using Infrastructure.Data.Models.Static.Data;
using UnityEngine;

namespace Infrastructure.Data.Models.Static
{
    public class StaticDataModel : IStaticDataModel
    {
        private const string GameConfigPath = "StaticData/GameConfig";
        private const string GameBalancePath = "StaticData/GameBalance";

        public Config Config { get; private set; }
        public Balance Balance { get; private set; }

        public void Load()
        {
            Config = Resources.Load<Config>(GameConfigPath);
            Balance = Resources.Load<Balance>(GameBalancePath);
        }
    }
}