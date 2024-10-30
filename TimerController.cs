using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Image[] timer_linear_image; // timer_linear_image[0] สำหรับ Player1, timer_linear_image[1] สำหรับ Player2
    public GameObject[] Timer_Holder;
    public float max_time = 5.0f; // กำหนดค่า max_time ที่นี่
    private float time_remaining = max_time;
    private System.Action onTimeEnd; // ฟังก์ชันที่เรียกใช้เมื่อหมดเวลา
    private int currentPlayerIndex; // ตัวแปรสำหรับเก็บเลข index ของผู้เล่น (0 สำหรับ Player1, 1 สำหรับ Player2)

    // เริ่มจับเวลาสำหรับผู้เล่น
    public void StartTimer(int playerIndex, System.Action onTimeEndCallback)
    {

        this.onTimeEnd = onTimeEndCallback;
        this.currentPlayerIndex = playerIndex;

        // แสดง Timer ของผู้เล่นที่ระบุ และซ่อน Timer ของอีกผู้เล่น
        Timer_Holder[playerIndex].SetActive(true);
        Timer_Holder[1 - playerIndex].SetActive(false);
    }

    public void StopTimer()
    {
        // ซ่อน Timer ของทั้งสองผู้เล่นและรีเซ็ตเวลา
        Timer_Holder[0].SetActive(false);
        Timer_Holder[1].SetActive(false);
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
