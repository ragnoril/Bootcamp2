using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterGame
{

    public class EnemyController : MonoBehaviour
    {
        public Rigidbody rigidbody;

        public PlayerController Target;

        public float MoveSpeed;
        public float AttackSpeed;
        private float attackTimer;

        public float AttackPower;
        public float Health;

        // Start is called before the first frame update
        void Start()
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody>();
            }

            if (Target == null)
            {
                Target = GameObject.Find("Player").GetComponent<PlayerController>();
            }
        }

        private void FixedUpdate()
        {
            float angleBetween = 270f - Mathf.Atan2(transform.position.z - Target.transform.position.z, transform.position.x - Target.transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, angleBetween, 0f));

            Vector3 vel = transform.forward * MoveSpeed * Time.fixedDeltaTime;
            vel.y = rigidbody.velocity.y;

            rigidbody.velocity = vel;
        }

        // Update is called once per frame
        void Update()
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Player")
            {
                if (attackTimer <= 0)
                    Attack();
            }
        }

        private void Attack()
        {
            attackTimer = AttackSpeed;
            Target.GetHurt(AttackPower);
        }

        public void GetHurt(float damage)
        {
            Health -= damage;

            if (Health <= 0)
                Destroy(gameObject);
        }
    }
}