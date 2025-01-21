//using UnityEngine;

//public class PlaneSpawner:MonoBehaviour
//{
//    public GameObject[] planeModels;

//    private void Start()
//    {
//        int selectedModel = PlaneData.Instance.selectedPlaneIndex;
//        Instantiate(planeModels[selectedModel],transform.position, transform.rotation);
//    }

//}
//using UnityEngine;

//public class PlaneSpawner : MonoBehaviour
//{
//    //public GameObject[] planeModels;  // Assign all plane prefabs in Inspector
//    public Vector3 spawnPosition = new Vector3(0, 0, 0);
//    public Quaternion spawnRotation = Quaternion.identity;

//    //void Start()
//    //{
//    //    if (PlaneData.Instance != null)
//    //    {
//    //        int selectedModel = PlaneData.Instance.selectedPlaneIndex;
//    //        if (selectedModel >= 0 && selectedModel < planeModels.Length)
//    //        {
//    //            Instantiate(planeModels[selectedModel], new Vector3(0, 0, 0), Quaternion.identity);
//    //            Debug.Log("Instantiated plane model: " + selectedModel);
//    //        }
//    //        else
//    //        {
//    //            Debug.LogError("Invalid plane index.");
//    //        }
//    //    }
//    //    else
//    //    {
//    //        Debug.LogError("PlaneData instance is null!");
//    //    }
//    //}
//    void Start()
//    {
//        if (PlaneData.Instance == null)
//        {
//            Debug.LogError("PlaneData instance is null! Plane data did not persist.");
//            return;
//        }

//        Debug.Log("PlaneData instance found.");
//        Debug.Log("Planes available in PlaneData: " + PlaneData.Instance.planeModels.Count);

//        int selectedModel = PlaneData.Instance.selectedPlaneIndex;
//        Debug.Log("Selected Plane Index: " + selectedModel);

//        if (selectedModel >= 0 && selectedModel < PlaneData.Instance.planeModels.Count)
//        {
//            GameObject selectedPlanePrefab = PlaneData.Instance.GetSelectPlane();
//            if (selectedPlanePrefab != null)
//            {
//                GameObject spawnedPlane = Instantiate(selectedPlanePrefab, transform.position, transform.rotation);
//                Debug.Log("Instantiated plane model: " + spawnedPlane.name);
//            }
//            else
//            {
//                Debug.LogError("Selected plane prefab is null!");
//            }
//        }
//        else
//        {
//            Debug.LogError("Invalid plane index: " + selectedModel);
//        }
//    }

//}

using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public Vector3 spawnPosition = new Vector3(0, 0, 0);
    public Quaternion spawnRotation = Quaternion.identity;

    void Start()
    {
        //GameObject planeSpawnerPrefab = Resources.Load<GameObject>("PlaneSpawner");
        if (PlaneData.Instance == null)
        {
            Debug.LogError("PlaneData instance is null! Ensure PlaneData exists.");
            return;
        }

        GameObject selectedPlanePrefab = PlaneData.Instance.GetSelectPlane();
        if (selectedPlanePrefab != null)
        {
            GameObject spawnedPlane = Instantiate(selectedPlanePrefab, spawnPosition, spawnRotation);
            Debug.Log("Spawned plane: " + spawnedPlane.name);
        }
        else
        {
            Debug.LogError("No plane selected, unable to spawn.");
        }
    }

    //private void Start()
    //{
    //    GameObject planeSpawnerPrefab = Resources.Load<GameObject>("PlaneSpawner");
    //    if (planeSpawnerPrefab != null)
    //    {
    //        Instantiate(planeSpawnerPrefab);
    //        Debug.Log("Plane spawner instantiate");
    //    }
    //}
}
