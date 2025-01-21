using UnityEngine;
using UnityEngine.UI;

public class PlaneShapeSwitcher : MonoBehaviour
{
    public GameObject[] planeModels;
    private GameObject currentPlane;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (planeModels.Length > 0)
        {
            ChangePlaneModel(0);
        }
        
    }

    public void ChangePlaneModel(int index)
    {
        if (index < 0 || index >= planeModels.Length)
        {
            Debug.LogWarning("invalid");
            return;
        }
        if (currentPlane != null) 
        { 
            Destroy(currentPlane);
        }

        currentPlane= Instantiate(planeModels[index], transform.position, transform.rotation);
        currentPlane.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
