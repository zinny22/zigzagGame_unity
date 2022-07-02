using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GameController.Instance.GameOver();
 
        }
    }
}
