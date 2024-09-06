
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerUpdater : MonoBehaviour
{
    private event Action OnUpdate;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += ClearListeners;
    }
    public void AddListener(Action update)
    {
        OnUpdate += update;
    }
    public void RemoveListener(Action update)
    {
        OnUpdate -= update;
    }
    private void Update()
    {
        OnUpdate?.Invoke();
    }
    private void ClearListeners(Scene scene, LoadSceneMode loadSceneMode)
    {
        OnUpdate = null;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ClearListeners;
    }
}
