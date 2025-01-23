using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class PlaneManager : MonoBehaviour
{
    public GameObject planePrefab; // Assign PaperAirPlane prefab in Inspector
    public Transform planeContainer; // Assign a UI container for plane selection buttons
    public Button addPlaneButton;
    public Button loadGameSceneButton;

    private string apiUrl = "https://fastapi-app-313452959284.us-central1.run.app/results/";

    void Start()
    {
        addPlaneButton.onClick.AddListener(FetchAndCreatePlane);
        loadGameSceneButton.onClick.AddListener(LoadGameScene);

        if (planePrefab == null)
        {
            Debug.LogError("Plane prefab is not assigned in PlaneManager!");
        }
    }

    private void FetchAndCreatePlane()
    {
        StartCoroutine(FetchPlaneData());
    }

    private IEnumerator FetchPlaneData()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("API Response: " + jsonResponse);

            PlaneInfo[] planeDataArray = JsonHelper.FromJson<PlaneInfo>(jsonResponse);
            if (planeDataArray != null && planeDataArray.Length > 0)
            {
                CreatePlaneFromData(planeDataArray[0]); // Use the first entry in the response
            }
            else
            {
                Debug.LogError("No data returned from the API.");
            }
        }
        else
        {
            Debug.LogError($"Error fetching data: {request.error}");
        }
    }

    private void CreatePlaneFromData(PlaneInfo data)
{
    if (data == null)
    {
        Debug.LogError("Received null data in CreatePlaneFromData!");
        return;
    }

    GameObject newPlane = Instantiate(planePrefab, Vector3.zero, Quaternion.identity);
    newPlane.name = data.filename;

    Aerodynamics planeScript = newPlane.GetComponent<Aerodynamics>();
    if (planeScript != null)
    {
        planeScript.wingArea = data.wing_area;
        planeScript.liftCoefficient = data.lift_coefficient;
        planeScript.dragCoefficient = data.drag_coefficient;
    }

    if (PlaneData.Instance != null)
    {
        PlaneData.Instance.planeModels.Add(newPlane);
        DontDestroyOnLoad(newPlane);

        Debug.Log($"Plane added: {newPlane.name}. Total planes: {PlaneData.Instance.planeModels.Count}");

        PlaneData.Instance.selectedPlaneIndex = PlaneData.Instance.planeModels.Count - 1; // Automatically select
        Debug.Log($"Plane added and selected: {newPlane.name}");
    }
    else
    {
        Debug.LogError("PlaneData.Instance is null! Plane cannot be stored.");
    }
}

    private void CreatePlaneSelectionButton(string planeName, int index)
    {
        GameObject newButton = new GameObject(planeName,typeof(RectTransform));
        newButton.transform.SetParent(planeContainer, false);

        Button button=newButton.AddComponent<Button>();
        button.onClick.AddListener(()=>SelectPlane(index));

        Text buttonText=newButton.AddComponent<Text>();
        buttonText.text=planeName;
        buttonText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        buttonText.alignment=TextAnchor.MiddleLeft;
        buttonText.color = Color.black;
    }

    private void SelectPlane(int index)
    {
        if (PlaneData.Instance != null && index >= 0 && index < PlaneData.Instance.planeModels.Count)
        {
            PlaneData.Instance.selectedPlaneIndex = index;
            Debug.Log($"Plane selected: {PlaneData.Instance.planeModels[index].name}");
        }
        else
        {
            Debug.LogError($"Invalid plane index: {index}. Cannot select plane.");
        }
    }

    private void LoadGameScene()
{
    if (PlaneData.Instance == null || PlaneData.Instance.planeModels.Count == 0)
    {
        Debug.LogError("No planes available to load the game scene!");
        return;
    }

    if (PlaneData.Instance.selectedPlaneIndex < 0 || PlaneData.Instance.selectedPlaneIndex >= PlaneData.Instance.planeModels.Count)
    {
        Debug.LogError($"Invalid selectedPlaneIndex: {PlaneData.Instance.selectedPlaneIndex}. Cannot load game.");
        return;
    }

    Debug.Log("Start button clicked. Loading PlaymapScene...");
    SceneManager.LoadScene("PlaymapScene");
}

    [System.Serializable]
    public class PlaneInfo
    {
        public string filename;
        public float wing_area;
        public float lift_coefficient;
        public float drag_coefficient;
    }
}

   

    //public void AddPlane()
    //{
    //    if (planePrefab != null)
    //    {
    //        PlaneData.Instance.AddNewPlane(planePrefab);
    //        GameObject newButton = new GameObject("PlaneButton");
    //        newButton.AddComponent<RectTransform>();
    //        newButton.AddComponent<Button>().onClick.AddListener(() => SelectPlane(PlaneData.Instance.planeModels.Count - 1));
    //        newButton.transform.SetParent(planeContainer, false);

    //        Text buttonText = newButton.AddComponent<Text>();
    //        buttonText.text = "Plane " + PlaneData.Instance.planeModels.Count;
    //        buttonText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
    //        buttonText.alignment = TextAnchor.MiddleCenter;

    //        Debug.Log("new plane added");
    //    }
    //    else
    //    {
    //        Debug.LogError("plane prefab not assigned");
    //    }
    //}

    //public void SelectPlane(int index)
    //{
    //    PlaneData.Instance.selectedPlaneIndex = index;
    //}

