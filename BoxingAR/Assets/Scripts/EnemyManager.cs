using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Entity
{
    private GameSystem gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameSystem>();
    }

    protected override void EntityDead()
    {
        gameManager.GameWin();
    }
}
