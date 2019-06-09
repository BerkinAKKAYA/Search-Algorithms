using UnityEngine;

public class ArrayManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI SearchingNumberText = null;
    [SerializeField] TMPro.TextMeshProUGUI ArrayUI = null;
    
    public static int SearchingNumber;
    private int[] array = null;
    private static ArrayManager instance;

    #region Awake
    void Awake()
    {
        instance = this;
        Populate();
    }
    #endregion

    #region Populate Array
    public void Populate()
    {
        var length = FindObjectOfType<Master>().arrayLength;

        Master.StopAllCoroutines();
        array = new int[length];

        for(int i=0; i<length; i++)
            array[i] = Random.Range(0, 100);

        ResetUI();
        UpdateSearchingNumber();
    }
    #endregion

    #region Reset UI
    public static void ResetUI()
    {
        var arrayVisualizer = instance.ArrayUI;
        var array = Array();
        var len = array.Length;

        arrayVisualizer.text = "[";

        for(int i=0; i<len; i++)
        {
            arrayVisualizer.text += "<color=\"black\">" + array[i] + "</color>";

            if(i != len-1)
                arrayVisualizer.text += ", ";
        }
        
        arrayVisualizer.text += "]";
    }
    #endregion

    #region Change Color of Number
    public static void ChangeColorOfNumber(int index, string color)
    {
        var arrayVisualizer = instance.ArrayUI;
        var array = Array();

        arrayVisualizer.text = "[";

        for(int i=0; i<array.Length; i++)
        {
            if(i==index) arrayVisualizer.text += "<color="+color+"><b>"+array[i]+"</b></color>";
            else         arrayVisualizer.text += array[i];   

            if(i != array.Length-1)
                arrayVisualizer.text += ", ";
        }

        arrayVisualizer.text += "]";
    }
    
    public static void ChangeColorOfTwoNumbers(int index1, int index2, string color)
    {
        var arrayVisualizer = instance.ArrayUI;
        var array = Array();

        arrayVisualizer.text = "[";

        for(int i=0; i<array.Length; i++)
        {
            if(i==index1)
                arrayVisualizer.text += "<color="+color+"><b>"+array[i]+"</b></color>";
            else if(i==index2)
                arrayVisualizer.text += "<color="+color+"><b>"+array[i]+"</b></color>";
            else
                arrayVisualizer.text += array[i];   

            if(i != array.Length-1)
                arrayVisualizer.text += ", ";
        }

        arrayVisualizer.text += "]";
    }
    #endregion

    #region Update Searching Number
    public static void UpdateSearchingNumber()
    {
        int index  = Random.Range(0, Array().Length);
        int number = Array()[index];
        SearchingNumber = number;

        var ui  = instance.SearchingNumberText;
        ui.text = number.ToString();
    }
    #endregion

    #region Get Array
    public static int[] Array() => instance.array;
    #endregion
}