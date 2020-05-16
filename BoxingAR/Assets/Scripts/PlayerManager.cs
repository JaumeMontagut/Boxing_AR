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

    void Update()
    {
        Debug.Log(player_state);
        //switch (player_state)
        //{
        //    case PLAYER_STATES.IDLE:
        //        {
        //        }
        //        break;
        //    case PLAYER_STATES.LEFT_HIT:
        //        {
        //        }
        //        break;
        //    case PLAYER_STATES.RIGHT_HIT:
        //        {
        //        }
        //        break;
        //    case PLAYER_STATES.LEFT_DODGE:
        //        {
        //        }
        //        break;
        //    case PLAYER_STATES.RIGHT_DODGE:
        //        {
        //        }
        //        break;
        //    default:
        //        {
        //        }
        //        break;
        //}
    }
    public void RequestRightPunch()
    {
        if (player_state == PLAYER_STATES.IDLE)
        {
            player_state = PLAYER_STATES.RIGHT_HIT;
            anim.SetTrigger("RightHit");
        }
        else
        {
            //TODO: Play error sound / tired
        }
    }

    public void RequestLeftPunch()
    {
        if (player_state == PLAYER_STATES.IDLE)
        {
            player_state = PLAYER_STATES.LEFT_HIT;
            anim.SetTrigger("LeftHit");
        }
        else
        {
            //TODO: Play error sound / tired
        }

    }

    public void RequestRightDodge()
    {
        if (player_state == PLAYER_STATES.IDLE)
        {
            player_state = PLAYER_STATES.RIGHT_DODGE;
            anim.SetTrigger("RightDodge");
        }
        else
        {
            //TODO: Play error sound / tired
        }
    }

    public void RequestLeftDodge()
    {
        if (player_state == PLAYER_STATES.IDLE)
        {
            player_state = PLAYER_STATES.RIGHT_DODGE;
            anim.SetTrigger("LeftDodge");
        }
        else
        {
            //TODO: Play error sound / tired
        }
    }

    //TODO: Check in which state the other player is
    public void LeftPunchAnimationEvent()
    {

    }


    public void RightPunchAnimationEvent()
    {

    }
}
