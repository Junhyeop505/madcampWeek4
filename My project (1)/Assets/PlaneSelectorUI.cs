using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ChoosePlane(int index)
    {
        PlaneData.Instance.SetPlaneModel(index);
        SceneManager.LoadScene("SampleScene");
    }
}
