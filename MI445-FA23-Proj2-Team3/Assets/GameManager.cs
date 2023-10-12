using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController player;
    [SerializeField]
    public Dictionary<string, bool> flags = new Dictionary<string, bool>();
    [SerializeField]
    Vector3 spawnPoint;

    [SerializeField]
    GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1;
        }

    }

    public void ResetPlayer()
    {
        StartCoroutine(RunResetPlayer());
    }

    IEnumerator RunResetPlayer()
    {
        player.transform.position = spawnPoint;
        yield return new WaitForFixedUpdate();
        player.canMove = true;
        gameOverScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
