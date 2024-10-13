using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject loseMenu;
    public GameObject winMenu;
    public GameObject player;
    public TextMeshProUGUI playerHP;
    public TextMeshProUGUI finishTime;
    public Vector3 startPosition;

    private float startTime;

    public void StartTime()
    {
        startTime = Time.time;
    }

    public void Finish()
    {
        finishTime.text = (Time.time - startTime).ToString("#.##");
        winMenu.SetActive(true);
    }

    public void Lose()
    {
        loseMenu.SetActive(true);
    }

    public void Restart(bool isWin)
    {
        (isWin ? winMenu : loseMenu).SetActive(false);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.hp = playerController.maxHealth;
        playerController.isGrounded = true;
        playerController.isMoving = false;
        playerHP.text = playerController.maxHealth.ToString();
        player.transform.position = startPosition;
    }
}
