using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class TriviaManager : MonoBehaviour
{
    public static TriviaManager instance;
    [SerializeField] int numberOfVictories = 0;
    [SerializeField] int limitOfVictories = 10;
    [SerializeField] GameObject victoryUI;
    [SerializeField] int numberOfDefeats = 0;
    [SerializeField] int limitOfDefeats = 10;
    [SerializeField] GameObject defeatUI;

    [Tooltip("Prefab de Pregunta a mostrar")]
    [SerializeField] private QuestionUILoader questionPrefab;

    [Tooltip("Contenedor de las preguntas")]
    [SerializeField] Transform questionsParent;

    private List<QuestionUILoader> loadedQuestions = new List<QuestionUILoader>();



    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public async Task<DataObtained> GetDataFromURL(string url)
    {
        try
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                await www.SendWebRequest();
                if (www.error != null)
                {
                    Debug.Log(www.error);
                    return null;
                }
                else
                {
                    String s = www.downloadHandler.text;
                    DataObtained actualData = JsonUtility.FromJson<DataObtained>(s);
                    actualData.ConvertRawData();
                    return actualData;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al hacer la petición: {e.Message}");
            return null;
        }
    }

    public void LoadNextQuestion()
    {
        QuestionUILoader question = Instantiate<QuestionUILoader>(questionPrefab, questionsParent);
        loadedQuestions.Add(question);
        question.Initialize();        
    }

    public void Questionresponsed(QuestionUILoader question, bool response)
    {
        if (response)
        {
            numberOfVictories++;
            if (numberOfVictories >= limitOfVictories)
            {
                ShowVictory();
            }
            else LoadNextQuestion();
        }
        else
        {
            numberOfDefeats++;
            if (numberOfDefeats >= limitOfDefeats)
            {
                ShowDefeat();
            }
            else LoadNextQuestion();
        }
        loadedQuestions.Remove(question);
        Destroy(question.gameObject);
    }

    public void ShowVictory()
    {
        Debug.Log("Has ganado");
        victoryUI.SetActive(true);
    }

    public void ShowDefeat()
    {
        Debug.Log("Has perdido");
        defeatUI.SetActive(true);
    }

    public void ResetQuestions()
    {
        victoryUI.SetActive(false);
        defeatUI.SetActive(false);
        foreach (QuestionUILoader question in loadedQuestions)
        {
            Destroy(question.gameObject);
        }
        numberOfDefeats = 0;
        numberOfVictories = 0;
    }

    public void StartGame()
    {
        ResetQuestions();
        LoadNextQuestion();
    }

    public void Start()
    {
        StartGame();
    }
}
