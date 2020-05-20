using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : Entity
{
    private GameSystem gameManager;
    private AudioSource chargingSound;
    private AudioSource uiSound;

    public Image rightPunchFill;
    public Image leftPunchFill;

    public bool lastFrames;//Used to register events while in some animation

    protected override void Start()
    {
        gameManager = FindObjectOfType<GameSystem>();
        chargingSound = transform.Find("ChargingSound").GetComponent<AudioSource>();
        uiSound = transform.Find("UISound").GetComponent<AudioSource>();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        leftPunchFill.fillAmount = (Time.time - timerPunch);
        rightPunchFill.fillAmount = (Time.time - timerPunch);

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
            ChargingParticlesR.enableEmission = true;
            uiSound.Play();
            chargingSound.Play();
            timerPunch = Time.time;//TODO: Remove this, if you press on the frames before you're going to have an advantage (but the visual should still be there to indicate you)
            rightPunchFill.enabled = true;
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
            anim.ResetTrigger("LeftReleasePunch");
            ChargingParticlesR.enableEmission = true;
            uiSound.Play();
            chargingSound.Play();
            timerPunch = Time.time;//TODO: Remove this, if you press on the frames before you're going to have an advantage (but the visual should still be there to indicate you)
            leftPunchFill.enabled = true;
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
    }

    public override void StartPunchRight()
    {
        punchDir = DIRECTION.RIGHT;
        entityState = ENTITY_STATE.PUNCH_ANTICIPATION;
    }

    public void RequestRightPunchRelease()
    {
        if (entityState == ENTITY_STATE.IDLE || entityState == ENTITY_STATE.PUNCH_ANTICIPATION || lastFrames)
        {
            ChargingParticlesR.enableEmission = false;
            uiSound.Play();
            chargingSound.Stop();
            rightPunchFill.enabled = false;
            anim.SetTrigger("RightReleasePunch");
        }
        else
        {
            //TODO: Play error sound / tired
        }
    }

    public void RequestLeftPunchRelease()
    {
        if (entityState == ENTITY_STATE.IDLE || entityState == ENTITY_STATE.PUNCH_ANTICIPATION || lastFrames)
        {
            ChargingParticlesL.enableEmission = false;
            uiSound.Play();
            chargingSound.Stop();
            leftPunchFill.enabled = false;
            anim.SetTrigger("LeftReleasePunch");
        }
        else
        {
            //TODO: Play error sound / tired
        }
    }

    public override void RightPunchReleaseEvent()
    {
        entityState = ENTITY_STATE.PUNCH_RELEASE;
    }

    public override void LeftPunchReleaseEvent()
    {
        entityState = ENTITY_STATE.PUNCH_RELEASE;
    }

    public void RequestRightDodge()
    {
        if (entityState == ENTITY_STATE.IDLE)
        {
            entityState = ENTITY_STATE.DODGE;
            entityPos = DIRECTION.RIGHT;
            uiSound.Play();
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
            uiSound.Play();
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
