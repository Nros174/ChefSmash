using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCollision : MonoBehaviour 
{
    public CustomerManager customerManager; // สร้างตัวแปรอ้างอิงถึง CustomerManager

    private void OnCollisionEnter(Collision collision)
    {
        // ตรวจสอบการชนกันของ Player1 กับอาวุธของ Player2
        if (gameObject.CompareTag("Player1") && collision.gameObject.CompareTag("Weapon_player2"))
        {
            customerManager.health_player1--; // ลดพลังชีวิตของ Player1
            Debug.Log("Player1 ถูกโจมตี! พลังชีวิตเหลือ: " + customerManager.health_player1);
            if(customerManager.health_player1 <= 0){
                gameObject.SetActive(false); // ปิดการใช้งาน Player1
            }
        }

        // ตรวจสอบการชนกันของ Player2 กับอาวุธของ Player1
        if (gameObject.CompareTag("Player2") && collision.gameObject.CompareTag("Weapon_player1"))
        {
            customerManager.health_player2--; // ลดพลังชีวิต
             if(customerManager.health_player2 <= 0){
                gameObject.SetActive(false); // ปิดการใช้งาน Player1
            }
        }

    }
 }