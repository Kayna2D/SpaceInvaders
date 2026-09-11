using UnityEngine;
using UnityEngine.SceneManagement;

public enum CampaignResult
{
    None,
    Victory,
    Defeat
}

public class CampaignManager : MonoBehaviour
{
    public const string StartSceneName = "StartScene";
    public const string PhaseOneSceneName = "MainScene";
    public const string PhaseTwoSceneName = "Phase2Scene";
    public const string FinalSceneName = "FinalScene";

    private static CampaignManager instance;

    public static CampaignManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<CampaignManager>();

                if (instance == null)
                {
                    GameObject campaignObject = new GameObject("CampaignManager");
                    instance = campaignObject.AddComponent<CampaignManager>();
                }
            }

            return instance;
        }
    }

    public int Score { get; private set; }
    public int Lives { get; private set; } = 3;
    public int CurrentPhase { get; private set; } = 1;
    public CampaignResult Result { get; private set; } = CampaignResult.None;

    private bool campaignActive;
    private bool transitionInProgress;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            instance = null;
        }
    }

    public void ResetCampaign()
    {
        Time.timeScale = 1f;
        Score = 0;
        Lives = 3;
        CurrentPhase = 1;
        Result = CampaignResult.None;
        campaignActive = false;
        transitionInProgress = false;
    }

    public void StartCampaign()
    {
        ResetCampaign();
        campaignActive = true;
        LoadScene(PhaseOneSceneName);
    }

    public void EnsureCampaignStarted(int phase)
    {
        Time.timeScale = 1f;

        if (!campaignActive && Result == CampaignResult.None)
        {
            Score = 0;
            Lives = 3;
            campaignActive = true;
        }

        CurrentPhase = phase;
        transitionInProgress = false;
    }

    public void AddScore(int points)
    {
        if (!campaignActive || transitionInProgress)
        {
            return;
        }

        Score += points;
    }

    public void LoseLife()
    {
        if (!campaignActive || transitionInProgress)
        {
            return;
        }

        Lives = Mathf.Max(0, Lives - 1);

        if (Lives == 0)
        {
            FinishCampaign(CampaignResult.Defeat);
        }
    }

    public void CompletePhase()
    {
        if (!campaignActive || transitionInProgress)
        {
            return;
        }

        if (CurrentPhase == 1)
        {
            CurrentPhase = 2;
            LoadScene(PhaseTwoSceneName);
            return;
        }

        FinishCampaign(CampaignResult.Victory);
    }

    public void Restart()
    {
        ResetCampaign();
        LoadScene(StartSceneName);
    }

    private void FinishCampaign(CampaignResult result)
    {
        Result = result;
        campaignActive = false;
        LoadScene(FinalSceneName);
    }

    private void LoadScene(string sceneName)
    {
        if (transitionInProgress)
        {
            return;
        }

        transitionInProgress = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
        transitionInProgress = false;
    }
}
