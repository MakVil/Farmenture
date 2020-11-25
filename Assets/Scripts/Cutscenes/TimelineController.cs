using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector playableDirector;

    public static TimelineController Instance;

    private void Awake()
    {
        Instance = this;
        playableDirector = gameObject.GetComponent<PlayableDirector>();
    }

    public void PlayNewGameCutscene()
    {
        NewGameCutsceneController.Instance.Play();
    }

    public void StartCutscene()
    {
        MainCharacterController.Instance.inCutscene = true;
        TimeSystem.Instance.SetPaused(true);
    }

    public void EndCutscene()
    {
        MainCharacterController.Instance.inCutscene = false;
        TimeSystem.Instance.SetPaused(false);
    }
}
