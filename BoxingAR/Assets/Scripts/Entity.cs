﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum ENTITY_STATE
    {
        IDLE,
        PUNCH_ANTICIPATION,
        PUNCH,
        PUNCH_RECOVERY,
        DODGE_ANTICIPATION,
        DODGE,
        DODGE_RECOVERY,
    };

    public enum DIRECTION
    {
        LEFT,
        MIDDLE,
        RIGHT,
        INVALID
    };

    protected ENTITY_STATE entityState = ENTITY_STATE.IDLE;
    protected DIRECTION entityPos = DIRECTION.MIDDLE;
    protected DIRECTION punchDir = DIRECTION.INVALID;

    public Entity opponent;

    protected static float maxLife = 120;
    protected float currLife = maxLife;

    protected float punchDamage = 15;
    [HideInInspector] public bool hitByLastAttack = false;

    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        switch (entityState)
        {
            case ENTITY_STATE.PUNCH:
                {
                    //We check every every frame that's on the punch state (it's like a collider that stays every few frames)
                    opponent.CheckHit(punchDir, punchDamage);
                }
                break;
            default:
                {
                }
                break;
        }
    }

    public void CheckHit(DIRECTION punchDir, float damage)
    {
        if(hitByLastAttack)
        {
            return;
        }

        if (punchDir == DIRECTION.LEFT)
        {
            if (entityPos == DIRECTION.LEFT || entityPos == DIRECTION.MIDDLE)
            {
                currLife -= damage;
                anim.SetTrigger("LeftHit");
                hitByLastAttack = true;
                if (IsDead())
                {
                    EntityDead();
                }
            }
            //TODO: Possible thing, add "tired" frames where you are exposed if you miss
        }
        else if (punchDir == DIRECTION.RIGHT)
        {
            if (entityPos == DIRECTION.RIGHT || entityPos == DIRECTION.MIDDLE)
            {
                currLife -= damage;
                anim.SetTrigger("RightHit");
                Debug.Log("right attack");
                hitByLastAttack = true;
                if (IsDead())
                {
                    EntityDead();
                }
            }
            //TODO: Possible thing, add "tired" frames where you are exposed if you miss
        }
    }

    public bool IsDead()
    {
        return maxLife <= 0;
    }

    protected virtual void EntityDead()
    {}

    public void BackToIdle()
    {
        entityState = ENTITY_STATE.IDLE;
        entityPos = DIRECTION.MIDDLE;
        punchDir = DIRECTION.INVALID;
    }

    public void PunchEvent()
    {
        entityState = ENTITY_STATE.PUNCH;
    }

    public void PunchRecoveryEvent()
    {
        entityState = ENTITY_STATE.PUNCH_RECOVERY;
        opponent.hitByLastAttack = false;
    }

    public void DodgeEvent()
    {
        entityState = ENTITY_STATE.DODGE;
    }

    public void DodgeRecoveryEvent()
    {
        entityState = ENTITY_STATE.DODGE_RECOVERY;
    }
}
