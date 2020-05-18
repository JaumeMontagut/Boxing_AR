using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    int win_player = 0;
    int rounds = 0;

    public GameObject rounds_enemy;
    public GameObject rounds_player;

    //TODO: Make this a singleton

    public void GameWin()
    {
        rounds++;
        win_player++;
        UpdateCounter();
        if (win_player >= 5) //Win
        {
            //TODO: Show Game Win UI, etc.
        }
        else
        {
            //Recover life
        }
    }

    public void GameOver()
    {
        rounds++;
        UpdateCounter();
        if (rounds - win_player >= 5) //Lose
        {
            //TODO: Show Game Over UI, etc.
        }
        else
        {
            //Recover life
        }
    }

    private void UpdateCounter()
    {
        foreach(Transform c in rounds_player.transform)
        {
            c.gameObject.SetActive(false);
        }
        rounds_player.transform.Find(win_player.ToString()).gameObject.SetActive(true);

        foreach (Transform c in rounds_enemy.transform)
        {
            c.gameObject.SetActive(false);
        }
        rounds_enemy.transform.Find((rounds - win_player).ToString()).gameObject.SetActive(true);
    }
}
