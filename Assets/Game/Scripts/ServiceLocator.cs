using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public Wallet CoinsWallet { get; private set; }
    public static ServiceLocator Locator { get; private set; }

    private void Awake()
    {
        CoinsWallet = new SavingWallet("Coins");
        if (Locator == null) Locator = this;
    }
}

public class SavingWallet : Wallet
{
    private readonly string _savingKey;
    public SavingWallet(string savingKey)
    {
        Coins = GlobalWallet.GetCoins(savingKey, 0);
        _savingKey = savingKey;
    }
    public override void Add(int coins)
    {
        base.Add(coins);
        GlobalWallet.AddCoins(coins, _savingKey);
    }
    public override bool TryRemove(int coins)
    {
        bool isRemoved = GlobalWallet.TryRemoveCoins(coins, _savingKey);
        if (isRemoved)
        {
            base.TryRemove(coins);
        }
        return isRemoved;
    }
}
