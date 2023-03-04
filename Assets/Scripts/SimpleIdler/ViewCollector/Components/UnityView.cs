using Leopotam.Ecs;
using UnityEngine;

namespace SimpleIdler.ViewCollector.Components
{
    public struct UnityView : IEcsAutoReset<UnityView>
    {
        public GameObject GameObject;

        public void AutoReset(ref UnityView c)
        {
            if (c.GameObject != null)
                Object.Destroy(c.GameObject);
            c.GameObject = null;
        }
    }
}