using UnityEngine;
using System;
using System.Collections;

public class SaudiCultureEvents : MonoBehaviour
{
    void Start()
    {
        CheckSaudiEvents();
    }

    void CheckSaudiEvents()
    {
        DateTime today = DateTime.Now;

        // اليوم الوطني السعودي - 23 سبتمبر
        if (today.Month == 9 && today.Day == 23)
        {
            AccessibilityAudio.Instance.Speak("كل عام والمملكة بخير! اليوم هو اليوم الوطني السعودي. تم منحك هدية خاصة.");
            // Give reward logic here
        }

        // يوم التأسيس - 22 فبراير
        if (today.Month == 2 && today.Day == 22)
        {
            AccessibilityAudio.Instance.Speak("يوم التأسيس! فخورين بجذورنا. استمتع بمكافأة يوم التأسيس.");
            // Give reward logic here
        }

        // رمضان والعيد (تحتاج حساب هجري، هنا مثال بسيط)
        // يمكن إضافة مكتبة للتقويم الهجري لاحقاً
    }
}
