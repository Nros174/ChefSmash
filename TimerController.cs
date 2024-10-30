using System.Collections; // นำเข้า namespace สำหรับการใช้ Collections
using System.Collections.Generic; // นำเข้า namespace สำหรับการใช้ Collections แบบ Generic
using UnityEngine; // นำเข้า namespace สำหรับ UnityEngine
using UnityEngine.UI; // นำเข้า namespace สำหรับ UI

public class TimerController : MonoBehaviour // สร้างคลาส TimerController ที่สืบทอดมาจาก MonoBehaviour
{
    public Image[] timer_linear_image; // timer_linear_image[0] สำหรับ Player1, timer_linear_image[1] สำหรับ Player2
    public GameObject[] Timer_Holder; // ตัวแปรสำหรับเก็บ GameObjects ที่ใช้แสดง Timer
    public float max_time = 5.0f; // กำหนดค่า max_time ที่นี่
    private int currentPlayerIndex; // ตัวแปรสำหรับเก็บเลข index ของผู้เล่น (0 สำหรับ Player1, 1 สำหรับ Player2)
    private GameStateController state; // เปลี่ยนชื่อเป็น state เพื่อใช้ใน ChangeTurn()

    void Start() // ฟังก์ชันเริ่มต้น
    {
        state = FindObjectOfType<GameStateController>(); // ค้นหา GameStateController
    }

    // เริ่มจับเวลาสำหรับผู้เล่น
    public void StartTimer(int playerIndex) // ฟังก์ชันเริ่มจับเวลา
    {
        currentPlayerIndex = playerIndex; // ตั้งค่า index ของผู้เล่น

        // แสดง Timer ของผู้เล่นที่ระบุ และซ่อน Timer ของอีกผู้เล่น
        Timer_Holder[0].SetActive(playerIndex == 0); // แสดง/ซ่อน Timer ของ Player1
        Timer_Holder[1].SetActive(playerIndex == 1); // แสดง/ซ่อน Timer ของ Player2

        StartCoroutine(TimerCoroutine()); // เรียกใช้ Coroutine สำหรับจับเวลา
    }

    private IEnumerator TimerCoroutine() // Coroutine สำหรับจับเวลา
    {
        float time_remaining = max_time; // ตั้งเวลาเริ่มต้น

        while (time_remaining > 0) // ขณะที่เวลายังเหลืออยู่
        {
            time_remaining -= Time.deltaTime; // ลดเวลาตามเวลาที่ผ่านมา
            timer_linear_image[currentPlayerIndex].fillAmount = time_remaining / max_time; // อัปเดต UI ของ Timer
            yield return null; // รอ frame ถัดไป
        }

        // เมื่อหมดเวลา เปลี่ยนเทิร์น
        ChangeTurn();
    }

    private void ChangeTurn() // ฟังก์ชันสำหรับเปลี่ยนเทิร์น
    {
        Debug.Log("Time's up! Change turn."); // แสดงข้อความเมื่อเวลาหมด

        // เปลี่ยนเทิร์นตาม index ของผู้เล่น
        if (currentPlayerIndex == 0)
        {
            state.state = TurnState.Player2_Turn; // เปลี่ยนสถานะเป็นเทิร์นของ Player2
            state.Player2_Turn(); // เรียกฟังก์ชันสำหรับเทิร์น Player2
        }
        else if (currentPlayerIndex == 1)
        {
            state.state = TurnState.Player1_Turn; // เปลี่ยนสถานะเป็นเทิร์นของ Player1
            state.Player1_Turn(); // เรียกฟังก์ชันสำหรับเทิร์น Player1
        }
    }

    public void StopTimer() // ฟังก์ชันหยุดนาฬิกา
    {
        // ซ่อน Timer ของทั้งสองผู้เล่น
        Timer_Holder[0].SetActive(false); // ซ่อน Timer ของ Player1
        Timer_Holder[1].SetActive(false); // ซ่อน Timer ของ Player2
    }
}
