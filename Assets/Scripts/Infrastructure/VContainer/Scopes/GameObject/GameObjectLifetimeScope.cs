using VContainer;
using VContainer.Unity;

namespace Infrastructure.VContainer.Scopes.GameObject
{
    public class GameObjectLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder) => builder.RegisterComponent(gameObject);
    }
}