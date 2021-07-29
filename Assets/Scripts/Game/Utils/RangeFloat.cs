using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Utils
{
    [Serializable]
    public struct RangeFloat
    {
        public float min;
        public float max;
    }

    public static class RangeFloatExtensions
    {
        public static Vector2 RandomVector2(this RangeFloat rangeFloat)
        {
            return new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized * Random.Range(rangeFloat.min, rangeFloat.max);
        }

        public static float RandomFloat(this RangeFloat rangeFloat)
        {
            return Random.Range(rangeFloat.min, rangeFloat.max);
        }
    } 
}