using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject player;
    public float smoothSpeed = 5f;
    public Vector3 offset;
    public HeroKnight heroKnight; // HeroKnight referansı
    void LateUpdate()
    {


        if(heroKnight != null){
            if (heroKnight.m_facingDirection == 1)
            {
                offset = new Vector3(2f, 0.3f, -6.75f);
            }
            else if (heroKnight.m_facingDirection == -1)
            {
                offset = new Vector3(-2f, 0.3f, -6.75f);
            }
        }
        else
        {
        if(player.transform.localScale.x == 1){
            offset = new Vector3(0.4f, 0, -1.25f);
        }
        else if(player.transform.localScale.x == -1){
            offset = new Vector3(-0.4f, 0, -1.25f);
        }
        }

        Vector3 targetPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
