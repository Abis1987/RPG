using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCheck : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == ("Water"))
        {
            gameObject.GetComponentInParent<ThirdPersonController>()._isInWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Water"))
        {
            gameObject.GetComponentInParent<ThirdPersonController>()._isInWater = false;
        }
    }
}
