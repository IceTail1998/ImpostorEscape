using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LevelMap 
{
    void OnStartLevel();
    void Setup();
    void TurnOffLevel();
    void TurnOnLevel();
    void OnFinishLevel();
}
