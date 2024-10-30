using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerController : MonoBehaviour
{
    public Image[] power_image; // timer_linear_image[0] สำหรับ Player1, timer_linear_image[1] สำหรับ Player2
    public GameObject[] Power_Holder;
    private int currentPlayerIndex; // ตัวแปรสำหรับเก็บเลข index ของผู้เล่น (0 สำหรับ Player1, 1 สำหรับ Player2)
     private TimerController timerController; // อ้างอิง TimerController

    // เริ่มจับเวลาสำหรับผู้เล่น
    public void StartTimer(float maxTime, int playerIndex, System.Action onTimeEndCallback)
    {
        this.currentPlayerIndex = playerIndex;

        // แสดง Timer ของผู้เล่นที่ระบุ และซ่อน Timer ของอีกผู้เล่น
        Timer_Holder[playerIndex].SetActive(true);
        Timer_Holder[1 - playerIndex].SetActive(false); 
    }

    public void StopTimer()
    {
        // ซ่อน Timer ของทั้งสองผู้เล่นและรีเซ็ตเวลา
        Timer_Holder[playerIndex].SetActive(false);
        Timer_Holder[1-playerIndex].SetActive(false);
        time_remaining = 0;
    }

    void Update()
    {
        if (time_remaining > 0)
        {
            time_remaining -= Time.deltaTime;

            // อัปเดต timer ของผู้เล่นปัจจุบัน
            timer_linear_image[currentPlayerIndex].fillAmount = time_remaining / max_time;
        }
        else if (time_remaining <= 0 && onTimeEnd != null)
        {
            onTimeEnd.Invoke(); // เรียกฟังก์ชันเมื่อหมดเวลา
            onTimeEnd = null; // ล้างการเรียกใช้งานเพื่อไม่ให้เรียกซ้ำ
        }
    }
}
