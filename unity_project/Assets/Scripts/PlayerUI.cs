using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    public Text powerText;
    private DatabaseReference dbRef;
    private string userId;

    void Start()
    {
        StartCoroutine(SetupUI());
    }

    IEnumerator SetupUI()
    {
        while (FirebaseAuth.DefaultInstance == null || FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            yield return new WaitForSeconds(1f);
        }

        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        // Listen for power changes
        dbRef.Child("players").Child(userId).Child("power").ValueChanged += HandlePowerChanged;
    }

    void HandlePowerChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        if (args.Snapshot != null && args.Snapshot.Exists)
        {
            long power = (long)args.Snapshot.Value;
            UpdatePowerUI(power);
        }
    }

    void UpdatePowerUI(long power)
    {
        if (powerText != null)
        {
            powerText.text = "القوة: " + power;
        }
        else
        {
            Debug.Log("Power UI Updated: " + power);
        }
    }

    void OnDestroy()
    {
        if (dbRef != null && userId != null)
        {
            dbRef.Child("players").Child(userId).Child("power").ValueChanged -= HandlePowerChanged;
        }
    }
}
