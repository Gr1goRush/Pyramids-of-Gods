using TMPro;
using UnityEngine;

public class HeightPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _maxHeightOutput;
    [SerializeField] private TextMeshProUGUI _heightOutput;

    public void Init(int maxHeight)
    {
        _maxHeightOutput.text = maxHeight.ToString();
        _heightOutput.text = "1";
    }
    public void UpdateHeight(int height)
    {
        _heightOutput.text = height.ToString();
    }
}
