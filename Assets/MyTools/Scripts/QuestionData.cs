using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/** Example of response form the trivial api:
  "response_code": 0,
  "results": [
    {
      "type": "multiple",
      "difficulty": "hard",
      "category": "Entertainment: Japanese Anime &amp; Manga",
      "question": "Who was the Director of the 1988 Anime film &quot;Grave of the Fireflies&quot;?",
      "correct_answer": "Isao Takahata",
      "incorrect_answers": [
        "Hayao Miyazaki",
        "Satoshi Kon",
        "Sunao Katabuchi"
      ]
    },
*/

[Serializable]
public enum QuestionType
{
    MULTIPLE,
    BOOLEAN
}

[Serializable]
public enum QuestionDifficulty
{
    EASY,
    MEDIUM,
    HARD
}

[Serializable]
public class DataObtained
{
    [SerializeField] public int response_code;
    [HideInInspector]
    public List<QuestionRawData> results;
    [SerializeField] public List<QuestionData> finalResults;

    public void ConvertRawData()
    {
        if (results.Count > 0)
        {
            foreach (QuestionRawData data in results)
                finalResults.Add(new QuestionData(data));
        }
    }
}

[Serializable]
public class QuestionRawData
{
    public string type;
    public string difficulty;
    public string category;
    public string question;
    public string correct_answer;
    public List<string> incorrect_answers;
}

[CustomPropertyDrawer(typeof(QuestionData))]
public class QuestionDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        base.OnGUI(position, property, label);
        SerializedProperty questionTitle = property.FindPropertyRelative("question");
        SerializedProperty correctAnswer = property.FindPropertyRelative("correct_answer");
        SerializedProperty incorrectAnswers = property.FindPropertyRelative("incorrect_answers");
        SerializedProperty type = property.FindPropertyRelative("type");
        SerializedProperty difficulty = property.FindPropertyRelative("difficulty");
        SerializedProperty category = property.FindPropertyRelative("category");

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Question: ", questionTitle.stringValue);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Correct Answer: ", correctAnswer.stringValue);
        for (int i = 0; i < incorrectAnswers.arraySize; i++)
        {
            EditorGUILayout.LabelField("Incorrect Answer " + i + ": ", incorrectAnswers.GetArrayElementAtIndex(i).stringValue);
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Type: ", type.enumNames[type.enumValueIndex]);
        EditorGUILayout.LabelField("Difficulty: ", difficulty.enumNames[difficulty.enumValueIndex]);
        EditorGUILayout.LabelField("Category: ", category.stringValue);


    }
}

[Serializable]
public class QuestionData
{
    public QuestionType type;
    public QuestionDifficulty difficulty;
    public string category;
    public string question;
    public string correct_answer;
    public List<string> incorrect_answers;

    public QuestionData(QuestionRawData raw)
    {
        category = raw.category;
        question = raw.question;
        correct_answer = raw.correct_answer;
        incorrect_answers = raw.incorrect_answers;
        //Parseo de tipo
        switch (raw.type)
        {
            case "multiple":
                type = QuestionType.MULTIPLE;
                break;
            case "boolean":
                type = QuestionType.BOOLEAN;
                break;
            default:
                Debug.Log("Type not found.");
                type = QuestionType.MULTIPLE;
                break;
        }
        //Parseo de dificultad
        switch (raw.difficulty)
        {
            case "easy":
                difficulty = QuestionDifficulty.EASY;
                break;
            case "medium":
                difficulty = QuestionDifficulty.MEDIUM;
                break;
            case "hard":
                difficulty = QuestionDifficulty.HARD;
                break;
            default:
                Debug.Log("Difficulty not found.");
                difficulty = QuestionDifficulty.EASY;
                break;
        }
    }
}
