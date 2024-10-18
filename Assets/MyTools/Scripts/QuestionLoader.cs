using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class QuestionLoader : MonoBehaviour 
{
    [SerializeField]
    private string urlToObtainQuestions = "https://opentdb.com/api.php?amount=10";

    [TextArea(3, 20)] // Podemos modificar el tamaño del campo en el inspector
    [SerializeField] private string urlResponse;

    [SerializeField] public DataObtained d;

    // Versión con corutinas
    IEnumerator GetText()
    {
        if (!urlToObtainQuestions.Contains(("http")))
        {
            urlToObtainQuestions = Application.streamingAssetsPath + "/" + urlToObtainQuestions;
        }

        using (UnityWebRequest www = UnityWebRequest.Get(urlToObtainQuestions))
        {
            yield return www.SendWebRequest(); // yield return se usa para esperar a que se complete la petición pero el código sigue ejecutándose tras esta línea cuando la petición se completa

            if (www.error != null) // isNetorkError y isHttpError están obsoletos - cambiado a error != null por comodidad
            {
                Debug.Log(www.error);
            }
            else
            {
                String s = www.downloadHandler.text;
                // Show results as text
                Debug.Log(s);
                urlResponse = s;
                d = JsonUtility.FromJson<DataObtained>(s);
                d.ConvertRawData();
            }
        }
    }

    // Versión con async/await (mejor opción) - funciones asyncronas de C#

    /// <summary>
    /// Función estática para poder ser llamada desde cualquier parte del código y desde el inspector
    /// Esta función se encarga de hacer una petición a una URL y devolver los datos obtenidos
    /// </summary>
    /// <param name="url"></param> URL a la que se va a hacer la petición
    /// <returns></returns> Devuelve un objeto de tipo DataObtained con los datos obtenidos
    public static async Task<DataObtained> GetDataFromURL(string url)
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


    [ContextMenu("Load Questions")]
    void Start()
    {
        StartCoroutine(GetText());
    }

    public async void LoadDataWithString(string url)
    {

    }

    [ContextMenu("LoadQuestionData")]
    public async void LoadQuestionDataInUI()
    {
       DataObtained d = await
       QuestionLoader.GetDataFromURL("https://opentdb.com/api.php?amount=1"); // cargamos una sola pregunta para comprobar que funciona
       if (d != null)
       {
           Debug.Log(d.finalResults[0].question); // Mostramos la pregunta en consola
        }
    }

}