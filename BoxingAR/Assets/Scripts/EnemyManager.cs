using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Entity
{
    private GameSystem gameManager;

    protected override void Start()
    {
        gameManager = FindObjectOfType<GameSystem>();
        base.Start();
    }

    protected override void EntityDead()
    {
        gameManager.GameWin();
    }
}
