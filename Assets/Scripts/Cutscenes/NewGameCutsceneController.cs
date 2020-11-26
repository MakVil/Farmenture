using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameCutsceneController : MonoBehaviour
{
    public static Color MainCharColor = new Color32(7, 62, 159, 255);
    public static Color KevinColor = new Color32(5, 111, 12, 255);
    public static Color defaultColor = new Color32(232, 99, 10, 255);

    public static NewGameCutsceneController Instance;

    public List<string> dialog = new List<string>();
    public List<Color> colors = new List<Color>();

    public int nextLine;


    private void Awake()
    {
        Instance = this;
        InitDialog();
    }

    public void Play()
    {
        ShowDialog();
    }

    public void ShowDialog()
    {
        DialogController.Instance.SetText(dialog[nextLine], colors[nextLine]);
        UIController.Instance.OpenDialog();
        nextLine++;
    }

    public void CloseDialog()
    {
        UIController.Instance.CloseDialog();
    }

    private void InitDialog()
    {
        AddLine("Aah, my head...", MainCharColor);
        AddLine("What... is this place? And these clothes?", MainCharColor);
        AddLine("Prr... Prr...", defaultColor);
        AddLine("Kevin?", MainCharColor);
        AddLine("Sam! Has the hangover hit you already?", KevinColor);
        AddLine("Yea, just woke up. What happened last night? I don't know where I am...", MainCharColor);
        AddLine("HAHA! Really? We were in the Red dog and you had some argument with an old farmer.", KevinColor);
        AddLine("You said you don't believe that farming is hard work. The farmer wanted to prove you wrong and took you with him.", KevinColor);
        AddLine("We tried to stop you but you were actually kinda exited. And me and Maria were witnesses for your...", KevinColor);
        AddLine("You really don't remember any of this?", KevinColor);
        AddLine("NO! Tell me. What you witnessed?", MainCharColor);
        AddLine("Your contract to buy the old farmer's farm!", KevinColor);
        AddLine("I DID WHAT! I don't have that kind of money.", MainCharColor);
        AddLine("It cost you almost your whole fortune.", KevinColor);
        AddLine("But I have to go now. Call me later!", KevinColor);
        AddLine("But wait...", MainCharColor);
        AddLine("Peep peep", defaultColor);
        AddLine("What I am gonna do now...", MainCharColor);
    }

    private void AddLine(string t, Color c)
    {
        dialog.Add(t);
        colors.Add(c);
    }
}
