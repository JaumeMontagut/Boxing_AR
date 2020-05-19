using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public enum ENTITY_STATE
    {
        IDLE,
        PUNCH_ANTICIPATION,
        PUNCH_RELEASE,
        PUNCH,
        PUNCH_RECOVERY,
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
    public Slider lifeBar;
    AudioSource HitSound;
    public ParticleSystem ChargingParticlesR;
    public ParticleSystem ChargingParticlesL;

    protected static float maxLife = 120f;
    protected float currLife = maxLife;

    protected float timerPunch = 0f;
    public float multiplierPunch = 30f;

    protected float punchDamage = 15f;
    [HideInInspector] public bool hitByLastAttack = false;
    [HideInInspector] public bool start = false;

    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        HitSound = gameObject.transform.Find("HitSound").GetComponent<AudioSource>();
        //ChargingParticlesR = gameObject.transform.Find("ChargingParticalsR").GetComponent<ParticleSystem>();
        //ChargingParticlesL = gameObject.transform.Find("ChargingParticalsL").GetComponent<ParticleSystem>();
        ChargingParticlesR.enableEmission = false;
        ChargingParticlesL.enableEmission = false;
    }

    protected virtual void Update()
    {
        if (start)
        {
            switch (entityState)
            {
                case ENTITY_STATE.PUNCH:
                    {
                        //We check every every frame that's on the punch state (it's like a collider that stays every few frames)
                        opponent.CheckHit(punchDir, punchDamage + (Time.time - timerPunch) * multiplierPunch);
                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }
    }

    public void CheckHit(DIRECTION punchDir, float damage)
    {
        if(hitByLastAttack)
        {
            return;
        }

        if (punchDir == DIRECTION.RIGHT)
        {
            if (entityPos == DIRECTION.LEFT || entityPos == DIRECTION.MIDDLE)
            {
                ReceiveHit(damage, "HitR");
            }
            //TODO: Possible thing, add "tired" frames where you are exposed if you miss
        }
        else if (punchDir == DIRECTION.LEFT)
        {
            if (entityPos == DIRECTION.RIGHT || entityPos == DIRECTION.MIDDLE)
            {
                ReceiveHit(damage, "HitL");
            }
            //TODO: Possible thing, add "tired" frames where you are exposed if you miss
        }
    }

    private void ReceiveHit(float damage, string triggerName)
    {
        currLife -= damage;
        lifeBar.value = currLife / maxLife;
        anim.ResetTrigger("LeftPunch");
        anim.ResetTrigger("RightPunch");
        anim.ResetTrigger("LeftReleasePunch");
        anim.ResetTrigger("RightReleasePunch");
        anim.ResetTrigger("LeftDodge");
        anim.ResetTrigger("RightDodge");
        Hitted();
        //TODO: Set RightHit or LeftHit depending on the way the entity is positioned (it could be in the middle, ideling to the right or left)
        //If we do this we don't even need to pass the "trigger name"
        anim.Play(triggerName);
        hitByLastAttack = true;
        if (HitSound)
        {
            HitSound.Play();
        }
        if (IsDead())
        {
            EntityDead();
        }
    }

    protected virtual void Hitted() { }

    public bool IsDead()
    {
        return currLife <= 0;
    }

    //protected virtual void EntityDead() FOR DEBUGGING
    public virtual void EntityDead()
    { }

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

    public void DodgeRecoveryEvent()
    {
        entityState = ENTITY_STATE.DODGE_RECOVERY;
        entityPos = DIRECTION.MIDDLE;
    }

    public ENTITY_STATE GetEntityState()
    {
        return entityState;
    }

    public DIRECTION GetPunchDirection()
    {
        return punchDir;
    }

    public void ResetLife()
    {
        currLife = maxLife;
        lifeBar.value = 1f;
    }
}
