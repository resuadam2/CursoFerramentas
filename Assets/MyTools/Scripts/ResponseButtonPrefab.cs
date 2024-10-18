using TMPro;
using UnityEngine;

public class ResponseButtonPrefab : MonoBehaviour
{
    [Tooltip("Texto dónde cargamos la respuesta")]
    [SerializeField] public TextMeshProUGUI responseText;

    [SerializeField] public QuestionUILoader questionUILoader;

    [SerializeField] private bool isCorrectAnswer;

    public void ResponseButtonClicked()
    {
        questionUILoader.ButtonClicked(this.responseText.text);
    }

}
