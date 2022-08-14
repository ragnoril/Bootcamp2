using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace BuildingGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

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

        public NavMeshSurface navMeshSurface;
        public Worker worker;

        public Building[] Buildings;

        public TMP_Text BuildingText;
        public TMP_Text MoneyText;

        public int BuildingIndex;

        public int CreditAmount;

        // Start is called before the first frame update
        void Start()
        {
            BuildingIndex = -1;
            if (navMeshSurface == null)
                navMeshSurface = GetComponent<NavMeshSurface>();

            MoneyText.text = "Money: " + CreditAmount;
        }

        // Update is called once per frame
        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo = new RaycastHit();

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
                {
                    if (BuildingIndex == -1)
                    {
                        worker.MoveTo(hitInfo.point);
                    }
                    else if (BuildingIndex > -1)
                    {
                        worker.Build(hitInfo.point);
                    }
                }
            }
        }

        public void SelectBuilding(int index)
        {
            if (index < 0)
            {
                BuildingIndex = -1;
                UpdateBuildingText();
            }
            else if (CreditAmount >= Buildings[index].Cost)
            {
                BuildingIndex = index;
                UpdateBuildingText();
            }
            else
            {
                BuildingIndex = -1;
                UpdateBuildingText();
            }
        }

        private void UpdateBuildingText()
        {
            BuildingText.text = "Building: ";
            if (BuildingIndex > -1)
            {
                BuildingText.text += Buildings[BuildingIndex].BuildingName;
            }
        }

        public void CreateBuilding(Vector3 pos)
        {
            GameObject go = Instantiate(Buildings[BuildingIndex].Prefab, pos, Quaternion.identity);
            navMeshSurface.BuildNavMesh();
            CreditAmount -= Buildings[BuildingIndex].Cost;
            UpdateMoneyText();
            SelectBuilding(-1);
        }

        private void UpdateMoneyText()
        {
            MoneyText.text = "Money: " + CreditAmount;
        }
    }

}
