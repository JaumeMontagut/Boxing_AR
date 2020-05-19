using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : Entity
{
    private GameSystem gameManager;
    public AudioSource chargingSound;

    public Image rightPunchFill;
    public Image leftPunchFill;

    public bool lastFrames;//Used to register events while in some animation

    protected override void Start()
    {
        gameManager = FindObjectOfType<GameSystem>();
        chargingSound = gameObject.transform.Find("ChargingSound").GetComponent<AudioSource>();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if(entityState == ENTITY_STATE.PUNCH_ANTICIPATION)
        {
            switch (punchDir)
            {
                case DIRECTION.LEFT:
                    leftPunchFill.fillAmount = (Time.time - timerPunch);
                    break;
                case DIRECTION.RIGHT:
                    rightPunchFill.fillAmount = (Time.time - timerPunch);
                    break;
            }
        }

        //DEBUG
        if (Input.GetKeyDown(KeyCode.W))
        {
            currLife -= 25;
            lifeBar.value = currLife / maxLife;
            if (opponent.IsDead())
                opponent.EntityDead();
        }
    }

    public override void LastFrames()
    {
        lastFrames = true;
    }

    public override void BackToIdle()
    {
        lastFrames = false;
        base.BackToIdle();
    }

    public void RequestRightPunch()
    {
        if (entityState == ENTITY_STATE.IDLE || lastFrames)
        {
            anim.SetTrigger("RightPunch");
            anim.ResetTrigger("RightReleasePunch");
        }
        else
        {
            //TODO: Play error sound / tired
        }
    }

    public void RequestLeftPunch()
    {
        if (entityState == ENTITY_STATE.IDLE || lastFrames)
        {
            anim.SetTrigger("LeftPunch");
            anim.SetTrigger("LeftReleasePunch");
        }
        else
        {
            //TODO: Play error sound / tired
        }

    }

    public override void StartPunchLeft()
    {
        punchDir = DIRECTION.LEFT; 
        entityState = ENTITY_STATE.PUNCH_ANTICIPATION;
        ChargingParticlesR.enableEmission = true;
        chargingSound.Play();
        timerPunch = Time.time;
    }

    public override void StartPunchRight()
    {
        punchDir = DIRECTION.RIGHT;
        entityState = ENTITY_STATE.PUNCH_ANTICIPATION;
        ChargingParticlesR.enableEmission = true;
        chargingSound.Play();
        timerPunch = Time.time;
    }

    public void RequestRightPunchRelease()
    {
        ChargingParticlesR.enableEmission = false;
        chargingSound.Stop();
        rightPunchFill.fillAmount = 0f;
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
        chargingSound.Stop();
        leftPunchFill.fillAmount = 0f;
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
            entityPos = DIRECTION.RIGHT;
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
            entityPos = DIRECTION.LEFT;
            anim.SetTrigger("LeftDodge");
        }
        else
        {
            //TODO: Play error sound / tired
        }
    }

    protected override void Hitted()
    {
        base.Hitted();
        chargingSound.Stop();
        leftPunchFill.fillAmount = 0f;
        rightPunchFill.fillAmount = 0f;
    }

    public override void EntityDead()
    {
        gameManager.GameOver();
    }
}
