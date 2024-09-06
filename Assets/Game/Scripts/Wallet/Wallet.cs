using System;

public class Wallet
{
    private int _coins;
    public virtual int Coins { 
        get
        {
            return _coins;
        } 
        protected set
        {
            _coins = value;
            OnCoinsChanget?.Invoke(_coins);
        }
    }
    public event Action<int> OnCoinsChanget;
    public virtual void Add(int coins)
    {
        Coins += coins;
    }
    public virtual bool TryRemove(int coins)
    {
        if(coins > Coins)
        {
            return false;
        }
        Coins -= coins;
        return true;
    }
}