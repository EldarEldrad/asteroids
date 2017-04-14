using UnityEngine;

namespace DK.Asteroids.GameLogic.Classes
{
    public sealed class Asteroid
    {
        private GameObject explosion;

        private GameObject smallerAsteroid;
        private GameObject smallerAsteroidSprite;
        private int pointsValue;

        private GameObject obj;

        private GameObject thisObject;
        private GameObject thisObjectSprite;

        public Asteroid(GameObject thisObject, GameObject thisObjectSprite, GameObject obj, GameObject explosion, GameObject smallerAsteroid, GameObject smallerAsteroidSprite, int pointsValue)
        {
            this.thisObject = thisObject;
            this.thisObjectSprite = thisObjectSprite;
            this.explosion = explosion;
            this.obj = obj;
            this.smallerAsteroid = smallerAsteroid;
            this.smallerAsteroidSprite = smallerAsteroidSprite;
            this.pointsValue = pointsValue;
        }

        public void ActionOnCollide(Collider other)
        {
            switch (other.tag)
            {
                case Constants.Tags.Bullet:
                    BreakInPieces();
                    break;
                case Constants.Tags.Laser:
                case Constants.Tags.Player:
                    DestroyAsteroid();
                    break;
            }
        }

        public void OutOfBoundary()
        {
            if (Utils.IsWithinBoundary(obj)) return;
            Utils.ReturnToBoundary(obj);
        }

        public void ChangeRepresentation()
        {
            GameObject newlyCreated;
            if (GameLoop.isSpriteRepresentation)
            {
                newlyCreated = Object.Instantiate(thisObjectSprite, obj.transform.position, Utils.DefineRotation());
            }
            else newlyCreated = Object.Instantiate(thisObject, obj.transform.position, Utils.DefineRotation());

            var velocity = obj.GetComponent<Rigidbody>().velocity;
            Object.Destroy(obj);
            obj = newlyCreated;
            obj.GetComponent<Rigidbody>().velocity = velocity;
        }

        private void BreakInPieces()
        {
            if (smallerAsteroid)
            {
                Object.Instantiate(GameLoop.isSpriteRepresentation ? smallerAsteroidSprite : smallerAsteroid,
                                    obj.transform.position,
                                    Utils.DefineRotation());
                Object.Instantiate(GameLoop.isSpriteRepresentation ? smallerAsteroidSprite : smallerAsteroid,
                                    obj.transform.position,
                                    Utils.DefineRotation());
            }
            DestroyAsteroid();
        }

        private void DestroyAsteroid()
        {
            var delay = obj.GetComponent<AudioSource>().clip.length;
            obj.GetComponent<AudioSource>().Play();
            if (!GameLoop.isStopped) Scores.AddScore(pointsValue);
            Object.Destroy(Object.Instantiate(explosion, obj.transform.position, Quaternion.identity), explosion.GetComponent<ParticleSystem>().main.duration);
            Object.Destroy(obj, delay);

            if (obj.GetComponent<MeshRenderer>()) obj.GetComponent<MeshRenderer>().enabled = false;
            else obj.GetComponent<SpriteRenderer>().enabled = false;

            obj.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
