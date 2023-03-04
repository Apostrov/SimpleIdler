using Leopotam.Ecs;
using UnityEngine;

namespace SimpleIdler.ViewCollector.Systems
{
    public class ViewCollectorInit : IEcsInitSystem
    {
        private EcsWorld _world;

        public void Init()
        {
            var viewElements = GameObject.FindObjectsOfType<UnityComponents.AViewElement>();
            foreach (var viewElement in viewElements)
            {
                EcsEntity entity = _world.NewEntity();
                ref var view = ref entity.Get<Components.UnityView>();
                view.GameObject = viewElement.gameObject;
                viewElement.OnSpawn(entity, _world);
            }
        }
    }
}