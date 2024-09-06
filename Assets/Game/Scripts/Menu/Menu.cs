using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private LevelsPanel _levelsPanel;
    [SerializeField] private GameSettings _settings;
    [SerializeField] private WalletPresenter _coinsWallet;

    private void Start()
    {
        _coinsWallet.Init(ServiceLocator.Locator.CoinsWallet);
        _levelsPanel.Close();
        _settings.Close();
    }
    public void StartGame()
    {
        _levelsPanel.Open();
    }
}
