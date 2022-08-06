using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterGame
{
    public class Spawner : MonoBehaviour
    {

        public GameObject[] EnemyPrefabs;

        public float SpawnRate;
        public int SpawnAmount;
        private float spawnTimer;


        // Start is called before the first frame update
        void Start()
        {
            spawnTimer = SpawnRate;
        }

        // Update is called once per frame
        void Update()
        {
            if (spawnTimer > 0f)
            {
                spawnTimer -= Time.deltaTime;
            }
            else
            {
                spawnTimer = SpawnRate;
                for (int i = 0; i < SpawnAmount; i++)
                    SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            int enemyId = UnityEngine.Random.Range(0, EnemyPrefabs.Length);
            Vector2 randInsideCircle = UnityEngine.Random.insideUnitCircle * 3f;

            Vector3 randPos = transform.position;
            randPos.x += randInsideCircle.x;
            randPos.z += randInsideCircle.y;
            randPos.y = 1f;

            GameObject go = Instantiate(EnemyPrefabs[enemyId], randPos, Quaternion.identity);
        }
    }
}
