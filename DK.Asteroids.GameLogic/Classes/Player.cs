using System.Collections;
using UnityEngine;

namespace DK.Asteroids.GameLogic.Classes
{
    public sealed class Player
    {
        public int currentLaserShots;

        private float thrustSpeed;
        private float rotateSpeed;

        private float bulletFireRate;
        private float addingLaserRate;

        private float nextFire = 0f;
        private float nextLaserAdd = 0f;

        private GameObject explosion;

        private static GameObject obj;
        private static Rigidbody rb;

        private GameObject thisObject;
        private GameObject thisObjectSprite;

        private bool isAlive;

        private static Player player = null;

        private Player()
        {
            isAlive = true;
            currentLaserShots = 0;
        }

        public static void SetPlayerToNull()
        {
            player = null;
            obj = null;
            rb = null;
        }

        public static Player GetPlayer()
        {
            if (player == null)
            {
                player = new Player();
            }
            return player;
        }

        public static Player GetPlayer(GameObject sprite, GameObject model)
        {
            if (player == null)
            {
                obj = Object.Instantiate(GameLoop.isSpriteRepresentation ? sprite : model, Vector3.zero, Utils.DefineRotation());
                rb = obj.GetComponent<Rigidbody>();
                player = new Player();
            }
            return player;
        }

        public void SetPlayerSettings(GameObject thisObject, GameObject thisObjectSprite, float thrustSpeed, float rotateSpeed, float bulletFireRate, float addingLaserRate, GameObject explosion)
        {
            this.thisObject = thisObject;
            this.thisObjectSprite = thisObjectSprite;
            this.thrustSpeed = thrustSpeed;
            this.rotateSpeed = rotateSpeed;
            this.bulletFireRate = bulletFireRate;
            this.addingLaserRate = addingLaserRate;
            this.explosion = explosion;
        }

        public void ChangeRepresentation()
        {
            if (obj != null && rb != null)
            {
                GameObject newlyCreated;
                if (GameLoop.isSpriteRepresentation)
                {
                    newlyCreated = Object.Instantiate(thisObjectSprite, obj.transform.position, Utils.DefineRotation());
                }
                else newlyCreated = Object.Instantiate(thisObject, obj.transform.position, Utils.DefineRotation());

                var velocity = rb.velocity;
                var angle = rb.rotation;
                Object.Destroy(obj);
                obj = newlyCreated;
                rb = obj.GetComponent<Rigidbody>();
                rb.velocity = velocity;
                rb.rotation = Quaternion.Euler(GameLoop.isSpriteRepresentation ? 90f : 0f, angle.eulerAngles.y, angle.eulerAngles.z);
            }
        }

        public static Transform GetPlayerTransform()
        {
            if (obj == null) return null;
            return obj.transform;
        }

        public void OutOfBoundary()
        {
            if (Utils.IsWithinBoundary(obj)) return;
            Utils.ReturnToBoundary(obj);
        }

        public void MovePlayer(float thrust, float turning)
        {
            rb.AddForce((GameLoop.isSpriteRepresentation ? GetPlayerTransform().up : GetPlayerTransform().forward) * thrust * thrustSpeed);

            rb.angularVelocity = new Vector3(0f, turning * rotateSpeed, 0f);

            rb.position = new Vector3(Mathf.Clamp(rb.position.x, -GameBoundary.width, GameBoundary.width),
                                        0.0f,
                                        Mathf.Clamp(rb.position.z, -GameBoundary.height, GameBoundary.height));
        }

        public bool TryShootBullet()
        {
            if (isAlive && Time.time > nextFire)
            {
                nextFire = Time.time + bulletFireRate;
                return true;
            }
            return false;
        }

        public bool TryShootLaser()
        {
            if (isAlive && currentLaserShots > 0)
            {
                --currentLaserShots;
                return true;
            }
            return false;
        }

        public void UpdateAddLaser()
        {
            if (isAlive &&Time.time > nextLaserAdd)
            {
                nextLaserAdd = Time.time + addingLaserRate;
                currentLaserShots++;
            }
        }

        public void CollideWithDangerous(GameObject gameObject, float delay)
        {
            obj.GetComponent<AudioSource>().Play();
            Object.Destroy(Object.Instantiate(explosion, obj.transform.position, Quaternion.identity), explosion.GetComponent<ParticleSystem>().main.duration);
            Object.Destroy(gameObject, delay);

            if (obj.GetComponent<MeshRenderer>()) obj.GetComponent<MeshRenderer>().enabled = false;
            else obj.GetComponent<SpriteRenderer>().enabled = false;

            obj.GetComponent<CapsuleCollider>().enabled = false;
            isAlive = false;

            GameLoop.GameOver();
        }
    }
}
