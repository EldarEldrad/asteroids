using UnityEngine;

namespace DK.Asteroids.GameLogic.Classes
{
    public class Projectile
    {
        private float speed;
        private int projectileType;

        private GameObject obj;

        public Projectile(GameObject obj, float speed, int projectileType)
        {
            this.obj = obj;
            this.speed = speed;
            this.projectileType = projectileType;
        }

        public void MoveProjectile(Rigidbody rb)
        {
            if (GameLoop.isSpriteRepresentation) rb.velocity = rb.gameObject.transform.up * speed;

            else rb.velocity = rb.gameObject.transform.forward * speed;
        }

        public void OnCollideTriggerEnter(Collider other)
        {
            if (projectileType == 0 && other.tag != Constants.Tags.Boundary && other.tag != Constants.Tags.Bullet)
            {
                Object.Destroy(obj);
            }
        }
    }
}
