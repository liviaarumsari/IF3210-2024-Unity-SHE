using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConfig
{
    public static Dictionary<GameState.Stage, Scene.SceneName> StageToSceneName = new Dictionary<GameState.Stage, Scene.SceneName>
    {
        { GameState.Stage.Cutscene01, Scene.SceneName.Cutscene01 },
        { GameState.Stage.Idle01, Scene.SceneName.IdleScene },
        { GameState.Stage.Quest01, Scene.SceneName.Quest01 },
        { GameState.Stage.Cutscene02, Scene.SceneName.Cutscene02 },
        { GameState.Stage.Idle02, Scene.SceneName.IdleScene },
        { GameState.Stage.Quest02, Scene.SceneName.Quest02 },
        { GameState.Stage.Cutscene03, Scene.SceneName.Cutscene03 },
        { GameState.Stage.Idle03, Scene.SceneName.IdleScene },
        { GameState.Stage.Quest03, Scene.SceneName.Quest03 },
        { GameState.Stage.Cutscene04, Scene.SceneName.Cutscene04 },
        { GameState.Stage.Idle04, Scene.SceneName.IdleScene },
        { GameState.Stage.Quest04, Scene.SceneName.Quest04 },
        { GameState.Stage.Victory, Scene.SceneName.Victory },
        { GameState.Stage.GameOver, Scene.SceneName.GameOver }
    };
}
