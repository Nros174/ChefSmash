using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip take_hit_defening;

    private void Start()
    {
        // เรียกใช้การเล่นเพลงพื้นหลังเมื่อเริ่มต้น
        musicSource.clip = background;
        musicSource.Play();
        
        // ลงทะเบียนการเปลี่ยนซีน
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDestroy()
    {
        // ยกเลิกการลงทะเบียนเพื่อป้องกันการเกิด Memory Leak
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        // เปลี่ยนเพลงพื้นหลังตามซีนที่โหลดใหม่
        ChangeBackgroundMusic();
    }

    private void ChangeBackgroundMusic()
    {
        // หยุดเพลงปัจจุบัน
        musicSource.Stop();

        // เล่นเพลงใหม่
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
