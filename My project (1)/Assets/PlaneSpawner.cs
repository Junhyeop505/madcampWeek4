using UnityEngine;

public class PlaneSpawner:MonoBehaviour
{
    public GameObject[] planeModels;

    private void Start()
    {
        int selectedModel = PlaneData.Instance.selectedPlaneIndex;
        Instantiate(planeModels[selectedModel],transform.position, transform.rotation);
    }

}
