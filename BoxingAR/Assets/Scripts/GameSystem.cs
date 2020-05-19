using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public int win_player = 0;
    public int rounds = 0;

    public GameObject rounds_enemy;
    public GameObject rounds_player;

    public GameObject Player;
    public GameObject Enemy;
    
    Animator player_anim;
    Animator enemy_anim;

    public GameObject robots;
    public GameObject winText;
    public GameObject loseText;

    GameObject playerwinLight;
    GameObject enemywinLight;

   public GameObject buttonsEndGame;
   public GameObject Confety;

    private void Start()
    {
        player_anim = Player.GetComponent<Animator>();
        enemy_anim = Enemy.GetComponent<Animator>();
        playerwinLight = Player.transform.Find("WinLight").gameObject;
        enemywinLight = Enemy.transform.Find("WinLight").gameObject;
        buttonsEndGame.SetActive(false);
        Confety.SetActive(false);
    }
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
            Debug.Log("Game win entered");
   
            player_anim.SetBool("Victory",true);
            enemy_anim.SetBool("Defeat", true);
            winText.SetActive(true);
            playerwinLight.SetActive(true);
            buttonsEndGame.SetActive(true);
            Confety.SetActive(true);
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
            Debug.Log("Game over entered");
            //TODO: Show Game Over UI, etc.

            player_anim.SetBool("Defeat", true);
            enemy_anim.SetBool("Victory", true);
            enemywinLight.SetActive(true);
            loseText.SetActive(true);
            Confety.SetActive(true);
            buttonsEndGame.SetActive(true);
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
        Debug.Log(rounds_enemy);
        Debug.Log(rounds_enemy);
        rounds_enemy.transform.Find((rounds - win_player).ToString()).gameObject.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetGame()
    {
        win_player = 0;
        rounds = 0;
        player_anim.SetBool("Victory", false);
        player_anim.SetBool("Defeat", false);
        enemy_anim.SetBool("Victory", false);
        enemy_anim.SetBool("Defeat", false);
        Enemy.GetComponent<EnemyManager>().difficult = EnemyManager.Difficult.EASY;
        buttonsEndGame.SetActive(false);
        winText.SetActive(false);
        playerwinLight.SetActive(false);
        enemywinLight.SetActive(false);
        loseText.SetActive(false);
        Confety.SetActive(false);
    }
}
