using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator player;

    private void Awake()
    {
        player = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("I'm Throwing");
            player.SetBool("Player1_isThrow", true);
        }
        if (Input.GetMouseButtonUp(0)) // ถ้าปุ่มเมาส์ถูกปล่อย
        {
            Debug.Log("finisend throw");
            player.SetBool("Player1_isThrow", false);
        }
    }
}
