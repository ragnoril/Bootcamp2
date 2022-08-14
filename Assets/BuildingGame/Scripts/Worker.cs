using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BuildingGame
{
    public class Worker : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;

        public bool isBuilding;

        private void Start()
        {
            isBuilding = false;
        }

        public void MoveTo(Vector3 pos)
        {
            navMeshAgent.SetDestination(pos);
        }

        public void Build(Vector3 point)
        {
            isBuilding = true;
            MoveTo(point);

        }

        private void Update()
        {
            if (navMeshAgent.remainingDistance < 0.1f && isBuilding)
            {
                GameManager.Instance.CreateBuilding(transform.position + new Vector3(0f, 0f, 10f));
                isBuilding = false;
                
            }
        }
    }
}