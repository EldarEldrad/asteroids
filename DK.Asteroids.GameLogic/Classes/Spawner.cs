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
            int side = Random.Range(0, 4);

            switch(side)
            {
                case 0:
                    return new Vector3(Random.Range(-GameBoundary.width, GameBoundary.width),
                                        0f,
                                        GameBoundary.height * 1.2f);
                case 1:
                    return new Vector3(GameBoundary.width * 1.2f,
                                        0f,
                                        Random.Range(-GameBoundary.height, GameBoundary.height));
                case 2:
                    return new Vector3(Random.Range(-GameBoundary.width, GameBoundary.width),
                                        0f,
                                        - GameBoundary.height * 1.2f);
                case 3:
                    return new Vector3(- GameBoundary.width * 1.2f,
                                        0f,
                                        Random.Range(-GameBoundary.height, GameBoundary.height));
                default:
                    return Vector3.zero;
            }
        }
    }
}
