using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class DynamicAudio : MonoBehaviour
{

    enum GameplayAudio1 { GAMEPLAY1_1, GAMEPLAY1_2, GAMEPLAY1_3, NONE} 
    enum GameplayAudio2 { GAMEPLAY2_1, GAMEPLAY2_2, GAMEPLAY2_3, GAMEPLAY2_4, NONE } 
    enum GameplayAudio3 { GAMEPLAY3_1, GAMEPLAY3_2, GAMEPLAY3_3, NONE } 
    enum GameplayAudio4 { GAMEPLAY4, NONE }

    GameplayAudio1 audio1;
    GameplayAudio2 audio2;
    GameplayAudio3 audio3;
    GameplayAudio4 audio4;

    void Awake ()
    {
        GameplayAudio1 audio1 = GameplayAudio1.NONE;
        GameplayAudio2 audio2 = GameplayAudio2.NONE;
        GameplayAudio3 audio3 = GameplayAudio3.NONE;
        GameplayAudio4 audio4 = GameplayAudio4.NONE;
    }

    void Update()
    {
        switch (audio1)
        {
            case GameplayAudio1.GAMEPLAY1_1:
                break;
            case GameplayAudio1.GAMEPLAY1_2:
                break;
            case GameplayAudio1.GAMEPLAY1_3:
                break;
            case GameplayAudio1.NONE:
                break;
            default:
                break;
        }

        switch (audio2)
        {
            case GameplayAudio2.GAMEPLAY2_1:
                break;
            case GameplayAudio2.GAMEPLAY2_2:
                break;
            case GameplayAudio2.GAMEPLAY2_3:
                break;
            case GameplayAudio2.GAMEPLAY2_4:
                break;
            case GameplayAudio2.NONE:
                break;
            default:
                break;
        }

        switch (audio3)
        {
            case GameplayAudio3.GAMEPLAY3_1:
                break;
            case GameplayAudio3.GAMEPLAY3_2:
                break;
            case GameplayAudio3.GAMEPLAY3_3:
                break;
            case GameplayAudio3.NONE:
                break;
            default:
                break;
        }

        switch (audio4)
        {
            case GameplayAudio4.GAMEPLAY4:
                break;
            case GameplayAudio4.NONE:
                break;
            default:
                break;
        }
    }

    
}
