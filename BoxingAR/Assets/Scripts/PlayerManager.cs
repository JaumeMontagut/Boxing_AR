using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Entity
{
    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void RequestRightPunch()
    {
        if (entityState == ENTITY_STATE.IDLE)
        {
            entityState = ENTITY_STATE.PUNCH_ANTICIPATION;
            anim.SetTrigger("RightPunch");
        }
        else
        {
            //TODO: Play error sound / tired
        }
    }

    public void RequestLeftPunch()
    {
        if (entityState == ENTITY_STATE.IDLE)
        {
            entityState = ENTITY_STATE.PUNCH_ANTICIPATION;
            anim.SetTrigger("LeftPunch");
        }
        else
        {
            //TODO: Play error sound / tired
        }

    }

    public void RequestRightDodge()
    {
        if (entityState == ENTITY_STATE.IDLE)
        {
            entityState = ENTITY_STATE.DODGE_ANTICIPATION;
            anim.SetTrigger("RightDodge");
        }
        else
        {
            //TODO: Play error sound / tired
        }
    }

    public void RequestLeftDodge()
    {
        if (entityState == ENTITY_STATE.IDLE)
        {
            entityState = ENTITY_STATE.DODGE_ANTICIPATION;
            anim.SetTrigger("LeftDodge");
        }
        else
        {
            //TODO: Play error sound / tired
        }
    }

    public void LeftPunchEvent()
    {
        entityState = ENTITY_STATE.PUNCH;
    }

    public void RightPunchEvent()
    {
        entityState = ENTITY_STATE.PUNCH;
    }

    public void LeftPunchRecoveryEvent()
    {
        entityState = ENTITY_STATE.PUNCH_RECOVERY;
        opponent.hitByLastAttack = false;
    }

    public void RightPunchRecoveryEvent()
    {
        entityState = ENTITY_STATE.PUNCH_RECOVERY;
        opponent.hitByLastAttack = false;
    }


}
