
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Toggle _soundsToggle;
    [SerializeField] private Toggle _vibroToggle;
    [SerializeField] private GameObject _privacyPolicy;
    private void Awake()
    {
        _soundsToggle.isOn = Saver.GetBool("Sounds", true);
        _vibroToggle.isOn = Saver.GetBool("Vibro", true);
        ClosePriacyPolicy();
    }
    public void OpenPriacyPolicy()
    {
        _privacyPolicy.SetActive(true);
    }
    public void ClosePriacyPolicy()
    {
        _privacyPolicy.SetActive(false);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void SetSounds(bool active)
    {
        Saver.SaveBool(active, "Sounds");
    }
    public void SetVibro(bool active)
    {
        Saver.SaveBool(active, "Vibro");
    }
}
