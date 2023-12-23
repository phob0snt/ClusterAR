using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private PlayerScript player;

    private void FixedUpdate()
    {
        if (player.isMoving)
            anim.Play();
        else
            anim.Stop();
    }
}
