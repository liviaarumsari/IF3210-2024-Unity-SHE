using UnityEngine;


public class Scene : MonoBehaviour
{
    public enum SceneName
    {
        MainMenu,
        IdleScene,
        Cutscene01,
        Cutscene02,
        Cutscene03,
        Cutscene04,
        Quest01,
        Quest02,
        Quest03,
        Quest04,
        Victory,
        GameOver
    }

    public static string GetSceneName(SceneName scene)
    {
        return scene.ToString();
    }
}
