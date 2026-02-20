using UnityEngine;
using System;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections.Generic;

public class MainEngine : MonoBehaviour
{
    public static MainEngine Instance;

    private FirebaseAuth auth;
    private DatabaseReference dbRef;

    private GestureManager gestureManager;
    private EventManager eventManager;
    private AccessibilityAudio audioSystem;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeFirebase();
        InitializeSystems();
    }

    void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                dbRef = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Firebase Ready");
                
                // Automatically login for testing
                LoginAnonymously();
            }
            else
            {
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    void InitializeSystems()
    {
        gestureManager = gameObject.AddComponent<GestureManager>();
        eventManager = gameObject.AddComponent<EventManager>();
        audioSystem = gameObject.AddComponent<AccessibilityAudio>();
    }

    public void LoginAnonymously()
    {
        if (auth == null) return;
        
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("Player Logged In: " + task.Result.User.UserId);
            }
            else
            {
                Debug.LogError("Login failed: " + task.Exception);
            }
        });
    }

    public DatabaseReference GetDatabaseReference()
    {
        return dbRef;
    }
}
