using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    [SerializeField] private GameObject _moneyTakeParticle;
    private TextMeshProUGUI _moneyCount;
    public float incomeRatio = 1;


    void Start()
    {
        _moneyCount = GameObject.FindWithTag("MoneyCount").gameObject.GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        _moneyCount.text = ((int)GameManager.Instance.Money).ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bus")
        {
            Explode();
            SoundManager.instance.Play("MoneyCollect");
            GameManager.Instance.MoneyCountPerLevel += GameManager.Instance.MoneyIncreaseRate;
            GameManager.Instance.UpdateMoney(GameManager.Instance.MoneyIncreaseRate);
            _moneyCount.text = ((int)GameManager.Instance.Money).ToString();
            Destroy(this.gameObject);
        }
    }


    private void Explode()
    {
        GameObject particle = Instantiate(_moneyTakeParticle, transform.position, new Quaternion(0f, 0f, 0f, 0f));
        particle.GetComponent<ParticleSystem>().Play();
    }
}
