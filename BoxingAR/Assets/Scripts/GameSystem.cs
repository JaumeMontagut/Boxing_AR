using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    int win_player = 0;
    int rounds = 0;
    //TODO: Make this a singleton

    public void GameWin()
    {
        rounds++;
        win_player++;
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
        if (rounds - win_player >= 5) //Lose
        {
            //TODO: Show Game Over UI, etc.
        }
        else
        {
            //Recover life
        }
    }
}
