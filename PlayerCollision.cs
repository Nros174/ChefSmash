using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public CustomerManager customerManager;
    private bool hasCollided = false; // สถานะเพื่อตรวจสอบว่าชนแล้วหรือยัง

    void Awake()
    {
        customerManager = FindObjectOfType<CustomerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasCollided) return; // ถ้าชนแล้ว ให้หยุดทำงาน

        // ตรวจสอบการชนกันของ Player1 กับอาวุธของ Player2
        if (gameObject.CompareTag("Player1") && other.CompareTag("Weapon_player2"))
        {
            hasCollided = true; // ตั้งค่าให้ชนแล้ว
            customerManager.health_player1--;
            Debug.Log("Player1 ถูกโจมตี! พลังชีวิตเหลือ: " + customerManager.health_player1);
            Destroy(other.gameObject); // ลบอาวุธของ Player2
        }
        // ตรวจสอบการชนกันของ Player2 กับอาวุธของ Player1
        else if (gameObject.CompareTag("Player2") && other.CompareTag("Weapon_player1"))
        {
            hasCollided = true; // ตั้งค่าให้ชนแล้ว
            customerManager.health_player2--;
            Debug.Log("Player2 ถูกโจมตี! พลังชีวิตเหลือ: " + customerManager.health_player2);
            Destroy(other.gameObject); // ลบอาวุธของ Player1
        }
        // ตรวจสอบการชนกันกับพื้นหลัง
        else if (gameObject.CompareTag("BG") && (other.CompareTag("Weapon_player1") || other.CompareTag("Weapon_player2")))
        {
            hasCollided = true; // ตั้งค่าให้ชนแล้ว
            Debug.Log("อาวุธชนกับพื้นหลัง!");
            Destroy(other.gameObject); // ลบอาวุธที่ชนกับพื้นหลัง
        }
    }

    // ฟังก์ชันที่ใช้รีเซ็ตสถานะการชน
    public void ChangeCollided()
    {
        hasCollided = false;
    }
}
