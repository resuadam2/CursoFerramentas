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


    [Tooltip("Variable para saber si la pregunta ha sido cargada")]
    public bool isDataLoaded = false; // Variable para saber si la pregunta ha sido cargada

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

    public QuestionData questionData;


    public string GetQuestionString()
    {
        return questionData.question;
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
        List<string> randomList = new List<string>();
        while (answers.Count > 0)
        {
            System.Random random = new System.Random();
            int randomNumber = random.Next(0, answers.Count);
            randomList.Add(answers[randomNumber]);
            answers.RemoveAt(randomNumber);
        }

        foreach (string response in randomList)
        {
            ResponseButtonPrefab answerButon = Instantiate<ResponseButtonPrefab>(buttonResponsePrefab, responseCanvasContainer);
            answerButon.responseText.text = response;
            answerButon.questionUILoader = this;

        }
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
        isDataLoaded = true;
    }

    [ContextMenu("ClearUI")]
    public void ClearUI()
    {
        questionTitle.text = "";
        while (responseCanvasContainer.childCount > 0)
        {
            DestroyImmediate(responseCanvasContainer.GetChild(0).gameObject);
        }
        isDataLoaded = false;
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
            TriviaManager.instance.Questionresponsed(this, true);
        }
        else
        {
            Debug.Log("Respuesta incorrecta");
            TriviaManager.instance.Questionresponsed(this, false);
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

    internal async void Initialize()
    {
        LoadQuestionData();
    }

}
