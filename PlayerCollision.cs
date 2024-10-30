using System.Collections; // นำเข้า namespace สำหรับการใช้ Collections
using System.Collections.Generic; // นำเข้า namespace สำหรับการใช้ Collections แบบ Generic
using UnityEngine; // นำเข้า namespace สำหรับ UnityEngine

public class PlayerCollision : MonoBehaviour // สร้างคลาส PlayerCollision ที่สืบทอดมาจาก MonoBehaviour
{
    public CustomerManager customerManager; // สร้างตัวแปรอ้างอิงถึง CustomerManager

    private void OnCollisionEnter(Collision collision) // ฟังก์ชันที่ถูกเรียกเมื่อเกิดการชนกัน
    {
        // ตรวจสอบการชนกันของ Player1 กับอาวุธของ Player2
        if (gameObject.CompareTag("Player1") && collision.gameObject.CompareTag("Weapon_player2"))
        {
            customerManager.health_player1--; // ลดพลังชีวิตของ Player1
            Debug.Log("Player1 ถูกโจมตี! พลังชีวิตเหลือ: " + customerManager.health_player1); // แสดงข้อความใน Console
            if (customerManager.health_player1 <= 0) // ถ้าพลังชีวิตลดลงถึง 0
            {
                gameObject.SetActive(false); // ปิดการใช้งาน Player1
            }
        }

        // ตรวจสอบการชนกันของ Player2 กับอาวุธของ Player1
        if (gameObject.CompareTag("Player2") && collision.gameObject.CompareTag("Weapon_player1"))
        {
            customerManager.health_player2--; // ลดพลังชีวิตของ Player2
            if (customerManager.health_player2 <= 0) // ถ้าพลังชีวิตลดลงถึง 0
            {
                gameObject.SetActive(false); // ปิดการใช้งาน Player2
            }
        }
    }
}
