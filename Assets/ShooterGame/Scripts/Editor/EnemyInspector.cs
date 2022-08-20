using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShooterGame
{

    [CustomEditor(typeof(EnemyController))]
    public class EnemyInspector : Editor
    {

        string enemyMessage;

        private void OnSceneGUI()
        {
            EnemyController enemy = (EnemyController)target;
            Handles.PositionHandle(enemy.transform.position, enemy.transform.rotation);
            if (Handles.Button(enemy.transform.position, enemy.transform.rotation, 1f, 1f, Handles.SphereHandleCap))
            {
                Debug.Log("pressed my handles, have you?");
            }
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            EnemyController enemy = (EnemyController)target;

            GUILayout.Label("My Inspector Stuff");

            GUILayout.BeginHorizontal();
            GUILayout.Label("Enemy Message: ", GUILayout.Width(150f));
            
            enemyMessage = GUILayout.TextField(enemyMessage);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Fill Missing References"))
            {
                enemy.rigidbody = enemy.gameObject.GetComponent<Rigidbody>();
                
            }

            if (GUILayout.Button("Set Stats For Easy Enemy"))
            {
                Debug.Log(enemyMessage);
                enemy.AttackPower = 3;
                enemy.AttackSpeed = 1;
                enemy.Health = 5;
            }

            if (GUILayout.Button("Set Stats For Hard Enemy"))
            {
                
                enemy.AttackPower = 5;
                enemy.AttackSpeed = 4;
                enemy.Health = 20;
            }


        }
    }
}