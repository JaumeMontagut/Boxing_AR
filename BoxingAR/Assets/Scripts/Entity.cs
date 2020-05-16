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

    public enum PLAYER_POSITION
    {
        LEFT,
        MIDDLE,
        RIGHT
    };

    [HideInInspector] public PLAYER_STATES player_state = PLAYER_STATES.IDLE;
    [HideInInspector] public PLAYER_POSITION player_pos = PLAYER_POSITION.MIDDLE;
    [HideInInspector] public float life;

    public bool IsDead()
    {
        return life <= 0;
    }
}
