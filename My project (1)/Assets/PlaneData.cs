using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlaneData : MonoBehaviour
{
    public static PlaneData Instance;
    public List<GameObject> planeModels = new List<GameObject>();
    public int selectedPlaneIndex = -1;
     public float planeTravelDistance = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public void AddNewPlane(GameObject newPlanePrefab)
    //{
    //    planeModels.Add(newPlanePrefab);
    //}

    public void SetPlaneModel(int index)
    {
        selectedPlaneIndex = index;
    }

    public GameObject GetSelectPlane()
    {
        return planeModels[selectedPlaneIndex];
    }

    //public void SpawnSelectedPlane(Vector3 position, Quaternion rotation)
    //{
    //    GameObject selectedPlane = GetSelectPlane();
    //    if (selectedPlane != null)
    //    {
    //        Instantiate(selectedPlane, position, rotation);
    //        Debug.Log("spawned plane: " + selectedPlane.name);
    //    }
    //}

    //public struct PlaneAttributes
    //{
    //    public float WingArea;
    //    public float LiftCoefficient;
    //    public float DragCoefficient;
    //}
}
public struct PlaneAttributes
{
    public float WingArea;
    public float LiftCoefficient;
    public float DragCoefficient;
}

