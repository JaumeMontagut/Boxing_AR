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

    protected static float maxLife = 120;
    protected float currLife = maxLife;

    protected float punchDamage = 15;
    [HideInInspector] public bool hitByLastAttack = false;

    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
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

        if (punchDir == DIRECTION.LEFT)
        {
            if (entityPos == DIRECTION.LEFT || entityPos == DIRECTION.MIDDLE)
            {
                currLife -= damage;
                lifeBar.value = currLife / maxLife;
                anim.SetTrigger("LeftHit");//TODO: Set RightHit or LeftHit depending on the way the entity is positioned (it could be in the middle, ideling to the right or left)
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
                lifeBar.value = currLife / maxLife;
                anim.SetTrigger("RightHit");//TODO: Set RightHit or LeftHit depending on the way the entity is positioned (it could be in the middle, ideling to the right or left)
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

    public void DodgeEvent()
    {
        entityState = ENTITY_STATE.DODGE;
    }

    public void DodgeRecoveryEvent()
    {
        entityState = ENTITY_STATE.DODGE_RECOVERY;
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
