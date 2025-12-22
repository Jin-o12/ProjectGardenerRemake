using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    // 외부에서 구독할 수 있는 씬 로드 이벤트
    public event Action<Scene, LoadSceneMode> SceneLoaded;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Unity의 씬 로드 이벤트를 받아 포워딩
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
        if (Instance == this) Instance = null;
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneLoaded?.Invoke(scene, mode);
    }

    // 편의 메서드들
    public void Load(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(sceneName, mode);
    }

    public AsyncOperation LoadAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        return SceneManager.LoadSceneAsync(sceneName, mode);
    }

    public void ReloadCurrent()
    {
        Load(SceneManager.GetActiveScene().name);
    }

    public void LoadByIndex(int index, LoadSceneMode mode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(index, mode);
    }
}