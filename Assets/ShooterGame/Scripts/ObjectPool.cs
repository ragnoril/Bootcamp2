using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ShooterGame
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance { get; private set; }

        public IObjectPool<Bullet> objectPool;
        public Bullet bulletPrefab;

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

            objectPool = new ObjectPool<Bullet>(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet);
        }

        private void DestroyBullet(Bullet obj)
        {
            Destroy(obj.gameObject);
        }

        private void ReleaseBullet(Bullet obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void GetBullet(Bullet obj)
        {
            obj.gameObject.SetActive(true);
        }

        private Bullet CreateBullet()
        {
            Bullet bullet = Instantiate(bulletPrefab);

            return bullet;
        }

        /*
        public List<GameObject> pooledObjects;
        public GameObject objectToPool;
        public int poolLimit;

        private void Start()
        {
            pooledObjects = new List<GameObject>();
            for(int i = 0; i < poolLimit; i++)
            {
                GameObject go = Instantiate(objectToPool);
                go.SetActive(false);
                go.transform.SetParent(transform);
                pooledObjects.Add(go);
            }
        }

        public GameObject GetPooledObject()
        {
            for(int i = 0; i < poolLimit; i++)
            {
                if (!pooledObjects[i].activeSelf)
                {
                    Debug.Log("return: " + i);
                    return pooledObjects[i];
                }
            }

            return null;
        }
        */
    }

}