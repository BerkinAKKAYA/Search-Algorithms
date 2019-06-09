using UnityEngine;
using UnityEditor;

public class Master : MonoBehaviour
{
    public int arrayLength = 50;

    #region Speed Settings
    [Header("Speed Settings")]
    public bool instant = false;
    public float stepLength = 0;

    public static bool Instant => FindObjectOfType<Master>().instant;
    public static float StepLength => FindObjectOfType<Master>().stepLength;
    #endregion

    #region Algorithm Enums
    [Header("Algorithms")]
    public SearchAlgorithm searchAlgorithm = 0;
    public SortAlgorithm sortAlgorithm = 0;

    public enum SearchAlgorithm{
        Linear,
        Binary,
        Jump,
        Interpolation,
        Exponential,
        Ternary
    }

    public enum SortAlgorithm{
        Selection,
        Bubble,
        Insertion,
        Merge
    }
    #endregion

    #region Stop All Coroutines
    public new static void StopAllCoroutines()
    {
        FindObjectOfType<SearchAlgorithms>().StopAllCoroutines();
        FindObjectOfType<SortAlgorithms>().StopAllCoroutines();
    }
    #endregion
}

#region Editor
[CustomEditor(typeof(Master))]
public class MasterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(Application.isPlaying)
        {
            var master  = (Master)target;
            var arrMngr = FindObjectOfType<ArrayManager>();
            var sort    = FindObjectOfType<SortAlgorithms>();
            var search  = FindObjectOfType<SearchAlgorithms>();

            if(GUILayout.Button("Populate")) arrMngr.Populate();
            if(GUILayout.Button("Sort"))     sort.Sort(master.sortAlgorithm);
            if(GUILayout.Button("Search"))   search.Search(master.searchAlgorithm);
        }
    }
}
#endregion