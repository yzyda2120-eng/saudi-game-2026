using UnityEngine;
using System.Collections;

public class AccessibilityAudio : MonoBehaviour
{
    public static AccessibilityAudio Instance;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = gameObject.AddComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Speak(string text)
    {
        Debug.Log("TTS (Text-to-Speech): " + text);
        
        // في يونيتي للأندرويد، نستخدم Text-to-Speech المدمج عبر AndroidJavaClass
        #if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass tts = new AndroidJavaClass("android.speech.tts.TextToSpeech"))
        {
            // هذا مجرد نموذج مبسط، في الواقع يتطلب الأمر إعداداً كاملاً لـ TTS
            // سنقوم بإرسال رسالة إلى النظام لطباعة النص كـ Toast كبديل سريع في هذا النموذج
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                    AndroidJavaObject toast = new AndroidJavaClass("android.widget.Toast")
                        .CallStatic<AndroidJavaObject>("makeText", currentActivity, text, 0);
                    toast.Call("show");
                }));
            }
        }
        #endif
        
        // يمكننا أيضاً تشغيل صوت بسيط كإشارة
        if (audioSource != null)
        {
            // audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Notification"));
        }
    }
}
