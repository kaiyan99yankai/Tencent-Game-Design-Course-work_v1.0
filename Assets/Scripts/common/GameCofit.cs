using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCofit
{
    private static GameCofit _instance = null;

    public static GameCofit GetInstance()
    {
        if (GameCofit._instance == null)
        {
            GameCofit._instance = new GameCofit();
        }
        return GameCofit._instance;
    }

    private GameLevel level = GameLevel.level_1;

    public void SetGameLevel(GameLevel _lvl)
    {
        level = _lvl;
    }

    public GameLevel GetGameLevel()
    {
        return level;
    }
}