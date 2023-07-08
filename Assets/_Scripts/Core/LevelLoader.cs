using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Core
{
    public class LevelLoader : MonoBehaviour
    {
        public Animator transition;

        [Button("Load menu Scene")]
        public void LoadMenuScene()
        {
            StartCoroutine(LoadLevel("Scenes/Menu"));
        }

        [Button("Load Main Scene")]
        public void LoadMainScene()
        {
            StartCoroutine(LoadLevel("Scenes/Main"));
        }

        public void ExistGame()
        {
            Application.Quit();
        }

        IEnumerator LoadLevel(string path)
        {
            transition.SetTrigger("Start");

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(path);

            // Animation time
            yield return new WaitForSeconds(transition.runtimeAnimatorController.animationClips[0].length);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

        }
    }
}
