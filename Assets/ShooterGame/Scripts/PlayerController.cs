using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShooterGame
{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody rigidbody;
        public Transform BulletSpawnPoint;

        public float Speed;

        public bool isGameRunning;

        public GameObject BulletPrefab;

        public float RateOfFire;
        private float fireCooldown;

        private Camera gameCam;

        public float Health;

        // Start is called before the first frame update
        void Start()
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody>();
            }

            /*
            if (BulletSpawnPoint == null)
            {
                GameObject go = GameObject.Find("BulletSpawnPoint");
                BulletSpawnPoint = go.transform;
                
            }
            */

            gameCam = Camera.main;

            isGameRunning = true;

        }

        private void FixedUpdate()
        {
            if (!isGameRunning)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                return;
            }
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector3 vel = new Vector3(moveX * Speed * Time.fixedDeltaTime, 0f, moveY * Speed * Time.fixedDeltaTime);
            vel.y = rigidbody.velocity.y;
            rigidbody.velocity = vel;

        }

        private void Update()
        {
            if (!isGameRunning)
                return;

            if (fireCooldown > 0f)
            {
                fireCooldown -= Time.deltaTime;
            }

            if (Input.GetMouseButton(0))
            {
                if (fireCooldown <= 0f)
                    Shoot();
            }

            Vector3 mouseWorldPosition = gameCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 15f);
            float angleBetween = 270f - Mathf.Atan2(transform.position.z - mouseWorldPosition.z, transform.position.x - mouseWorldPosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, angleBetween, 0f));
        }

        public void Shoot()
        {
            fireCooldown = RateOfFire;

            GameObject go = GameObject.Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
            go.transform.forward = transform.forward;
            go.GetComponent<Bullet>().OwnerTag = transform.tag;

        }

        public void GetHurt(float damage)
        {
            Health -= damage;
            if (Health <= 0)
                isGameRunning = false;
        }


    }
}
