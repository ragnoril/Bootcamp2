using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterGame
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public event Action<float> OnPlayerHit;
        public event Action<EnemyController, float> OnEnemyHit;
        public event Action OnGameOver;
        public event Action OnEnemyKilled;

        public void PlayerHit(float damage)
        {
            OnPlayerHit?.Invoke(damage);
        }

        public void EnemyHit(EnemyController enemy, float damage)
        {
            OnEnemyHit?.Invoke(enemy, damage);
        }

        public void EnemyKilled()
        {
            OnEnemyKilled?.Invoke();
        }

        public void GameOver()
        {
            OnGameOver?.Invoke();
        }
    }
}
