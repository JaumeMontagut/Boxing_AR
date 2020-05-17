﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Entity
{
    private GameSystem gameManager;

    public enum Difficult
    {
        EASY, MEDIUM, HARD
    }

    public Difficult difficult = Difficult.MEDIUM;

    float timer = 0f;
    float timeBtwAttacks = 0f;

    protected override void Start()
    {
        gameManager = FindObjectOfType<GameSystem>();
        base.Start();

        timer = Time.time;
        timeBtwAttacks = Random.Range(5f, 10f);
    }

    protected override void Update()
    {
        base.Update();

        if (entityState == ENTITY_STATE.IDLE)
        {
            switch (difficult)
            {
                case Difficult.EASY:
                    LogicEasyAttack();
                    break;
                case Difficult.MEDIUM:
                    LogicMediumAttack();
                    break;
                case Difficult.HARD:
                    LogicHardAttack();
                    break;
            }
        }
    }

    private void LogicEasyAttack()
    {
        if(Time.time - timer >= timeBtwAttacks)
        {
            switch(Random.Range(0, 4))
            {
                case 0:
                    entityState = ENTITY_STATE.PUNCH_ANTICIPATION;
                    punchDir = DIRECTION.LEFT;
                    anim.SetTrigger("LeftPunch");
                    break;
                case 1:
                    entityState = ENTITY_STATE.PUNCH_ANTICIPATION;
                    punchDir = DIRECTION.RIGHT;
                    anim.SetTrigger("RightPunch");
                    break;
                case 2:
                    entityState = ENTITY_STATE.DODGE_ANTICIPATION;
                    entityPos = DIRECTION.RIGHT;
                    anim.SetTrigger("RightDodge");
                    break;
                case 3:
                    entityState = ENTITY_STATE.DODGE_ANTICIPATION;
                    entityPos = DIRECTION.LEFT;
                    anim.SetTrigger("LeftDodge");
                    break;
            }
            timer = Time.time;
        }
    }

    private void LogicMediumAttack()
    {
        if (Time.time - timer >= timeBtwAttacks)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    entityState = ENTITY_STATE.PUNCH_ANTICIPATION;
                    punchDir = DIRECTION.LEFT;
                    anim.SetTrigger("LeftPunch");
                    break;
                case 1:
                    entityState = ENTITY_STATE.PUNCH_ANTICIPATION;
                    punchDir = DIRECTION.RIGHT;
                    anim.SetTrigger("RightPunch");
                    break;
            }
            timer = Time.time;
        }
        else
        {
            if (opponent.GetEntityState() == ENTITY_STATE.PUNCH_ANTICIPATION)
            {
                if (Random.Range(0, 100) >= 50)
                {
                    entityState = ENTITY_STATE.DODGE_ANTICIPATION;
                    switch (opponent.GetPunchDirection())
                    {
                        case DIRECTION.LEFT:
                            entityPos = DIRECTION.RIGHT;
                            anim.SetTrigger("RightDodge");
                            break;
                        case DIRECTION.RIGHT:
                            entityPos = DIRECTION.LEFT;
                            anim.SetTrigger("LeftDodge");
                            break;
                    }
                }
            }
        }
    }

    private void LogicHardAttack()
    {
        if(opponent.GetEntityState() == ENTITY_STATE.PUNCH_ANTICIPATION)
        {
            entityState = ENTITY_STATE.DODGE_ANTICIPATION;
            switch (opponent.GetPunchDirection())
            {
                case DIRECTION.LEFT:
                    entityPos = DIRECTION.RIGHT;
                    anim.SetTrigger("RightDodge");
                    break;
                case DIRECTION.RIGHT:
                    entityPos = DIRECTION.LEFT;
                    anim.SetTrigger("LeftDodge");
                    break;
            }
        }
    }

    protected override void EntityDead()
    {
        gameManager.GameWin();
    }
}
