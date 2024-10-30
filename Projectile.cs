// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Projectile : MonoBehaviour
// {
//     private void OnCollisionEnter(Collision collision)
//     {
//         // ตรวจสอบให้ Weapon_player1 หายไปเมื่อชนกับสิ่งที่ไม่ใช่ Player1
//         if (gameObject.CompareTag("Weapon_player1") && !collision.gameObject.CompareTag("Player1"))
//         {
//             Destroy(gameObject); // ทำลายโปรเจคไทล์
//         }

//         // ตรวจสอบให้ Weapon_player2 หายไปเมื่อชนกับสิ่งที่ไม่ใช่ Player2
//         if (gameObject.CompareTag("Weapon_player2") && !collision.gameObject.CompareTag("Player2"))
//         {
//             Destroy(gameObject); // ทำลายโปรเจคไทล์
//         }
//     }
// }
