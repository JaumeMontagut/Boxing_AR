using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : Entity
{
    private GameSystem gameManager;

    protected override void Start()
    {
        gameManager = FindObjectOfType<GameSystem>();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        //Debug.Log(entityState.ToString());
        //DEBUG
        if (Input.GetKeyDown(KeyCode.W))
        {
            currLife -= 25;
            lifeBar.value = currLife / maxLife;
            if (opponent.IsDead())
                opponent.EntityDead();
        }
    }

    public void RequestRightPunch()
    {
        if (entityState == ENTITY_STATE.IDLE)
        {
            entityState = ENTITY_STATE.PUNCH_ANTICIPATION;
            punchDir = DIRECTION.RIGHT;
            anim.SetTrigger("RightPunch");
            ChargingParticlesR.enableEmission = true;
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
            punchDir = DIRECTION.LEFT;
            anim.SetTrigger("LeftPunch");
            ChargingParticlesL.enableEmission = true;
        }
        else
        {
            //TODO: Play error sound / tired
        }

    }

    public void RequestRightPunchRelease()
    {
        ChargingParticlesR.enableEmission = false;
        if (entityState == ENTITY_STATE.PUNCH_ANTICIPATION)
        {
            entityState = ENTITY_STATE.PUNCH_RELEASE;
            anim.SetTrigger("RightReleasePunch");
        }
        else
        {
            //TODO: Play error sound / tired
        }
    }

    public void RequestLeftPunchRelease()
    {
        ChargingParticlesL.enableEmission = false;
        if (entityState == ENTITY_STATE.PUNCH_ANTICIPATION)
        {
            entityState = ENTITY_STATE.PUNCH_RELEASE;
            anim.SetTrigger("LeftReleasePunch");
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
            entityState = ENTITY_STATE.DODGE;
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
            entityState = ENTITY_STATE.DODGE;
            anim.SetTrigger("LeftDodge");
        }
        else
        {
            //TODO: Play error sound / tired
        }
    }

    public override void EntityDead()
    {
        gameManager.GameOver();
        ResetLife();
        opponent.ResetLife();
    }
}
