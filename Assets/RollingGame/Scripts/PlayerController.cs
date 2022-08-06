using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RollingGame
{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody rigidbody;

        public float Speed;
        public float JumpForce;

        public int CoinCount;
        public bool isGameRunning;

        public float SpeedBonusCooldown;
        public float SpeedBonus;
        private float speedBonusTimer;

        public bool isGround;
        // Start is called before the first frame update
        void Start()
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody>();
            }

            CoinCount = 0;
            isGameRunning = true;
            isGround = false;
            speedBonusTimer = 0f;
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

            rigidbody.AddForce(new Vector3(moveX * (Speed + SpeedBonus) * Time.fixedDeltaTime, 0f, moveY * (Speed + SpeedBonus) * Time.fixedDeltaTime));

            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                rigidbody.AddForce(0f, JumpForce, 0f);
            }
            /*
            Vector3 newVelocity = new Vector3(moveX * Speed * Time.fixedDeltaTime, 0f, moveY * Speed * Time.fixedDeltaTime);
            newVelocity.y = rigidbody.velocity.y;
            rigidbody.velocity = newVelocity;
            /*

        }

        // Update is called once per frame
        void Update()
        {
            /*
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            transform.Translate(moveX * Speed * Time.deltaTime * 0.1f, 0f, moveY * Speed * Time.deltaTime * 0.1f);
            */
        }

        private void Update()
        {
            if (speedBonusTimer > 0f)
            {
                speedBonusTimer -= Time.deltaTime;

                Debug.Log("Powerup remaining time: " + speedBonusTimer);
            }
            else
            {
                SpeedBonus = 0f;

            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Block")
            {
                isGameRunning = false;
                Debug.Log("GAME OVER");

            }
            else if (collision.collider.tag == "Ground")
            {
                isGround = true;
            }


        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Coin")
            {
                CoinCount += 1;
                Debug.Log("Coin Collected. Coin Count: " + CoinCount);
                Destroy(other.gameObject);
            }
            else if (other.tag == "PowerUp")
            {
                SpeedBonus = (Speed / 2);
                speedBonusTimer = SpeedBonusCooldown;
                Debug.Log("Powerup gained");
            }

        }
        /*
        private void OnCollisionExit(Collision collision)
        {
            if (collision.collider.tag == "Block")
                Debug.Log("Separated from: " + collision.gameObject.name);
        }
        private void OnCollisionStay(Collision collision)
        {
            if (collision.collider.tag == "Block")
                Debug.Log("Still collided from: " + collision.gameObject.name);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Block")
                Debug.Log("Collided with: " + other.gameObject.name);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Block")
                Debug.Log("Separated from: " + other.gameObject.name);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Block")
                Debug.Log("Still Collided with: " + other.gameObject.name);

        }
        */

    }
}
