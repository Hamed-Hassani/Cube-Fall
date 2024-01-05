using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScripts : MonoBehaviour
{

    public float move_Speed = 2f;
    public float bound_Y = 6f;

    public bool moving_Platform_left, moving_Platform_Right, is_Breakable, is_Spike, is_Platform;
    private Animator anim;
    void Awake()
    {
        if (is_Breakable)
        {
            anim = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        Vector2 temp = transform.position;
        temp.y += move_Speed * Time.deltaTime;
        transform.position = temp;

        if(temp.y >= bound_Y)
        {
            gameObject.SetActive(false);
        }
    }

    void breakableDeactive()
    {
        Invoke("DeactivateGameObject", 0.35f);
    }

    void DeactiveteGameObject()
    {
        SoundManager.instance.IceBreakSound();
        gameObject.SetActive(false);
    }

     void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            if (is_Spike)
            {
                target.transform.position = new Vector2(1000f, 1000f);
                SoundManager.instance.GameOverSounds();
                GameManager.instance.RestartGame();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (is_Breakable)
            {
                SoundManager.instance.LandSound();
                anim.Play("Break");
            }

            if (is_Platform)
            {
                SoundManager.instance.LandSound();
            }
        }
    }

    private void OnCollisionStay(Collision target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (moving_Platform_left)
            {
                target.gameObject.GetComponent<PlayerMovement>().PlatformMove(-1f);
            }

            if (moving_Platform_Right)
            {
                target.gameObject.GetComponent<PlayerMovement>().PlatformMove(1f);
            }
        }
    }
}
