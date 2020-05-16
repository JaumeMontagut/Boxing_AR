using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum PLAYER_STATES
    {
        IDLE,
        RIGHT_HIT,
        LEFT_HIT,
        LEFT_DODGE,
        RIGHT_DODGE,
        DODGE_ANTICIPATION,
        DODGE,
        DODGE_RECOVERY,
    };

    public enum DIRECTION
    {
        LEFT,
        MIDDLE,
        RIGHT
    };

    [HideInInspector] public PLAYER_STATES player_state = PLAYER_STATES.IDLE;
    [HideInInspector] public DIRECTION player_pos = DIRECTION.MIDDLE;
    [HideInInspector] public float life;
    
    private GameSystem gameManager;

    public void Start()
    {
        gameManager = FindObjectOfType<GameSystem>();
    }

    public void CheckHit(DIRECTION punchDir, float damage)
    {
        if (punchDir == DIRECTION.LEFT)
        {
            if (player_pos == DIRECTION.LEFT || player_pos == DIRECTION.MIDDLE)
            {
                life -= damage;
                if (IsDead())
                {
                    gameManager.GameOver();
                }
            }
            //TODO: Possible thing, add "tired" frames where you are exposed if you miss
        }
        else if (punchDir == DIRECTION.RIGHT)
        {
            if (player_pos == DIRECTION.RIGHT || player_pos == DIRECTION.MIDDLE)
            {
                life -= damage;
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
        return life <= 0;
    }
}
