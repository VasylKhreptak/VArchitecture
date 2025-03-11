using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.VContainer.Tools
{
    [DefaultExecutionOrder(-4999)]
    public class AutoInject : MonoBehaviour
    {
        private IObjectResolver _resolver;

        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
            _injected = true;
        }

        private bool _injected;

        private void Awake()
        {
            if (_injected)
                return;

            _resolver.InjectGameObject(gameObject);
        }
    }
}