using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private CustomerManager customerManager; // อ้างอิง CustomerManager
    private TimerController timerController; // อ้างอิง TimerController
    private Launcher launcher; // อ้างอิง Launcher

    private GameStateController gameStateController;
    private PlayerCollision playerCollision;

    private FanRotation fan;

    public bool Graceskill = false;
    public bool ChargeSkill = false;
    public bool MonsherSkill = false;
    public bool WindSkill = false;
    public bool ShieldSkill = false;


    private int characterIndex_Player1; // ตัวแปรสำหรับเก็บ index ของตัวละคร Player1
    private int characterIndex_Player2; // ตัวแปรสำหรับเก็บ index ของตัวละคร Player2

    [Header("Player1")]
    public GameObject[] skill_player1; // อาร์เรย์ของตัวละครที่สามารถเลือกได้สำหรับผู้เล่น 1
    public GameObject shile_player1;

    [Header("Player2")]
    public GameObject[] skill_player2; // อาร์เรย์ของตัวละครที่สามารถเลือกได้สำหรับผู้เล่น 2  
    public GameObject shile_player2;

    void Awake()
    {
        characterIndex_Player1 = PlayerPrefs.GetInt("SelectedCharacter_Player1", 0); // ดึงค่า index ตัวละคร Player1
        characterIndex_Player2 = PlayerPrefs.GetInt("SelectedCharacter_Player2", 0); // ดึงค่า index ตัวละคร Player2
        customerManager = FindObjectOfType<CustomerManager>(); // ค้นหา CustomerManager
        timerController = FindObjectOfType<TimerController>(); // ค้นหา TimerController
        launcher = FindObjectOfType<Launcher>(); // ค้นหา Launcher
        playerCollision = FindObjectOfType<PlayerCollision>();
        gameStateController = FindObjectOfType<GameStateController>();
        fan = FindObjectOfType<FanRotation>();
        skill_player1[characterIndex_Player1].gameObject.SetActive(true);
        skill_player2[characterIndex_Player2].gameObject.SetActive(true);
    }




    // F 
    public void Roar()
    {
        Debug.Log("Roar");
        // ลบเลือด1เพิ่ม2
        if (gameStateController.state == TurnState.Player1_Turn)
        {
            customerManager.health_player1--; // ลดค่าก่อน
            customerManager.health_player1 = Mathf.Min(customerManager.health_player1 + 2, 5); // เพิ่มและจำกัดไม่ให้เกิน 5
        }
        else if (gameStateController.state == TurnState.Player2_Turn)
        {
            customerManager.health_player2--; // ลดค่าก่อน
            customerManager.health_player2 = Mathf.Min(customerManager.health_player2 + 2, 5); // เพิ่มและจำกัดไม่ให้เกิน 5
        }

    }

    public void Monsher()
    {
        //ทำให้โจมตีไม่ได้
        Debug.Log("Monsher");

        MonsherSkill = true;

    }

    // J แทงค์
    public void Grace()
    {
        //ใหญ่ขึ้น 
        Debug.Log("Grace");
        Graceskill = true;
    }

    public void Wind()
    {

        Debug.Log("Wind");
        WindSkill = true;
        fan.StopRotation();

    }

    // T 
    public void trumpet()

    {

        Debug.Log("trumpet");
        // ตกใจ -1
        if (gameStateController.state == TurnState.Player1_Turn)
        {
            customerManager.health_player2--; // เข้าถึง health จาก CustomerManager
        }
        else if (gameStateController.state == TurnState.Player2_Turn)
        {
            customerManager.health_player1--; // เข้าถึง health จาก CustomerManager
        }
    }

    public void Stomp()
    {
        Debug.Log("Stomp");
        //ตีแรง-2
        ChargeSkill = true;

    }

    // C 
    public void Shield() ///////////////////////////////////
    {
        ShieldSkill = true;
        Debug.Log("Shield");
        // โจมตีไม่เข้า, รักษา 1 // 0 +1

        if (gameStateController.state == TurnState.Player1_Turn)
        {
            shile_player1.SetActive(true);
            customerManager.health_player1 = Mathf.Min(customerManager.health_player1++); // เข้าถึง health จาก CustomerManager และจำกัดไม่ให้เกิน 5
        }
        else if (gameStateController.state == TurnState.Player2_Turn)
        {
            shile_player2.SetActive(true);
            customerManager.health_player2 = Mathf.Min(customerManager.health_player2++); // เข้าถึง health จาก CustomerManager และจำกัดไม่ให้เกิน 5
        }
    }

    public void Tranquility()
    {
        Debug.Log("Tranquillity");
        // รักษา 2 // +2
        if (gameStateController.state == TurnState.Player1_Turn)
        {
            customerManager.health_player1 = Mathf.Min(customerManager.health_player1 + 2, 5); // เข้าถึง health จาก CustomerManager และจำกัดไม่ให้เกิน 5
        }
        else if (gameStateController.state == TurnState.Player2_Turn)
        {
            customerManager.health_player2 = Mathf.Min(customerManager.health_player2 + 2, 5); // เข้าถึง health จาก CustomerManager และจำกัดไม่ให้เกิน 5
        }

    }

    public void disableChargeSkill()
    {
        ChargeSkill = false;
    }

    public void disableGraceskill()
    {
        Graceskill = false;
    }
    public void disableMonsher()
    {
        MonsherSkill = false;
    }
    public void disableWind()
    {
        WindSkill = false;
    }

    public void disableShield()
    {
        ShieldSkill = true;
        if (gameStateController.state == TurnState.Player1_Turn)
        {
            shile_player2.SetActive(false);
            
        }
        else if (gameStateController.state == TurnState.Player2_Turn)
        {
            shile_player1.SetActive(false);
            
        }

    }
}