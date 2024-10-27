using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayerManager : MonoBehaviour
{
    public GameObject[] playerPrefabs_player1; // อาร์เรย์ของตัวละครที่สามารถเลือกได้สำหรับผู้เล่น 1
    public GameObject[] playerPrefabs_player2; // อาร์เรย์ของตัวละครที่สามารถเลือกได้สำหรับผู้เล่น 2   

    private int characterIndex_Player1; // ตัวแปรเก็บค่าตัวละครที่เลือกสำหรับผู้เล่น 1
    private int characterIndex_Player2; // ตัวแปรเก็บค่าตัวละครที่เลือกสำหรับผู้เล่น 2

    // กำหนดตำแหน่งแยกเป็นค่า X, Y, Z
    public float positionX_Player1 = -2f; // ตำแหน่งของผู้เล่น 1
    public float positionX_Player2 = 2f; // ตำแหน่งของผู้เล่น 2
    public float positionY = 0f;
    public float positionZ = 0f;

    private Vector3 spawnPosition_Player1; // ตัวแปรเก็บตำแหน่งเกิดสำหรับผู้เล่น 1
    private Vector3 spawnPosition_Player2; // ตัวแปรเก็บตำแหน่งเกิดสำหรับผู้เล่น 2

    private void Awake()
    {
        // โหลดค่าตัวละครที่เลือกจาก PlayerPrefs
        characterIndex_Player1 = PlayerPrefs.GetInt("SelectedCharacter_Player1", 0);
        characterIndex_Player2 = PlayerPrefs.GetInt("SelectedCharacter_Player2", 0);

        // กำหนดตำแหน่งของ spawnPosition ใน Awake
        spawnPosition_Player1 = new Vector3(positionX_Player1, positionY, positionZ);
        spawnPosition_Player2 = new Vector3(positionX_Player2, positionY, positionZ);

        // สร้างวัตถุในตำแหน่งที่กำหนดใน spawnPosition
        if (characterIndex_Player1 >= 0 && characterIndex_Player1 < playerPrefabs_player1.Length)
        {
            Instantiate(playerPrefabs_player1[characterIndex_Player1], spawnPosition_Player1, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Invalid character index for Player 1.");
        }

        if (characterIndex_Player2 >= 0 && characterIndex_Player2 < playerPrefabs_player2.Length)
        {
            Instantiate(playerPrefabs_player2[characterIndex_Player2], spawnPosition_Player2, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Invalid character index for Player 2.");
        }
    }
}
