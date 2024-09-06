using TMPro;
using UnityEngine;
public class WalletPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsOutput;

    private Wallet _wallet;
    public void Init(Wallet wallet)
    {
        _wallet = wallet; 
        _coinsOutput.text = _wallet.Coins.ToString();
        _wallet.OnCoinsChanget += (coins) => _coinsOutput.text = coins.ToString();
    }
}