using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MovementScript : MonoBehaviour
{

    public AudioClip JumpClip, DeathClip, ScoreClip, BGMClip;
    public GameObject IntroUI, DeathUI, ScoreUI;
    [Header("Bird Settings")]
    public float Jump = 1500f;
    public float FallSpeed = 200f;
    public float BGMVolume = 1f;
    public float SFXVolume = 0.5f;
    private Rigidbody2D rb;
    private AudioSource bgmSource;
    private AudioSource sfxSource;
    private ScoreManager scoreManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip = BGMClip;
        bgmSource.loop = true;
        bgmSource.Play();


        scoreManager = FindObjectOfType<ScoreManager>();

        GameStateManager.GameState = GameState.Intro;
        Time.timeScale = 0;
    }

    void Update()
    {
        if (GameStateManager.GameState == GameState.Intro)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
            {
                GameStateManager.GameState = GameState.Playing;
                IntroUI.SetActive(false);
                ScoreUI.SetActive(true);
                Time.timeScale = 1;
                rb.gravityScale = FallSpeed;
            }
        }
        else if (GameStateManager.GameState == GameState.Playing)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                FlappyJump();
            }
        }
    }

    void FlappyJump()
    {
        rb.velocity = new Vector2(0, Jump);
        sfxSource.PlayOneShot(JumpClip);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            if (coll.gameObject.CompareTag ("obstacle"))
            {
                Time.timeScale = 0;
                BirdDead();
            }
            else if (coll.gameObject.CompareTag ("score"))
            {
                sfxSource.PlayOneShot(ScoreClip);
                if (scoreManager != null)
                {
                    scoreManager.AddScore(1);
                }
            }
        }
    }

    void BirdDead()
    {
        GameStateManager.GameState = GameState.Dead;
        sfxSource.PlayOneShot(DeathClip);
        DeathUI.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
