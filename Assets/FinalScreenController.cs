using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalScreenController : MonoBehaviour
{
    [Header("Fundo opcional")]
    public Sprite background;

    void Start()
    {
        Time.timeScale = 1f;
        CreateInterface();
    }

    public void RestartGame()
    {
        CampaignManager.Instance.Restart();
    }

    private void CreateInterface()
    {
        CampaignManager campaign = CampaignManager.Instance;
        bool victory = campaign.Result == CampaignResult.Victory;

        Canvas canvas = ScreenUIBuilder.CreateCanvas("FinalCanvas");
        ScreenUIBuilder.CreateBackground(
            canvas.transform,
            background,
            new Color(0.025f, 0.02f, 0.07f, 1f)
        );

        TextMeshProUGUI resultText = ScreenUIBuilder.CreateText(
            canvas.transform,
            "ResultText",
            victory ? "Você venceu!" : "Game Over",
            92f,
            new Vector2(0f, 190f),
            new Vector2(1200f, 170f)
        );
        resultText.color = victory
            ? new Color(0.35f, 1f, 0.55f, 1f)
            : new Color(1f, 0.3f, 0.3f, 1f);
        resultText.fontStyle = FontStyles.Bold;

        ScreenUIBuilder.CreateText(
            canvas.transform,
            "FinalScoreText",
            "Pontuação final: " + campaign.Score,
            54f,
            new Vector2(0f, 40f),
            new Vector2(900f, 100f)
        );

        Button restartButton = ScreenUIBuilder.CreateButton(
            canvas.transform,
            "RestartButton",
            "Reiniciar",
            new Vector2(0f, -150f),
            new Vector2(380f, 110f)
        );
        restartButton.onClick.AddListener(RestartGame);

        ScreenUIBuilder.EnsureEventSystem();
    }
}
