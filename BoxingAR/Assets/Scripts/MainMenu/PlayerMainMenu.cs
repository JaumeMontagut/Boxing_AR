using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainMenu : MonoBehaviour
{
    Animator anim;

    public Vector2 minmaxRandom = new Vector2(2f, 5f);
    public bool dodge_animation = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("RandomHit", Random.Range(minmaxRandom.x, minmaxRandom.y));
    }

    void RandomHit()
    {
        if (dodge_animation)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    anim.SetBool("LeftHit", true);
                    break;
                case 1:
                    anim.SetBool("RightHit", true);
                    break;
                case 2:
                    anim.SetBool("RightDodging", true);
                    break;
                case 3:
                    anim.SetBool("LeftDodging", true);
                    break;
            }
        }
        else
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    anim.SetBool("LeftHit", true);
                    break;
                case 1:
                    anim.SetBool("RightHit", true);
                    break;
            }
        }

        Invoke("RandomHit", Random.Range(minmaxRandom.x, minmaxRandom.y));
    }
}
