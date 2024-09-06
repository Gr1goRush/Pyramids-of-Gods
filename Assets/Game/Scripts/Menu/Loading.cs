using System;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private float _addPointTime;
    [SerializeField] private float _minLoadingTime = 2;
    [SerializeField] private Transform _lable;
    [SerializeField] private Transform _BG;
    [SerializeField] private Vector3 _bgDirection;
    [SerializeField] private float _distance;
    [SerializeField] private float _scaling;
    [SerializeField] private TextMeshProUGUI _loadingText;

    private Vector3 _position;
    private float _currentPointTime;
    private int _pointCount;
    private float _loadTime;

    private void Awake()
    {
        _position = _BG.position;
    }

    private void Update()
    {
        _currentPointTime += Time.deltaTime;
        _loadTime += Time.deltaTime;
        if (_currentPointTime >= _addPointTime)
        {
            _currentPointTime = 0;
            _pointCount += 1;
            if (_pointCount >= 4)
            {
                _pointCount = 0;
            }
            string loading = "Wait Loading";
            for (int i = 0; i < _pointCount; i++)
            {
                loading += ".";
            }
            _loadingText.text = loading;
        }
        float sin = Mathf.Sin(Time.time);
        float aSin = Mathf.Abs(sin);
        Vector3 scale = Vector3.one * Mathf.Lerp(1,_scaling, aSin);
        _lable.localScale = scale;
        Vector3 nextPos = new Vector3((float)Screen.width * _bgDirection.x, (float)Screen.height * _bgDirection.y);
        Vector3 position = Vector3.Lerp(_position, _position + nextPos, aSin);
        _BG.position = position;
    }


    public void LoadGame()
    {
        gameObject.SetActive(true);
        StartCoroutine(Load());
    }
    private IEnumerator Load()
    {
        yield return null;
        AsyncOperation loading = SceneManager.LoadSceneAsync("Game");
        loading.allowSceneActivation = false;
        while (_loadTime <= _minLoadingTime)
        {
            yield return null;
        }
        loading.allowSceneActivation = true;

    }
    public void OnLoad(AsyncOperation operation)
    {
        operation.allowSceneActivation = true;
    }
}
