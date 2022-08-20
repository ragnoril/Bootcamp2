using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterGame
{

    public class EnemyController : MonoBehaviour
    {
        [Header("Unity Components")]
        public Rigidbody rigidbody;

        public PlayerController Target;

        [Header("Speed Stats")]
        public float MoveSpeed;
        [Range(0f,2f)]
        public float AttackSpeed;
        private float attackTimer;

        [Header("Attack and Health Stats")]
        public float AttackPower;
        [Tooltip("Hit points for the Enemy character. How many health points does it have before dying from wound player attacks?")]
        public float Health;

        
        [HideInInspector]
        public bool isGameOver;

        // Start is called before the first frame update
        void Start()
        {
            isGameOver = false;

            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody>();
            }

            if (Target == null)
            {
                Target = GameObject.Find("Player").GetComponent<PlayerController>();
            }

            EventManager.Instance.OnGameOver += GameOver;
        }

        private void OnDestroy()
        {
            EventManager.Instance.OnGameOver -= GameOver;
        }

        public void GameOver()
        {
            isGameOver = true;
        }

        private void FixedUpdate()
        {
            if (isGameOver)
            {
                rigidbody.velocity = Vector3.zero;
                return;
            }

            float angleBetween = 270f - Mathf.Atan2(transform.position.z - Target.transform.position.z, transform.position.x - Target.transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, angleBetween, 0f));

            Vector3 vel = transform.forward * MoveSpeed * Time.fixedDeltaTime;
            vel.y = rigidbody.velocity.y;

            rigidbody.velocity = vel;
        }

        // Update is called once per frame
        void Update()
        {
            if (isGameOver)
            {
                return;
            }

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
            EventManager.Instance.PlayerHit(AttackPower);
            //Target.GetHurt(AttackPower);
        }

        public void GetHurt(float damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                EventManager.Instance.EnemyKilled();
                Destroy(gameObject);
            }
        }
    }
}