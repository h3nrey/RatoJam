using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] public Image fuelBarImage;
    [SerializeField] public Sprite[] fuelImages;
    [SerializeField] private TMP_Text cheeseText;
    public static UIController Ui;

    private void Awake() {
        Ui = this;
    }

    public void UpdateFuelBar(Sprite image) {
        fuelBarImage.sprite = image;
    }

    public void UpdateCheeseText(int cheeseAmount) {
        cheeseText.text = $"X {cheeseAmount}";
    }

    public void ToggleElement(GameObject uiElement) {
        uiElement.SetActive(!uiElement.activeSelf);
    }
}
