using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class Loader
{
    private static System.Action OnLoaderCallBack;
    static AsyncOperation operation;
    private class LoadingMonoBehaviour : MonoBehaviour { };
    public static void Load(int ind, Animator animator, float transitionTime, GameObject gameLoseUI = null)
    {
        GameObject dummy = new GameObject("Dummy");
        //OnLoaderCallBack = () =>
        //{
            
        //    dummy.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(ind));
            
        //};

        dummy.AddComponent<LoadingMonoBehaviour>().StartCoroutine(Animation(transitionTime, animator, ind, gameLoseUI)); // LoadingAnimation

       
      // SceneManager.LoadScene("Loading"); // justLoadingScreen
    }
   
    private static IEnumerator Animation(float transitionTime, Animator animator, int ind, GameObject gameLoseUI = null)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        if (gameLoseUI != null)
        {
            if (gameLoseUI.activeSelf == true)
                SceneManager.LoadScene(ind - 1);
            else if (ind == 8)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(ind);
            }
        }
        else
        {
            SceneManager.LoadScene(ind);
        }
    }
    private static IEnumerator LoadSceneAsync(int ind) // for justLoading Screen
    {
        yield return null;
        operation = SceneManager.LoadSceneAsync(ind);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
    public static float GetLoadingProgress()
    {
        if (operation != null) return operation.progress;
        return 1f;
    }
    public static void LoaderCallBack()
    {
        if (OnLoaderCallBack != null)
        {
            OnLoaderCallBack();
            OnLoaderCallBack = null;
        }
    }
}

