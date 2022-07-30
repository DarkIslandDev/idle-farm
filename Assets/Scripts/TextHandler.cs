using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextHandler : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    public Slider wheatSlider;

    private void Start()
    {
        wheatSlider.maxValue = 40;
    }

    private void Update()
    {
        MoneyText.text = Barn.Money.ToString();
        wheatSlider.value = PlayerManager.instance.whBlocks.Count;
    }
}