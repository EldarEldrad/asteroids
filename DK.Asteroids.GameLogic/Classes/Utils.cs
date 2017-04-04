using UnityEngine;

namespace DK.Asteroids.GameLogic.Classes
{
    internal static class Utils
    {
        public static Quaternion DefineRotation()
        {
            if (GameLoop.isSpriteRepresentation)
            {
                return Quaternion.Euler(90f, 0f, 0f);
            }
            return Quaternion.identity;
        }

        public static Quaternion DefineRotation(Transform transform, float y = 0f)
        {
            if (GameLoop.isSpriteRepresentation)
            {
                return Quaternion.Euler(90f, y, 0f);
            }
            return transform.rotation;
        }
    }
}
