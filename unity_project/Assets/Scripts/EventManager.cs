using UnityEngine;
using System;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;

public class EventManager : MonoBehaviour
{
    private DatabaseReference dbRef;
    private string userId;
    private bool isInitialized = false;

    void Start()
    {
        StartCoroutine(WaitForFirebase());
    }

    IEnumerator WaitForFirebase()
    {
        // Wait for MainEngine to initialize Firebase
        while (FirebaseAuth.DefaultInstance == null || FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            yield return new WaitForSeconds(1f);
        }

        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        isInitialized = true;
        
        Debug.Log("EventManager Initialized with User: " + userId);
        CheckDateEvents();
    }

    void CheckDateEvents()
    {
        if (!isInitialized) return;

        DateTime today = DateTime.Now;

        // الخميس
        if (today.DayOfWeek == DayOfWeek.Thursday)
        {
            AccessibilityAudio.Instance.Speak("اليوم الخميس! حصلت على مكافأة أسبوعية.");
        }

        // 20 مارس 2026
        if (today.Year == 2026 && today.Month == 3 && today.Day == 20)
        {
            AddPowerToPlayer(5000);
        }
    }

    void AddPowerToPlayer(int amount)
    {
        if (!isInitialized) return;

        DatabaseReference powerRef = dbRef.Child("players").Child(userId).Child("power");

        powerRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                int currentPower = 0;

                if (task.Result.Exists)
                {
                    currentPower = Convert.ToInt32(task.Result.Value);
                }

                int newPower = currentPower + amount;

                powerRef.SetValueAsync(newPower).ContinueWith(updateTask => {
                    if (updateTask.IsCompleted)
                    {
                        Debug.Log("Power updated to: " + newPower);
                        AccessibilityAudio.Instance.Speak("تم منحك حزمة العيد وقوة خمسة آلاف.");
                    }
                });
            }
            else
            {
                Debug.LogError("Error fetching player power: " + task.Exception);
            }
        });
    }
}
