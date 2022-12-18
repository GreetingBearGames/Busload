using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopMenuButtons : MonoBehaviour
{
    public Button passengerButton, incomeButton;
    public TextMeshProUGUI IncomeLvText, PassengerLvText, IncomeBuyValueText, PassengerBuyValueText;
    public float incomeBuyValue, passengerBuyValue, incomeLv, passengerLv;

    private void Awake()
    {
        passengerLv = PlayerPrefs.GetFloat("PassengerLevel", 1f);
        incomeLv = PlayerPrefs.GetFloat("MoneyLevel", 1f);
        IncomeLvText.text = ((int)incomeLv).ToString();
        PassengerLvText.text = ((int)passengerLv).ToString();
    }

    private void Start()
    {
        incomeBuyValue = 20 * GameManager.Instance.PassengerIncreaseRate;
        passengerBuyValue = 20 * GameManager.Instance.PassengerIncreaseRate;

        IncomeBuyValueText.text = ((int)(incomeBuyValue)).ToString();
        IncomeBuyValueText.text = ((int)(passengerBuyValue)).ToString();
    }
    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            passengerButton.gameObject.SetActive(true);
            incomeButton.gameObject.SetActive(true);
        }
        else
        {
            passengerButton.gameObject.SetActive(false);
            incomeButton.gameObject.SetActive(false);
        }
        if (GameManager.Instance.Money < incomeBuyValue)
        {
            incomeButton.interactable = false;
        }
        if (GameManager.Instance.Money < passengerBuyValue)
        {
            passengerButton.interactable = false;
        }
        if (GameManager.Instance.Money > incomeBuyValue)
        {
            incomeButton.interactable = true;
        }
        if (GameManager.Instance.Money > passengerBuyValue)
        {
            passengerButton.interactable = true;
        }
    }

    public void IncomeUpgrade()
    {
        GameManager.Instance.UpdateMoney(-incomeBuyValue);
        incomeLv++;
        PlayerPrefs.SetFloat("MoneyLevel", incomeLv);
        incomeBuyValue *= incomeLv;
        GameManager.Instance.MoneyIncreaseRate *= 1.25f;
        IncomeBuyValueText.text = ((int)(incomeBuyValue)).ToString();
        SoundManager.instance.Play("Button Sound");
        IncomeLvText.text = ((int)incomeLv).ToString();
    }

    public void PassengerUpgrade()
    {
        GameManager.Instance.UpdateMoney(-passengerBuyValue);
        passengerLv++;
        PlayerPrefs.SetFloat("PassengerLevel", passengerLv);
        passengerBuyValue *= passengerLv;
        GameManager.Instance.PassengerIncreaseRate *= 1.25f;
        PassengerBuyValueText.text = ((int)(passengerBuyValue)).ToString();
        SoundManager.instance.Play("Button Sound");
        PassengerLvText.text = ((int)passengerLv).ToString();
    }
}
