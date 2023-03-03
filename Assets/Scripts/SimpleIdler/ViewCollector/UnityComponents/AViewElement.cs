using Leopotam.Ecs;
using UnityEngine;

namespace SimpleIdler.ViewCollector.UnityComponents
{
    public abstract class AViewElement : MonoBehaviour
    {
        public abstract void OnSpawn(EcsEntity entity, EcsWorld world);
    }
}