using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lableOutput;
    [SerializeField] private TextMeshProUGUI _scoreOutput;
    [SerializeField] private TextMeshProUGUI _heightOutput;

    private int _level;
    private int _nextLevel;
    private int _height;

    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void Open(int level, int nextLevel, int height, int coins)
    {
        _level = level;
        _height = height;
        _nextLevel = nextLevel;
        gameObject.SetActive(true);
        _lableOutput.text = "Level " + level.ToString();
        _heightOutput.text = height.ToString();
        _scoreOutput.text = coins.ToString();
    }
    public void BackToHome()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Next()
    {
        Saver.SaveInt(_nextLevel, "Level");
        SceneManager.LoadScene("Game");
    }
}