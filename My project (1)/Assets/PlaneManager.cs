using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlaneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject planePrefab;
    public Transform planeContainer;
    public Button addPlaneButton;
    public Button loadGameSceneButton;
    public GameObject addPlanePanel;

    public TMP_InputField inputWingArea;
    public TMP_InputField inputLiftCoefficient;
    public TMP_InputField inputDragCoefficient;
    public Button createPlaneButton;
    public Button closePanelButton;
    void Start()
    {
        addPlanePanel.SetActive(false);
        addPlaneButton.onClick.AddListener(OpenAddPlanePanel);
        createPlaneButton.onClick.AddListener(CreateNewPlane);
        closePanelButton.onClick.AddListener(CloseAddPlanePanel);
        loadGameSceneButton.onClick.AddListener(LoadGameScene);
    }

    public void OpenAddPlanePanel()
    {
        addPlanePanel.SetActive(true);
        planeContainer.gameObject.SetActive(false);

    }

    public void CloseAddPlanePanel()
    {
        addPlanePanel.SetActive(false);
        planeContainer.gameObject.SetActive(true);
    }

    private void CreateNewPlane()
    {
        float wingArea, liftCoefficient, dragCoefficient;
        if(float.TryParse(inputWingArea.text, out wingArea) && 
           float.TryParse(inputLiftCoefficient.text, out liftCoefficient) &&
            float.TryParse(inputDragCoefficient.text, out dragCoefficient))
        {
            //GameObject newPlane=Instantiate(planePrefab,new Vector3(0,0,0), Quaternion.identity);
            //Aerodynamics planeScript=newPlane.GetComponent<Aerodynamics>();

            //if(planeScript != null)
            //{
            //    planeScript.wingArea = wingArea;
            //    planeScript.liftCoefficient = liftCoefficient;
            //    planeScript.dragCoefficient = dragCoefficient;
            //}

            ////PlaneData.Instance.AddNewPlane(newPlane);
            //PlaneData.Instance.planeModels.Add(newPlane);

            ////CreatePlaneSelectionButton("Plane"+PlaneData.Instance.planeModels.Count, PlaneData.Instance.planeModels.Count-1);
            //int newIndex = PlaneData.Instance.planeModels.Count - 1;
            //CreatePlaneSelectionButton("Plane " + newIndex, newIndex);

            //addPlanePanel.SetActive(false);
            //planeContainer.gameObject.SetActive(true);
            PlaneData.Instance.planeModels.Add(planePrefab);
            int newIndex = PlaneData.Instance.planeModels.Count - 1;
            CreatePlaneSelectionButton("Plane" + newIndex, newIndex);
            addPlanePanel.SetActive(false);
            planeContainer.gameObject.SetActive(true);

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
        PlaneData.Instance.selectedPlaneIndex = index;
        Debug.Log("Plane selected: " + index);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("PlaymapScene");
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
}
