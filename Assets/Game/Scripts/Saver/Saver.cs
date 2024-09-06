
using System;
using UnityEngine;

public static class Saver 
{
    public static void SaveInt(int value, string key)
    {
        PlayerPrefs.SetInt(key, value);
    }
    public static int GetInt(string key, int defaultValue = 0)
    {
        int value = PlayerPrefs.GetInt(key, defaultValue);
        return value;
    }
    public static void SaveString(string value, string key)
    {
        PlayerPrefs.SetString(key, value);
    }
    public static string GetString(string key, string defaultValue = "")
    {
        string value = PlayerPrefs.GetString(key, defaultValue);
        return value;
    }
    public static void SaveBool(bool value, string key)
    {
        int intValue = value ? 1 : 0;
        PlayerPrefs.SetInt(key, intValue);
    }
    public static bool GetBool(string key, bool defaultValue = false)
    {
        int intValue = defaultValue ? 1 : 0;
        int value = PlayerPrefs.GetInt(key, intValue);
        return value > 0;
    }
}
