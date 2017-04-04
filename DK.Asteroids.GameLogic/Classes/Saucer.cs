using System.Collections;
using UnityEngine;

namespace DK.Asteroids.GameLogic.Classes
{
    public sealed class Saucer
    {
        private float rateOfFire;
        private float movementSpeed;

        private int pointsValue;

        private GameObject explosion;
        private AudioClip explosionAudio;

        private bool isAlive;

        private GameObject obj;

        private GameObject thisObject;
        private GameObject thisObjectSprite;

        public Saucer(GameObject thisObject, GameObject thisObjectSprite, GameObject obj, float rateOfFire, float movementSpeed, int pointsValue, GameObject explosion, AudioClip explosionAudio)
        {
            isAlive = true;
            this.thisObject = thisObject;
            this.thisObjectSprite = thisObjectSprite;
            this.obj = obj;
            this.rateOfFire = rateOfFire;
            this.movementSpeed = movementSpeed;
            this.pointsValue = pointsValue;
            this.explosion = explosion;
            this.explosionAudio = explosionAudio;
        }

        public void ChangeRepresentation()
        {
            GameObject newlyCreated;
            if (GameLoop.isSpriteRepresentation)
            {
                newlyCreated = Object.Instantiate(thisObjectSprite, obj.transform.position, Utils.DefineRotation());
            }
            else newlyCreated = Object.Instantiate(thisObject, obj.transform.position, Utils.DefineRotation());

            Object.Destroy(obj);
            obj = newlyCreated;

        }

        public IEnumerator ShotAtPlayer(GameObject projectile, GameObject projectileSprite, Transform spawnPosition)
        {
            yield return new WaitForSeconds(rateOfFire / 2.0f);

            while (isAlive)
            {
                obj.GetComponent<AudioSource>().Play();
                Object.Instantiate(GameLoop.isSpriteRepresentation ? projectileSprite : projectile,
                    spawnPosition.position, Utils.DefineRotation(spawnPosition, spawnPosition.rotation.eulerAngles.y));
                yield return new WaitForSeconds(rateOfFire);
            }
        }

        public void AlignToTarget(Transform target)
        {
            if (target == null) return;

            Vector3 lineToTarget = target.position - obj.transform.position;
            var angleToTarget = Mathf.Atan2(lineToTarget.x, lineToTarget.z) * Mathf.Rad2Deg;

            if (GameLoop.isSpriteRepresentation) obj.transform.eulerAngles = new Vector3(90f, angleToTarget, 0f); 
            else obj.transform.eulerAngles = new Vector3(0f, angleToTarget, 0f);
        }

        public void MoveToTarget(Transform target)
        {
            if (target == null) return;

            Vector3 lineToTarget = target.position - obj.transform.position;
            obj.GetComponent<Rigidbody>().velocity = lineToTarget * movementSpeed;
        }

        public void ActionOnCollide(Collider other)
        {
            if (obj && other)
            {
                switch (other.tag)
                {
                    case Constants.Tags.Bullet:
                    case Constants.Tags.Laser:
                    case Constants.Tags.Player:
                        isAlive = false;
                        obj.GetComponent<AudioSource>().clip = explosionAudio;
                        obj.GetComponent<AudioSource>().Play();
                        Object.Destroy(Object.Instantiate(explosion, obj.transform.position, Quaternion.identity), explosion.GetComponent<ParticleSystem>().main.duration);
                        if (!GameLoop.isStopped) Scores.AddScore(pointsValue);
                        Object.Destroy(obj, explosionAudio.length);

                        if (obj.GetComponent<MeshRenderer>()) obj.GetComponent<MeshRenderer>().enabled = false;
                        else obj.GetComponent<SpriteRenderer>().enabled = false;

                        obj.GetComponent<CapsuleCollider>().enabled = false;
                        break;
                }
            }
        }
    }
}
