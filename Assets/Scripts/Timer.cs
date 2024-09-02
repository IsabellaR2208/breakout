using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Breakout
{
    public class Timer : MonoBehaviour
    {
       [SerializeField]  private TextMeshProUGUI timerText;  // Assign a UI Text component in the Inspector to display the timer
        [SerializeField]  float countdownTime = 60f;  // Set the countdown time in seconds
        private float timeRemaining;
        private bool timerRunning = false;

        public delegate void TimerEnded();
        public event TimerEnded OnTimerEnd;  // Event triggered when the timer ends

        void Update()
        {
            if (timerRunning)
            {
                // Update the time remaining
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    UpdateTimerDisplay();
                }
                else
                {
                    // Time has run out
                    timeRemaining = 0;
                    timerRunning = false;
                    UpdateTimerDisplay();

                    // Trigger the timer end event
                    if (OnTimerEnd != null)
                    {
                        OnTimerEnd?.Invoke();
                    }
                }
            }
        }

        public void StartTimer()
        {
            timeRemaining = countdownTime;
            timerRunning = true;
            UpdateTimerDisplay();
        }

        public void StopTimer()
        {
            timerRunning = false;
        }

        public void ResetTimer()
        {
            timeRemaining = countdownTime;
            UpdateTimerDisplay();
        }

        private void UpdateTimerDisplay()
        {
            // Format the time into minutes and seconds
            int minutes = Mathf.FloorToInt(timeRemaining / 60F);
            int seconds = Mathf.FloorToInt(timeRemaining % 60F);
            int milliseconds = Mathf.FloorToInt((timeRemaining * 100F) % 100F);

            // Update the timerText UI element
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
    }
}
