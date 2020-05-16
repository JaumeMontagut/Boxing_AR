using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   private Animator anim;

   public enum PLAYER_STATES   
    {
        IDLE, 
        RIGHT_HIT,
        LEFT_HIT,
    };

   public PLAYER_STATES player_state = PLAYER_STATES.IDLE;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player_state);
       
    }
    public void RightPunch()
    {
        player_state = PLAYER_STATES.RIGHT_HIT;
        anim.SetTrigger("RightHit");
    }

    public void LeftPunch()
    {
        player_state = PLAYER_STATES.LEFT_HIT;
        anim.SetTrigger("LeftHit");
    }
}
