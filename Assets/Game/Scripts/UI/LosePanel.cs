
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreOutput;
    [SerializeField] private TextMeshProUGUI _heightOutput;
    [SerializeField] private TextMeshProUGUI _maxHeightOutput;
    [SerializeField] private TextMeshProUGUI _levelOutput;
    private Wallet _scoreWallet;

    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void Open(int maxHeight, int height, int level, int coins)
    {
        gameObject.SetActive(true);
        _maxHeightOutput.text = maxHeight.ToString();
        _heightOutput.text = height.ToString();
        _levelOutput.text = "Level " + level.ToString();
        _scoreOutput.text = coins.ToString();
    }
    public void BackToHome()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
