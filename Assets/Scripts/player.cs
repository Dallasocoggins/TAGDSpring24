using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class player : MonoBehaviour
{
    public float speed = 500f;
    public float speedTimeLeft = 0f;
    public float speedBoostTime = 1.5f; // Increase this value to boost the speed more.
    public KeyCode[] keys = {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F,
        KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R,
        KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X,
        KeyCode.Y, KeyCode.Z,
        KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
        KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7,
        KeyCode.Alpha8, KeyCode.Alpha9,
        KeyCode.Space, KeyCode.Return, KeyCode.Tab,
        KeyCode.LeftShift, KeyCode.RightShift, KeyCode.LeftControl,
        KeyCode.RightControl, KeyCode.LeftAlt, KeyCode.RightAlt,
        KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.F1, KeyCode.F2, KeyCode.F3, KeyCode.F4, KeyCode.F5, KeyCode.F6,
        KeyCode.F7, KeyCode.F8, KeyCode.F9, KeyCode.F10, KeyCode.F11, KeyCode.F12
    }; // List of keys to trigger the speed boost.
    public Rigidbody2D rb;
    public KeyCode currentKey;
    public GameManager gm;

    public TextMeshProUGUI promptText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI heightText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetRandomKey();

        gm = GameManager.instance;

        promptText = GameObject.FindGameObjectWithTag("Prompt").GetComponent<TextMeshProUGUI>();
        pointsText = GameObject.FindGameObjectWithTag("Points").GetComponent<TextMeshProUGUI>();
        heightText = GameObject.FindGameObjectWithTag("Height").GetComponent<TextMeshProUGUI>();
        gm.UpdatePoints(this);
        UpdatePointsText(gm.points);
    }

    void Update()
    {

        if (Input.GetKeyDown(currentKey))
        {
            // Apply speed boost to the rigidbody.
            speedTimeLeft += speedBoostTime;
            SetRandomKey();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gm.PauseMenuToggle();
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            gm.ClearPoints();
            UpdatePointsText(gm.points);
        }
    }

    void FixedUpdate()
    {
        if (speedTimeLeft > 0f)
        {
            Vector3 vel = rb.velocity;
            vel.x = speed;
            rb.velocity = vel;
            speedTimeLeft -= Time.fixedDeltaTime;
        }

        gm.UpdatePoints(this);
        UpdatePointsText(gm.points);
    }

    void SetRandomKey()
    {
        int randomIndex = Random.Range(0, keys.Length);
        currentKey = keys[randomIndex];
        promptText.text = currentKey.ToString();
        Debug.Log(currentKey);
    }

    public void UpdatePointsText(int points)
    {
        pointsText.text = "Points: " + points.ToString();
    }

    public void UpdateHeightText(float height)
    {
        heightText.text = "Max Height: " + height.ToString();
    }
}
