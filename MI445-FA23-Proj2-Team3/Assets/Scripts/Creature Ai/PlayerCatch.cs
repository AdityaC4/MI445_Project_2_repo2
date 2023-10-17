using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatch : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverScreen;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!other.GetComponent<PlayerController>().hidden)
            {
                //Pause game
                Cursor.lockState = CursorLockMode.None;

                other.GetComponent<PlayerController>().canMove = false;
                gameOverScreen.SetActive(true);
            }
        }
    }
}
