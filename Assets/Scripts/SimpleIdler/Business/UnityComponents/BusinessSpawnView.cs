using Leopotam.Ecs;
using SimpleIdler.ViewCollector.UnityComponents;
using UnityEngine;

namespace SimpleIdler.Business.UnityComponents
{
    public class BusinessSpawnView : AViewElement
    {
        [SerializeField] private RectTransform _transform;
        
        public override void OnSpawn(EcsEntity entity, EcsWorld world)
        {
            entity.Get<Components.BusinessSpawnTransform>().Transform = _transform; 
        }
    }
}