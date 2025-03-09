using UnityEngine;
using Zenject;

namespace Infrastructure.Tools
{
    public static class InstallerTools
    {
        public static TOut SelectImplementation<TOut, TAndroid, TIOS, TDefault>(InjectContext injectContext)
            where TAndroid : TOut
            where TIOS : TOut
            where TDefault : TOut
        {
            TOut implementation;

            if (Application.platform == RuntimePlatform.Android)
                implementation = injectContext.Container.Instantiate<TAndroid>();
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                implementation = injectContext.Container.Instantiate<TIOS>();
            else
                implementation = injectContext.Container.Instantiate<TDefault>();

            return implementation;
        }
    }
}