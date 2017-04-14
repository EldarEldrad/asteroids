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

        public static bool IsWithinBoundary(GameObject obj)
        {
            if (Mathf.Abs(obj.transform.position.x) <= GameBoundary.width && Mathf.Abs(obj.transform.position.z) <= GameBoundary.height) return true;
            return false;
        }

        public static void ReturnToBoundary(GameObject obj)
        {
            if (obj.transform.position.x > GameBoundary.width) obj.transform.position = new Vector3(-GameBoundary.width, obj.transform.position.y, obj.transform.position.z);

            if (obj.transform.position.x < -GameBoundary.width) obj.transform.position = new Vector3(GameBoundary.width, obj.transform.position.y, obj.transform.position.z);

            if (obj.transform.position.z > GameBoundary.height) obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, -GameBoundary.height);

            if (obj.transform.position.z < -GameBoundary.height) obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, GameBoundary.height);
        }
    }
}
