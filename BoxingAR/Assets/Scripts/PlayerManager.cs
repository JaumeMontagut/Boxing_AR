using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   public enum PLAYER_STATES   
    {
        IDLE, 
        RIGHT_HIT,
        LEFT_HIT,
    };
    
    [HideInInspector] public PLAYER_STATES player_state = PLAYER_STATES.IDLE;
    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Debug.Log(player_state);
        switch (player_state)
        {
            case PLAYER_STATES.IDLE:
                {

                }
                break;
            case PLAYER_STATES.LEFT_HIT:
                {

                }
                break;
            case PLAYER_STATES.RIGHT_HIT:
                {

                }
                break;
            default:
                {
                    Debug.LogWarning("Player shouldn't enter here. There's a case that's not been coded.");
                }
                break;
        }
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
        }
        else
        {
            //TODO: Play error sound / tired
        }
    }
}
