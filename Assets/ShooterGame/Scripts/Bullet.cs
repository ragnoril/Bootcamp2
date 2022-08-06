using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterGame
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody rigidbody;
        public float Speed;
        public float Damage;
        public string OwnerTag;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rigidbody.velocity = transform.forward * Speed * Time.fixedDeltaTime;
            rigidbody.angularVelocity = Vector3.zero;

            // Vector3.forward (0,0,1)
            // Vector3.up (0,1,0)
            //Vector3.right (1,0,0)
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == OwnerTag)
                return;

            if (collision.collider.tag == "Enemy")
            {
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                enemy.GetHurt(Damage);
            }
            else if (collision.collider.tag == "Player")
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                player.GetHurt(Damage);
            }

            Destroy(gameObject);
        }
    }
}
