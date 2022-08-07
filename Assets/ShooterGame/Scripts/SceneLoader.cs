using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShooterGame
{
    public class SceneLoader : MonoBehaviour
    {
        public int loadingSceneIndex;
        public int sceneIndexToLoad;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

        }

        private void Update()
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene.buildIndex == loadingSceneIndex)
            {
                SceneManager.LoadScene(sceneIndexToLoad);
            }
            else if (scene.buildIndex == sceneIndexToLoad)
            {
                Destroy(gameObject);
            }
        }

    }

}