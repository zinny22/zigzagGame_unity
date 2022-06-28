using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    private GameController gameController;

    private void EnterItem( Collider other)
    {
        if (other.tag.Equals("Player")){
            gameController.GameOver();
        }
    }
}
