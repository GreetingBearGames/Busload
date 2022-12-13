using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopMenuButtons : MonoBehaviour {
    public Button passengerButton, incomeButton;
    public TextMeshProUGUI IncomeLvText, PassengerLvText, IncomeBuyValueText, PassengerBuyValueText;
    public int incomeBuyValue, passengerBuyValue, incomeLv, passengerLv;
    [SerializeField] private AudioSource buttonSound;
    private void Start() {
        incomeBuyValue = 20;
        passengerBuyValue = 20;
    }
    private void Update() {
        if (!GameManager.Instance.IsGameStarted) {
            passengerButton.gameObject.SetActive(true);
            incomeButton.gameObject.SetActive(true);
        } else {
            passengerButton.gameObject.SetActive(false);
            incomeButton.gameObject.SetActive(false);
        }
        if (GameManager.Instance.Money < incomeBuyValue) {
            incomeButton.interactable = false;
        }
        if (GameManager.Instance.Money < passengerBuyValue) {
            passengerButton.interactable = false;
        }
        if (GameManager.Instance.Money > incomeBuyValue) {
            incomeButton.interactable = true;
        }
        if (GameManager.Instance.Money > passengerBuyValue) {
            passengerButton.interactable = true;
        }
    }

    public void IncomeUpgrade() {
        GameManager.Instance.UpdateMoney(-incomeBuyValue);
        incomeLv++;
        incomeBuyValue *= incomeLv;
        GameManager.Instance.MoneyIncreaseRate *= 1.25f;
        buttonSound.Play();
    }

    public void PassengerUpgrade() {
        GameManager.Instance.UpdateMoney(-passengerBuyValue);
        passengerLv++;
        passengerBuyValue *= passengerLv;
        GameManager.Instance.PassengerIncreaseRate *= 1.25f;
        buttonSound.Play();
    }
}
