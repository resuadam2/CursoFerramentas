using NUnit.Framework;
using System.Collections.Generic;
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
    public async void LoadQuestionData()
    {
        DataObtained d = await
        QuestionLoader.GetDataFromURL("https://opentdb.com/api.php?amount=1"); // cargamos una sola pregunta para comprobar que funciona
        if (d != null)
        {
            Debug.Log(d.finalResults[0].question); // Mostramos la pregunta en consola
            questionData = d.finalResults[0]; // Guardamos la pregunta en la variable questionData
        }
    }

    private void RandomizeResponses()
    {
        List<string> answers = new List<string>();
        answers.Add(questionData.correct_answer);
        foreach (string res in questionData.incorrect_answers)
        {
            answers.Add(res);
        }

        List<string> randomizedAnswers = new List<string>();
        // TODO: Randomize the answers and create the buttons
    }

    [ContextMenu("InflateUI")]
    public void InflateUI()
    {
        
        if (questionData != null)
        {
            ClearUI();
            questionTitle.text = questionData.question;
            RandomizeResponses();
        }
    }

    [ContextMenu("ClearUI")]
    public void ClearUI()
    {
        questionTitle.text = "";
        while (responseCanvasContainer.childCount > 0)
        {
            DestroyImmediate(responseCanvasContainer.GetChild(0).gameObject);
        }
    }

    [ContextMenu("LoadAndInflate")]
    public void LoadAndInflate()
    {
        LoadQuestionData();
        InflateUI();
    }

    public void ButtonClicked(string response)
    {
        if (response == questionData.correct_answer)
        {
            Debug.Log("Respuesta correcta");
        }
        else
        {
            Debug.Log("Respuesta incorrecta");
        }
    }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        if (questionData.question.Length == 0)
        {
            LoadQuestionData();
        }
        InflateUI();
    }
}
