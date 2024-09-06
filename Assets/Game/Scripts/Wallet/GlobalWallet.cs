using UnityEngine;

public static class GlobalWallet
{
    public static bool TryRemoveCoins(int coins, string walletName)
    {
        int balance = Saver.GetInt(walletName, 0);
        if(coins > balance)
        {
            return false;
        }
        else
        {
            balance -= coins;
            Saver.SaveInt(balance, walletName);
            return true;
        }

    }
    public static void AddCoins(int coins, string walletName)
    {
        int balance = Saver.GetInt(walletName, 0);
        balance += coins;
        Saver.SaveInt(balance, walletName);
    }
    public static int GetCoins( string walletName, int defaultValue = 0)
    {
        return Saver.GetInt(walletName, defaultValue);
    }
}
