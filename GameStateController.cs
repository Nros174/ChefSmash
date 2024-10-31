using System.Collections; // นำเข้า namespace สำหรับการใช้ Collections
using System.Collections.Generic; // นำเข้า namespace สำหรับการใช้ Collections แบบ Generic
using UnityEngine; // นำเข้า namespace สำหรับ UnityEngine

public enum TurnState { Player1_Turn, Player2_Turn } // กำหนด enum สำหรับสถานะเทิร์น

public class GameStateController : MonoBehaviour // สร้างคลาส GameStateController ที่สืบทอดมาจาก MonoBehaviour
{
    private CustomerManager customerManager; // อ้างอิง CustomerManager เพื่อเข้าถึง health_player1 และ health_player2
    private TimerController timerController; // อ้างอิง TimerController
    private Launcher launcher; // อ้างอิง Launcher
    public TurnState state; // ตัวแปรสำหรับเก็บสถานะเทิร์น

    void Start() // ฟังก์ชันที่เรียกเมื่อเริ่มต้น
    {
        // ค้นหา CustomerManager, TimerController และ Launcher ใน Scene
        customerManager = FindObjectOfType<CustomerManager>(); // ค้นหา CustomerManager
        timerController = FindObjectOfType<TimerController>(); // ค้นหา TimerController
        launcher = FindObjectOfType<Launcher>(); // ค้นหา Launcher

        state = TurnState.Player1_Turn; // ตั้งค่าเริ่มต้นเป็นเทิร์นของ Player1
        Player1_Turn(); // เรียกฟังก์ชันสำหรับเทิร์น Player1
    }

    public void Player1_Turn() // ฟังก์ชันสำหรับเทิร์นของ Player1
    {
        if (state != TurnState.Player1_Turn) // ถ้าสถานะไม่ใช่ Player1_Turn
            return; // ออกจากฟังก์ชัน

        timerController.StartTimer(0); // เริ่มจับเวลาสำหรับ Player1
        launcher.EnablePlayer1Launch(); // เปิดใช้งานการโยนของ Player1
    }


    public void Player2_Turn() // ฟังก์ชันสำหรับเทิร์นของ Player2
    {
        if (state != TurnState.Player2_Turn) // ถ้าสถานะไม่ใช่ Player2_Turn
            return; // ออกจากฟังก์ชัน

        timerController.StartTimer(1); // เริ่มจับเวลาสำหรับ Player2
        launcher.EnablePlayer2Launch(); // เปิดใช้งานการโยนของ Player2
    }

}
