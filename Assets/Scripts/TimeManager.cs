using UnityEngine;
using System.Collections;

public static class TimeManager
{
    private static float CurrentTime;
    private static bool paused = false;

    public static float DeltaTime { get; private set; } = 0.0f;
    public static float TimeModifier { get; set; } = 1.0f;
    public static bool IsPaused() {
        return paused;
    }
    public static void SetPaused(bool p) {
        paused = p;
    }
    public static float GetTime() {
        return CurrentTime;
    }

    public static void Update() {
        DeltaTime = 0;
        if (!paused)
            DeltaTime = Time.deltaTime * TimeModifier;
        CurrentTime += DeltaTime;
    }
}
