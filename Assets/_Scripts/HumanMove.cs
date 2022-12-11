using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMove : MonoBehaviour
{
    private GameObject _target;
    [SerializeField] private float _moveSpeed;
    private Vector3 _targetPos;
    [HideInInspector] public bool isMoving = false;

    void Start()
    {
        _target = GameObject.FindWithTag("Bus").gameObject;

    }

    void Update()
    {
        if (isMoving)
        {
            MoveHumantoBus();
        }
    }

    public void MoveHumantoBus()
    {
        _targetPos = new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, _moveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bus")
        {
            Destroy(this.gameObject);
            //OTOBÜSÜN ÜZERİNDEKİ SAYIYI ARTTIR
        }
    }
}
