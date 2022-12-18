using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusGetOn : MonoBehaviour
{
    private GameObject _target;
    private Transform _parentObj;
    private HumanMove[] _humanMoveScripts;

    void Start()
    {
        _target = GameObject.FindWithTag("Bus").gameObject;
        _parentObj = this.transform.parent.gameObject.transform;
        _humanMoveScripts = _parentObj.GetComponentsInChildren<HumanMove>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bus")
        {
            SoundManager.instance.Play("Take Passenger");

            foreach (HumanMove child in _humanMoveScripts)
            {
                child.isMoving = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bus")
        {
            foreach (HumanMove child in _humanMoveScripts)
            {
                child.isMoving = false;
            }
        }
    }


}
