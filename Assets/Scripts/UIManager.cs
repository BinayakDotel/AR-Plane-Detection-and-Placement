using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }
    public Text scoreTxt;
    public Button closeBtn;
    public Button placeShootBtn, shootBtn, restartBtn, showHidePlaneBtn;
    public Image shootingPoint;
    public Sprite show, hide;
    public bool showPlane;
    public PlacementHandler placementHandler;

    public GameObject Tracked;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        showPlane = false;
        instance = this;
        showHidePlaneBtn.onClick.AddListener(() => { showPlane = !showPlane; OnShowHidePlane(); });
        //UpdateScore();
        Tracked = GameObject.Find("Trackables");
    }

    public void UpdateScore()
    {
        scoreTxt.text = GameManager.Instance.score.ToString();
    }
    public void GameCompleteUI()
    {
        scoreTxt.text = "CONGRATULATIONS!";
        closeBtn.gameObject.SetActive(true);
        restartBtn.gameObject.SetActive(true);
    }
    public void OnShowHidePlane()
    {
        print("CLICKED");
        showHidePlaneBtn.GetComponent<Image>().sprite = showPlane ? show : hide;

        if (Tracked == null)
            Tracked = GameObject.Find("Trackables");
        Tracked.SetActive(showPlane);

        //PlacementHandler.Instance.showHidePlane(showPlane);
        //placementHandler.showHidePlane(showPlane);
    }
}
