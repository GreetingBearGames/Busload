using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPassengerDie : MonoBehaviour
{
    private GameObject _target;
    private Transform _parentObj;
    private HumanMove[] _humanMoveScripts;
    public bool isBusFullyCrashed = false;

    void Start()
    {
        _target = GameObject.FindWithTag("Bus").gameObject;
        _parentObj = this.transform.parent.gameObject.transform;
        _humanMoveScripts = _parentObj.GetComponentsInChildren<HumanMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bus")
        {
            SoundManager.instance.Play("Drop Passenger");

            // foreach (HumanMove child in _humanMoveScripts)
            // {
            //     Destroy(child.gameObject);
            // }

            isBusFullyCrashed = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bus")
        {
            isBusFullyCrashed = false;
        }
    }

}
