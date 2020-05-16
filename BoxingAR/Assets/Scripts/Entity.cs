using System.Collections;
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

    [HideInInspector] public ENTITY_STATE entityState = ENTITY_STATE.IDLE;
    protected DIRECTION entityPos = DIRECTION.MIDDLE;
    protected DIRECTION punchDir = DIRECTION.INVALID;

    public Entity opponent;

    protected static float maxLife = 120;
    protected float currLife = maxLife;

    protected float punchDamage = 15;
    [HideInInspector] public bool hitByLastAttack = false;

    protected GameSystem gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameSystem>();
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
                hitByLastAttack = true;
                if (IsDead())
                {
                    gameManager.GameOver();
                }
            }
            //TODO: Possible thing, add "tired" frames where you are exposed if you miss
        }
        else if (punchDir == DIRECTION.RIGHT)
        {
            if (entityPos == DIRECTION.RIGHT || entityPos == DIRECTION.MIDDLE)
            {
                currLife -= damage;
                hitByLastAttack = true;
                if (IsDead())
                {
                    gameManager.GameOver();
                }
            }
            //TODO: Possible thing, add "tired" frames where you are exposed if you miss
        }
    }

    public bool IsDead()
    {
        return maxLife <= 0;
    }
}
