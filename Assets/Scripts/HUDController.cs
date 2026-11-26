using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class HUDController : MonoBehaviour
{
    private Label heartsLabel;
    private Label timerLabel;
    private Label starsVisualLabel;

    private Label item1Label;
    private Label item2Label;
    private Label item3Label;

    private float timeRemaining = 60f;
    private int lives = 3;
    private int stars = 3;

    private bool item1Collected = false;
    private bool item2Collected = false;
    private bool item3Collected = false;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        heartsLabel = root.Q<Label>("HeartsLabel");
        timerLabel = root.Q<Label>("TimerLabel");
        starsVisualLabel = root.Q<Label>("StarsVisualLabel");

        item1Label = root.Q<Label>("Item1Label");
        item2Label = root.Q<Label>("Item2Label");
        item3Label = root.Q<Label>("Item3Label");

        UpdateHUD();
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
                timeRemaining = 0;
        }

        // Testing keys using the NEW Input System

        if (Keyboard.current != null)
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
                item1Collected = true;

            if (Keyboard.current.digit2Key.wasPressedThisFrame)
                item2Collected = true;

            if (Keyboard.current.digit3Key.wasPressedThisFrame)
                item3Collected = true;

            if (Keyboard.current.hKey.wasPressedThisFrame)
            {
                lives--;
                if (lives < 0)
                    lives = 0;
            }
        }

        CalculateStars();
        UpdateHUD();
    }

    void UpdateHUD()
    {
        heartsLabel.text = GetHeartDisplay(lives);
        timerLabel.text = "Time: " + Mathf.CeilToInt(timeRemaining);
        starsVisualLabel.text = GetStarDisplay(stars);

        item1Label.text = item1Collected ? "☑ Milk" : "☐ Milk";
        item2Label.text = item2Collected ? "☑ Bread" : "☐ Bread";
        item3Label.text = item3Collected ? "☑ Apple" : "☐ Apple";
    }

    void CalculateStars()
    {
        if (timeRemaining >= 40)
            stars = 3;
        else if (timeRemaining >= 20)
            stars = 2;
        else if (timeRemaining >= 5)
            stars = 1;
        else
            stars = 0;
    }

    string GetHeartDisplay(int currentLives)
    {
        string result = "";

        for (int i = 0; i < 3; i++)
        {
            if (i < currentLives)
                result += "♥ ";
            else
                result += "♡ ";
        }

        return result.TrimEnd();
    }

    string GetStarDisplay(int currentStars)
    {
        string result = "";

        for (int i = 0; i < 3; i++)
        {
            if (i < currentStars)
                result += "★ ";
            else
                result += "☆ ";
        }

        return result.TrimEnd();
    }
}