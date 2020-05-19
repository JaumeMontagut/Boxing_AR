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
    public Slider lifeBar;
    public AudioSource HitSound;

    protected static float maxLife = 120;
    protected float currLife = maxLife;

    protected float punchDamage = 15;
    [HideInInspector] public bool hitByLastAttack = false;

    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        HitSound = gameObject.transform.Find("HitSound").GetComponent<AudioSource>();
    }

    protected virtual void Update()
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
        //TODO: Set RightHit or LeftHit depending on the way the entity is positioned (it could be in the middle, ideling to the right or left)
        //If we do this we don't even need to pass the "trigger name"
        anim.Play("HitL");
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

    public void LeftDodgeEvent()
    {
        entityState = ENTITY_STATE.DODGE;
        entityPos = DIRECTION.LEFT;
    }

    public void RightDodgeEvent()
    {
        entityState = ENTITY_STATE.DODGE;
        entityPos = DIRECTION.RIGHT;
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
