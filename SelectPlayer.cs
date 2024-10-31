using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ต้องเพิ่มสำหรับการเปลี่ยนซีน

public class SelectPlayer : MonoBehaviour
{
    [Header("Chef")]
    public GameObject[] skins_player1; // อาร์เรย์ของตัวละครที่สามารถเลือกได้สำหรับผู้เล่น 1
    public GameObject[] skins_player2; // อาร์เรย์ของตัวละครที่สามารถเลือกได้สำหรับผู้เล่น 2
 
    private int selectedCharacter_Player1; // ตัวแปรเก็บค่าตัวละครที่เลือกสำหรับผู้เล่น 1
    private int selectedCharacter_Player2; // ตัวแปรเก็บค่าตัวละครที่เลือกสำหรับผู้เล่น 2

    private AudioManager audioManager;

    private void Awake()
    {
        // ค้นหา AudioManager ด้วยแท็ก "Audio"
        audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
        
        // โหลดค่าตัวละครที่เลือกจาก PlayerPrefs
        selectedCharacter_Player1 = PlayerPrefs.GetInt("SelectedCharacter_Player1", 0);
        selectedCharacter_Player2 = PlayerPrefs.GetInt("SelectedCharacter_Player2", 0);

        // ตรวจสอบขอบเขตของ selectedCharacter
        if (selectedCharacter_Player1 < 0 || selectedCharacter_Player1 >= skins_player1.Length)
        {
            selectedCharacter_Player1 = 0; // หากไม่อยู่ในขอบเขต ให้ตั้งค่าเป็น 0
        }

        if (selectedCharacter_Player2 < 0 || selectedCharacter_Player2 >= skins_player2.Length)
        {
            selectedCharacter_Player2 = 0; // หากไม่อยู่ในขอบเขต ให้ตั้งค่าเป็น 0
        }

        // ปิดการแสดงผลตัวละครทั้งหมดและแสดงตัวละครที่เลือก
        UpdatePlayerSkins();
    }

    public void ChangeNextPlayer1()
    {
        // เปลี่ยนไปตัวละครถัดไปสำหรับผู้เล่น 1
        skins_player1[selectedCharacter_Player1].SetActive(false);
        selectedCharacter_Player1++;
        if (selectedCharacter_Player1 >= skins_player1.Length)
        {
            selectedCharacter_Player1 = 0; // กลับไปที่ตัวละครแรกหากถึงตัวละครสุดท้าย
        }
        UpdatePlayerSkins();
    }

    public void ChangeBackPlayer1()
    {
        // เปลี่ยนไปตัวละครก่อนหน้า สำหรับผู้เล่น 1
        skins_player1[selectedCharacter_Player1].SetActive(false);
        selectedCharacter_Player1--;
        if (selectedCharacter_Player1 < 0)
        {
            selectedCharacter_Player1 = skins_player1.Length - 1; // กลับไปที่ตัวละครสุดท้ายหากอยู่ที่ตัวละครแรก
        }
        UpdatePlayerSkins();
    }

    public void ChangeNextPlayer2()
    {
        // เปลี่ยนไปตัวละครถัดไปสำหรับผู้เล่น 2
        skins_player2[selectedCharacter_Player2].SetActive(false);
        selectedCharacter_Player2++;
        if (selectedCharacter_Player2 >= skins_player2.Length)
        {
            selectedCharacter_Player2 = 0; // กลับไปที่ตัวละครแรกหากถึงตัวละครสุดท้าย
        }
        UpdatePlayerSkins();
    }

    public void ChangeBackPlayer2()
    {
        // เปลี่ยนไปตัวละครก่อนหน้า สำหรับผู้เล่น 2
        skins_player2[selectedCharacter_Player2].SetActive(false);
        selectedCharacter_Player2--;
        if (selectedCharacter_Player2 < 0)
        {
            selectedCharacter_Player2 = skins_player2.Length - 1; // กลับไปที่ตัวละครสุดท้ายหากอยู่ที่ตัวละครแรก
        }
        UpdatePlayerSkins();
    }

    public void ConfirmSelection()
    {
        // บันทึกค่าตัวละครที่เลือกลง PlayerPrefs
        PlayerPrefs.SetInt("SelectedCharacter_Player1", selectedCharacter_Player1);
        PlayerPrefs.SetInt("SelectedCharacter_Player2", selectedCharacter_Player2);
        PlayerPrefs.Save();

        // เปลี่ยนซีนไปยังหน้าเกม
        SceneManager.LoadScene("GameChefSmash"); // เปลี่ยนชื่อ "GameChefSmash" เป็นชื่อซีนที่ต้องการโหลด
    }

    private void UpdatePlayerSkins()
    {
        // ปิดการแสดงผลตัวละครทั้งหมดสำหรับผู้เล่น 1
        foreach (GameObject player in skins_player1)
        {
            player.SetActive(false);
        }
        skins_player1[selectedCharacter_Player1].SetActive(true); // แสดงตัวละครที่เลือกสำหรับผู้เล่น 1

        // ปิดการแสดงผลตัวละครทั้งหมดสำหรับผู้เล่น 2
        foreach (GameObject player in skins_player2)
        {
            player.SetActive(false);
        }
        skins_player2[selectedCharacter_Player2].SetActive(true); // แสดงตัวละครที่เลือกสำหรับผู้เล่น 2
    }
}
