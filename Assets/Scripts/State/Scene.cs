using UnityEngine;


public class Scene : MonoBehaviour
{
    public enum SceneName
    {
        MainMenu,
        Cutscene01,
        IdleScene,
        Quest01,
        Quest02,
        Cutscene02,
        Quest03,
        Victory
    }

    public static string GetSceneName(SceneName scene)
    {
        return scene.ToString();
    }
}
