using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTriggerScript : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameController.Lose();
        }
    }
}
