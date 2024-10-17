using System;
using UnityEngine;

/// <summary>
/// TestingTools is a MonoBehaviour class that is used to test the Unity Editor features
/// </summary>

[AddComponentMenu("MyTools/TestingTools")]
[HelpURL("https://docs.unity3d.com/")]
// [ExecuteInEditMode] // Add this line if you wanna execute the script in the Editor
// [ExecuteAlways] // Add this line if you wanna execute the script in the Editor and in Play mode
// [DisallowMultipleComponent] // Add this line if you wanna allow only one instance of the script in a GameObject
// [RequireComponent(typeof(Rigidbody))] // Add this line if you wanna make a Component required (Rigidbody in this case)   
public class TestingTools : MonoBehaviour
{
    [Header("Stats")]
    [Range(0, 100)]
    [SerializeField] private int health;
    [Range(0f, 100f)]
    [SerializeField] private float mana;
    [Space(2)]
    [Header("Description")]
    [SerializeField] private string playerName;
    [Range(0, 10)]
    [SerializeField] private float size;
    [Multiline(3)]
    [SerializeField] private string shortDescription;
    [TextArea(4,7)]
    [SerializeField] private string longDescription;
    [Tooltip("Should use Long description instead of the short one")]
    [SerializeField] private bool useLongDescription;
    [HideInInspector]
    public bool hidedVariable;


    /// <summary>
    /// Testing function
    /// </summary>
    [ContextMenu("Randomize Stats")]
    public void testFunction()
    {
        health = UnityEngine.Random.Range(30, 101);
        mana = UnityEngine.Random.Range(10f, 100f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnValidate is called when the script is loaded or a value is changed in the Inspector
    private void OnValidate()
    {
        Debug.Log("OnValidate - something changed");
    }

    // OnDrawGizmos draws Gizmos in the Scene view always
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, size);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, size/2);
    }

    // OnDrawGizmosSelected draws Gizmos in the Scene view only when the GameObject is selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(size, size, size));
    }

    // Reset is called when the Reset button is pressed in the Inspector, we can override it to reset the values of the GameObject
    private void Reset()
    {
        Debug.Log("Reseting the GameObject to default values");
        health = 100;
        mana = 100;
        size = 2;
    }


}
