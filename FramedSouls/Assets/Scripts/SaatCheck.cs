using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaatCheck : MonoBehaviour
{
    public bool isPlayerCheckedClock = false;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.Instance.currentState == GameState.ClockChimed)
        {
            isPlayerCheckedClock = true;
        }
    }


}
