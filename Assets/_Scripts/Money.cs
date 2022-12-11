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

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bus")
        {
            Explode();
            _moneyCount.text = (int.Parse(_moneyCount.text) + incomeRatio).ToString();
            //incomeRatio ne kadar fazlaysa her para aldığında daha fazla para kazanmış olur.
            GameManager.Instance.UpdateMoney((int)(int.Parse(_moneyCount.text) + incomeRatio));
            Destroy(this.gameObject);
        }
    }


    private void Explode()
    {
        GameObject particle = Instantiate(_moneyTakeParticle, transform.position, new Quaternion(0f, 0f, 0f, 0f));
        particle.GetComponent<ParticleSystem>().Play();
    }
}
