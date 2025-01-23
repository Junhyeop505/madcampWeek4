using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text gradeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float distance = PlaneData.Instance.planeTravelDistance;
        string grade = GetGrade(distance);
        string colorGrade = (grade == "A") ? $"<color=yellow>{grade}</color>" : grade;
        gradeText.text = $"Distance: {distance:F2} meters\nGrade: {grade}";
    }

    private string GetGrade(float distance)
    {
        if (distance > 40)
            return $"<color=yellow>A</color>";
        else if (distance > 30)
            return "B";
        else if (distance > 20)
            return "C";
        else if (distance > 10)
            return "D";
        else return "F";
    }
}
