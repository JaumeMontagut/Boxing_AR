﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    int win_player = 0;
    int rounds = 0;

    public GameObject rounds_enemy;
    public GameObject rounds_player;
    public GameObject robots;

    //TODO: Make this a singleton

    public void StartGame()
    {
        foreach(Transform t in robots.transform)
        {
            t.gameObject.GetComponent<Entity>().start = true;
        }
    }

    public void GameWin()
    {
        rounds++;
        win_player++;
        UpdateCounter();
        if (win_player >= 3) //Win
        {
            //TODO: Show Game Win UI, etc.
        }
        else
        {
            robots.transform.Find("Enemy").GetComponent<EnemyManager>().difficult++;
            //Recover life
        }

        foreach (Transform t in robots.transform)
        {
            t.gameObject.GetComponent<Entity>().ResetLife();
        }
    }

    public void GameOver()
    {
        rounds++;
        UpdateCounter();
        if (rounds - win_player >= 3) //Lose
        {
            //TODO: Show Game Over UI, etc.
        }
        else
        {
            //Recover life
        }

        foreach (Transform t in robots.transform)
        {
            t.gameObject.GetComponent<Entity>().ResetLife();
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
