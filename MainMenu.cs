using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuController : MonoBehaviour
{
    private AudioManager audioManager;

    private void Awake() {
    audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
}

    public void PlayGame()
{
    // ใช้ชื่อที่ถูกต้องตามที่ประกาศใน AudioManager
    // audioManager.PlaySFX(audioManager.take_hit_defening);
    SceneManager.LoadScene("Select_ChefSmash"); 
}
    

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
