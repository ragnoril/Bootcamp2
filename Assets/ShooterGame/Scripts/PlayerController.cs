using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterGame
{
    public class PlayerController : MonoBehaviour
    {
        public AudioSource audioSource;
        public Animator animator;
        public Rigidbody rigidbody;
        public Transform BulletSpawnPoint;

        public float Speed;

        public bool isGameRunning;

        public GameObject BulletPrefab;

        public float RateOfFire;
        private float fireCooldown;

        private Camera gameCam;

        public float MaxHealth;
        public float CurHealth;

        public int Score;
        public int HighScore;

        public AudioClip fireClip;
        public AudioClip hurtClip;
        public AudioClip gameoverClip;

        // Start is called before the first frame update
        void Start()
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody>();
            }

            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }

            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }

            HighScore = PlayerPrefs.GetInt("HighScore", 0);
            Score = 0;
            CurHealth = MaxHealth;
            
            /*
            if (BulletSpawnPoint == null)
            {
                GameObject go = GameObject.Find("BulletSpawnPoint");
                BulletSpawnPoint = go.transform;
                
            }
            */

            gameCam = Camera.main;

            isGameRunning = true;

            EventManager.Instance.OnPlayerHit += GetHurt;
            EventManager.Instance.OnEnemyKilled += EnemyKilled;
        }

        private void OnDestroy()
        {
            EventManager.Instance.OnPlayerHit -= GetHurt;
            EventManager.Instance.OnEnemyKilled -= EnemyKilled;
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

            animator.SetFloat("Speed", rigidbody.velocity.magnitude);

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

            animator.SetTrigger("Shoot");

            AudioManager.Instance.PlaySfx((int)SFXList.PlayerShoot);

            /*
            GameObject go = GameObject.Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
            go.transform.forward = transform.forward;
            go.GetComponent<Bullet>().OwnerTag = transform.tag;
            */

            //GameObject go = BulletPool.GetPooledObject();
            Bullet bullet = ObjectPool.Instance.objectPool.Get();
            bullet.transform.position = BulletSpawnPoint.position;
            bullet.transform.rotation = BulletSpawnPoint.rotation;
            bullet.transform.forward = transform.forward;
            bullet.OwnerTag = transform.tag;
        }

        public void GetHurt(float damage)
        {
            CurHealth -= damage;

            animator.SetTrigger("GotHurt");
            
            if (CurHealth <= 0)
            {
                EventManager.Instance.GameOver();
                animator.SetBool("isDead", true);
                AudioManager.Instance.PlaySfx((int)SFXList.GameOver);
            }
        }

        public void EnemyKilled()
        {
            AnalyticsManager.Instance.SendAnalyticsEvent("EnemyKilled");
            AudioManager.Instance.PlaySfx((int)SFXList.EnemyKilled);
            Score += 1;
            if (Score > HighScore)
                PlayerPrefs.SetInt("HighScore", Score);
        }


    }
}
