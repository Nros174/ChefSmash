using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { Player1_Turn, Player2_Turn }

public class GameStateController : MonoBehaviour
{
    private CustomerManager customerManager; // อ้างอิง CustomerManager เพื่อเข้าถึง health_player1 และ health_player2
    private TimerController timerController; // อ้างอิง TimerController
    private Launcher launcher; // อ้างอิง Launcher

    public TurnState state;

    void Start()
    {
        // ค้นหา CustomerManager, TimerController และ Launcher ใน Scene
        customerManager = FindObjectOfType<CustomerManager>();
        timerController = FindObjectOfType<TimerController>();
        launcher = FindObjectOfType<Launcher>();

        state = TurnState.Player1_Turn;
        Player1_Turn();
    }

    void Player1_Turn()
    {
        if (state != TurnState.Player1_Turn)
            return;

        timerController.StartTimer( 0, OnPlayer1TurnEnd); // ใช้ max_time จาก TimerController
        launcher.EnablePlayer1Launch(OnWeaponThrownByPlayer1);
    }

    private void OnWeaponThrownByPlayer1()
    {
        timerController.StopTimer(); // หยุดนาฬิกา
        state = TurnState.Player2_Turn; // เปลี่ยนสถานะเป็นเทิร์นของ Player2
        Player2_Turn(); // เรียกฟังก์ชันสำหรับเทิร์น Player2
    }

    private void OnPlayer1TurnEnd()
    {
        state = TurnState.Player2_Turn; // เปลี่ยนสถานะเป็นเทิร์นของ Player2
        Player2_Turn(); // เรียกฟังก์ชันสำหรับเทิร์น Player2
    }

    void Player2_Turn()
    {
        if (state != TurnState.Player2_Turn)
            return;

        timerController.StartTimer( 1, OnPlayer2TurnEnd); // ใช้ max_time จาก TimerController
        launcher.EnablePlayer2Launch(OnWeaponThrownByPlayer2);
    }

    private void OnWeaponThrownByPlayer2()
    {
        timerController.StopTimer(); // หยุดนาฬิกา
        state = TurnState.Player1_Turn; // เปลี่ยนสถานะเป็นเทิร์นของ Player1
        Player1_Turn(); // เรียกฟังก์ชันสำหรับเทิร์น Player1
    }

    private void OnPlayer2TurnEnd()
    {
        state = TurnState.Player1_Turn; // เปลี่ยนสถานะเป็นเทิร์นของ Player1
        Player1_Turn(); // เรียกฟังก์ชันสำหรับเทิร์น Player1
    }
}
