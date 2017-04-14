using System.Collections;
using UnityEngine;

namespace DK.Asteroids.GameLogic.Classes
{
    public static class Spawner
    {
        private static bool isSpawning = true;

        public static void StopSpawn()
        {
            isSpawning = false;
        }

        public static void ResumeSpawn()
        {
            isSpawning = true;
        }

        public static IEnumerator SpawnHazards(GameObject[] objects, GameObject[] sprites, float spawnRate)
        {
            while (isSpawning)
            {
                GameObject spawning = GameLoop.isSpriteRepresentation ? sprites[ChooseObjectToSpawn(sprites.Length)] : objects[ChooseObjectToSpawn(objects.Length)];
                Vector3 spawnPosition = GenerateSpawnPosition();
                Quaternion spawnRotation = Quaternion.identity;

                var newlyCreated = Object.Instantiate(spawning, spawnPosition, Utils.DefineRotation());
                yield return new WaitForSeconds(spawnRate);
            }
        }

        private static int ChooseObjectToSpawn(int maximum)
        {
            var rand = Random.Range(0, 100);

            if (rand < 85) return 0;
            return 1;
        }

        private static Vector3 GenerateSpawnPosition()
        {
            var position = new Vector3(Random.Range(-GameBoundary.width, GameBoundary.width),
                                        0f,
                                        Random.Range(-GameBoundary.height, GameBoundary.height));

            var playerPosition = Player.GetPlayerTransform();
            if((position - playerPosition.position).magnitude < 7)
            {
                position = (position - playerPosition.position).normalized * 7;
            }
            return position;
        }
    }
}
