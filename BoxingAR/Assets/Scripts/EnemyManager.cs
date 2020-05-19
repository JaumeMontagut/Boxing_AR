using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : Entity
{
    private GameSystem gameManager;

    public enum Difficult
    {
        PUNCH_BAG, EASY, MEDIUM, HARD
    }

    public Difficult difficult = Difficult.MEDIUM;

    float timerAttack = 0f;
    float timerCharge = 0f;
    float timeBtwAttacks = 0f;
    float timeCharging = 0f;
    public Vector2 randomTimeToAttack = new Vector2(5f, 10f);

    [Header("EASY")]
    public Vector2 randomTimeChargeEasy = new Vector2(0.5f, 3f);
    [Header("MEDIUM")]
    public Vector2 randomTimeChargeMedium = new Vector2(0.5f, 3f);
    [Header("HARD")]
    public Vector2 randomTimeChargeHard = new Vector2(0.5f, 3f);

    protected override void Start()
    {
        gameManager = FindObjectOfType<GameSystem>();
        base.Start();

        timerAttack = Time.time;
        timerCharge = Time.time;
        timeBtwAttacks = Random.Range(randomTimeToAttack.x, randomTimeToAttack.y);
    }

    protected override void Update()
    {
        base.Update();

        if (entityState == ENTITY_STATE.IDLE)
        {
            switch (difficult)
            {
                case Difficult.PUNCH_BAG:
                    //Literally does nothing
                    break;
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
        else if (entityState == ENTITY_STATE.PUNCH_ANTICIPATION)
        {
            if (Time.time - timerCharge >= timeCharging)
            {
                entityState = ENTITY_STATE.PUNCH_RELEASE;
                switch (punchDir)
                {
                    case DIRECTION.LEFT:
                        anim.SetTrigger("LeftReleasePunch");
                        break;
                    case DIRECTION.RIGHT:
                        anim.SetTrigger("RightReleasePunch");
                        break;
                }
                timerAttack = Time.time;
                timeBtwAttacks = Random.Range(randomTimeToAttack.x, randomTimeToAttack.y);
            }
        }

        //DEBUG
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currLife -= 25;
            lifeBar.value = currLife / maxLife;
            if (opponent.IsDead())
                opponent.EntityDead();
        }
    }

    private void LogicEasyAttack()
    {
        if (Time.time - timerAttack >= timeBtwAttacks)
        {
            switch (Random.Range(0, 4))
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
            timerCharge = Time.time;
            timeCharging = Random.Range(randomTimeChargeEasy.x, randomTimeChargeEasy.y);
        }
    }

    private void LogicMediumAttack()
    {
        if (Time.time - timerAttack >= timeBtwAttacks)
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
            timerCharge = Time.time;
            timeCharging = Random.Range(randomTimeChargeMedium.x, randomTimeChargeMedium.y);
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
        if (opponent.GetEntityState() == ENTITY_STATE.PUNCH_ANTICIPATION)
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
        else if (Time.time - timerAttack >= timeBtwAttacks)
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
            timerCharge = Time.time;
            timeCharging = Random.Range(randomTimeChargeHard.x, randomTimeChargeHard.y);
        }
    }

    public override void EntityDead()
    {
        gameManager.GameWin();
        ResetLife();
        opponent.ResetLife();
    }
}