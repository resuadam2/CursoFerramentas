using TMPro;
using UnityEngine;

public class QuestionUILoader : MonoBehaviour
{
    [Tooltip("Texto dónde cargamos la pregunta")]
    public TextMeshProUGUI questionTitle;

    [Tooltip("Contenedor de las respuestas")]
    public Transform responseCanvasContainer;

    [Tooltip("Prefab del botón con una posible respuesta")]
    public ResponseButtonPrefab buttonResponsePrefab;

    public QuestionData questionData;

    [ContextMenu("LoadQuestionData")]
    public async void InflateData()
    {
        DataObtained d = await
        QuestionLoader.GetDataFromURL("https://opentdb.com/api.php?amount=1"); // cargamos una sola pregunta para comprobar que funciona
        if (d != null)
        {
            Debug.Log(d.finalResults[0].question); // Mostramos la pregunta en consola
            questionData = d.finalResults[0]; // Guardamos la pregunta en la variable questionData
        }
    }

    [ContextMenu("Inflate")]
    public void LoadUI()
    {
        
        if (questionData != null)
        {
            questionTitle.text = questionData.question;
            ResponseButtonPrefab correctAnswer = Instantiate<ResponseButtonPrefab>(buttonResponsePrefab, responseCanvasContainer);
            correctAnswer.responseText.text = questionData.correct_answer;
            foreach (string res in questionData.incorrect_answers)
            {
                ResponseButtonPrefab incorrectAnswer = Instantiate<ResponseButtonPrefab>(buttonResponsePrefab, responseCanvasContainer);
                incorrectAnswer.responseText.text = res;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
}
