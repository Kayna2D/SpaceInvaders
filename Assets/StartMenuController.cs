using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [Header("Fundo opcional")]
    public Sprite background;

    void Start()
    {
        CampaignManager.Instance.ResetCampaign();
        CreateInterface();
    }

    public void StartGame()
    {
        CampaignManager.Instance.StartCampaign();
    }

    private void CreateInterface()
    {
        Canvas canvas = ScreenUIBuilder.CreateCanvas("StartCanvas");
        ScreenUIBuilder.CreateBackground(
            canvas.transform,
            background,
            new Color(0.015f, 0.025f, 0.08f, 1f)
        );

        TextMeshProUGUI title = ScreenUIBuilder.CreateText(
            canvas.transform,
            "Title",
            "SPACE INVADERS",
            96f,
            new Vector2(0f, 170f),
            new Vector2(1200f, 180f)
        );
        title.fontStyle = FontStyles.Bold;

        Button startButton = ScreenUIBuilder.CreateButton(
            canvas.transform,
            "StartButton",
            "Iniciar jogo",
            new Vector2(0f, -90f),
            new Vector2(430f, 110f)
        );
        startButton.onClick.AddListener(StartGame);

        ScreenUIBuilder.EnsureEventSystem();
    }
}
