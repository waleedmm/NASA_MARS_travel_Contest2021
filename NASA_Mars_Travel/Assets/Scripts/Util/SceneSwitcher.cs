using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

    public class SceneSwitcher : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ChangeSceneToIndex(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void ReloadCurrentScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
