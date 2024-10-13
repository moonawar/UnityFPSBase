using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class SceneLoadEvents {
    public Action OnSceneLoadStart;
    public Action OnSceneLoadComplete;
}

public class SceneLoader : SingletonMB<SceneLoader>
{
    public string CurrentScene => SceneManager.GetActiveScene().name;
    public bool IsLoading { get; private set; } = false;

    // Loader Callback Handler
    private Action onLoaderStart;
    private AsyncOperation asyncOp;
    private bool loaderDone = true;

    public void LoadScene(string sceneName, SceneLoadEvents sceneLoadEvents = null, bool loader = true) {
        switch (loader)
        {
            case true:
                LoadSceneWithLoader(sceneName, sceneLoadEvents);
                break;
            case false:
                LoadSceneNoLoader(sceneName, sceneLoadEvents);
                break;
        }
    }

    private void LoadSceneWithLoader(string sceneName, SceneLoadEvents sceneLoadEvents = null)
    {   
        if (IsLoading) { return; }
        onLoaderStart = () => StartCoroutine(LoadSceneAsync(sceneName, sceneLoadEvents));

        loaderDone = false;
        SceneManager.LoadScene("Loader");
    }

    private void LoadSceneNoLoader(string sceneName, SceneLoadEvents sceneLoadEvents = null)
    {
        if (IsLoading) { return; }
        StartCoroutine(LoadSceneAsync(sceneName, sceneLoadEvents));
    }

    private IEnumerator LoadSceneAsync(string sceneName, SceneLoadEvents sceneLoadEvents = null)
    {
        IsLoading = true;

        asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;

        while (!asyncOp.isDone)
        {
            if (asyncOp.progress >= 0.9f)
            {
                yield return null;
                
                sceneLoadEvents?.OnSceneLoadStart?.Invoke();
                yield return new WaitUntil(() => loaderDone);
                asyncOp.allowSceneActivation = true;
            }

            yield return null;
        }

        sceneLoadEvents?.OnSceneLoadComplete?.Invoke();
        IsLoading = false;
    }

    public void LoaderDone() 
    {
        loaderDone = true;
    }

    public void LoaderStart()
    {
        if (Instance.onLoaderStart != null)
        {
            Instance.onLoaderStart();
            Instance.onLoaderStart = null;
        }
    }
}