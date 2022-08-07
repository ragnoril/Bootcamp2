using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterGame
{

    public class ShootingEnemyController : EnemyController
    {
        public GameObject BulletPrefab;
        public Transform BulletSpawnPoint;

        public float RateOfFire;
        private float fireCooldown;

        public float ShootingDistance;
        public bool CanShoot = false;


        private void FixedUpdate()
        {
            if (isGameOver)
            {
                rigidbody.velocity = Vector3.zero;
                return;
            }

            float angleBetween = 270f - Mathf.Atan2(transform.position.z - Target.transform.position.z, transform.position.x - Target.transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, angleBetween, 0f));

            if (Vector3.Distance(transform.position, Target.transform.position) > ShootingDistance)
            {
                CanShoot = false;
                Vector3 vel = transform.forward * MoveSpeed * Time.fixedDeltaTime;
                vel.y = rigidbody.velocity.y;

                rigidbody.velocity = vel;
            }
            else
            {
                CanShoot = true;
                rigidbody.velocity = Vector3.zero;
            }
        }

        private void Update()
        {
            if (!CanShoot)
                return;

            if (isGameOver)
                return;

            if (fireCooldown > 0)
            {
                fireCooldown -= Time.deltaTime;
            }
            else
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            fireCooldown = RateOfFire;

            /*
            GameObject go = GameObject.Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
            go.transform.forward = transform.forward;
            */

            Bullet bullet = ObjectPool.Instance.objectPool.Get();
            bullet.transform.position = BulletSpawnPoint.position;
            bullet.transform.rotation = BulletSpawnPoint.rotation;
            bullet.transform.forward = transform.forward;
        }
    }

}