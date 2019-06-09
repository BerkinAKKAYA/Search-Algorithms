using UnityEngine;
using System.Collections;

public class SearchAlgorithms : MonoBehaviour
{
    #region Search
    public void Search(Master.SearchAlgorithm algorithm)
    {
        Master.StopAllCoroutines();
        
        if (algorithm==Master.SearchAlgorithm.Linear)
            StartCoroutine("LinearSearch");
        else if (algorithm==Master.SearchAlgorithm.Binary)
            StartCoroutine("BinarySearch");
        else if (algorithm==Master.SearchAlgorithm.Jump)
            StartCoroutine("JumpSearch");
        else if (algorithm==Master.SearchAlgorithm.Interpolation)
            StartCoroutine("InterpolationSearch");
        else if (algorithm==Master.SearchAlgorithm.Exponential)
            StartCoroutine("ExponentialSearch");
        else if (algorithm==Master.SearchAlgorithm.Ternary)
            StartCoroutine("TernarySearch");
    }
    #endregion

    #region Linear Search
    IEnumerator LinearSearch()
    {
        int key = ArrayManager.SearchingNumber;

        int i=0;
        foreach(int num in ArrayManager.Array())
        {
            if(num == key)
            {
                ArrayManager.ChangeColorOfNumber(i, "yellow");
                break;
            }
            
            ArrayManager.ChangeColorOfNumber(i, "red");
            i++;

            if(!Master.Instant)
                yield return new WaitForSeconds(Master.StepLength);
        }
    }
    #endregion

    #region Binary Search
    IEnumerator BinarySearch()
    {
        var arr   = ArrayManager.Array();
        int left  = 0;
        int right = arr.Length-1;

        int key = ArrayManager.SearchingNumber;

        while(left <= right)
        {
            int mid = (left+right) / 2;
            int midVal = arr[mid];

            if(midVal == key)
            {
                ArrayManager.ChangeColorOfNumber(mid, "yellow");
                break;
            }
            else if(midVal > key) right = mid-1;
            else if(midVal < key) left  = mid+1;
            
            ArrayManager.ChangeColorOfNumber(mid, "red");
            
            if(!Master.Instant)
                yield return new WaitForSeconds(Master.StepLength);
        }
    }
    #endregion

    #region Jump Search
    IEnumerator JumpSearch()
    {
        var arr = ArrayManager.Array();
        int len = arr.Length;

        int lengthRoot = Mathf.FloorToInt(Mathf.Sqrt(len));
        int step = lengthRoot;
        int prev = 0;

        int key = ArrayManager.SearchingNumber;

        while(arr[Mathf.Min(step,len) - 1] < key)
        {
            prev  = step;
            step += lengthRoot;

            if(prev >= len)
                break;

            if(!Master.Instant)
            {
                ArrayManager.ChangeColorOfNumber(prev, "red");
                yield return new WaitForSeconds(Master.StepLength);
            }
        }

        while(arr[prev] < key)
        {
            prev++;

            if(prev == Mathf.Min(step, len))
                break;

            if(!Master.Instant)
            {
                ArrayManager.ChangeColorOfNumber(prev, "red");
                yield return new WaitForSeconds(Master.StepLength);
            }
        }

        if(arr[prev] == key)
            ArrayManager.ChangeColorOfNumber(prev, "yellow");
    }
    #endregion

    #region Interpolation Search
    IEnumerator InterpolationSearch()
    {
        var array = ArrayManager.Array();

        int low  = 0;
        int high = (array.Length-1);
        int key  = ArrayManager.SearchingNumber;

        while(low<=high && key>=array[low] && key<=array[high])
        {
            if (low == high)
            {
                if (array[low] == key)
                    ArrayManager.ChangeColorOfNumber(low, "yellow");

                break;
            }

            int pos = low + (((high-low) / (array[high]-array[low]))) * (key-array[low]);

            if(array[pos] == key)
            {
                ArrayManager.ChangeColorOfNumber(pos, "yellow");
                break;
            }

            if(array[pos]<key) low  = (pos+1);
            else               high = (pos-1);
            
            ArrayManager.ChangeColorOfNumber(pos, "red");
            
            if(!Master.Instant)
                yield return new WaitForSeconds(Master.StepLength);
        }
    }
    #endregion

    #region Exponential Search
    IEnumerator ExponentialSearch()
    {
        var arr = ArrayManager.Array();
        int key = ArrayManager.SearchingNumber;
        int len = arr.Length;

        bool found = false;

        if(arr[0] == key)
        {
            ArrayManager.ChangeColorOfNumber(0, "red");
            found = true;
        }

        int i=1;
        while((i<len) && (arr[i]<=key) && !found)
        {
            i *= 2;
            ArrayManager.ChangeColorOfNumber(i, "red");
            
            if(!Master.Instant)
                yield return new WaitForSeconds(Master.StepLength);
        }
        
        //  BINARY SEARCH
        int result = 0;
        int left   = 0;
        int right  = arr.Length-1;

        while(left <= right)
        {
            int mid = (left+right) / 2;

            if(arr[mid] == key)
            {
                result = mid;
                break;
            }
            else if(arr[mid] < key) left  = mid+1;
            else if(arr[mid] > key) right = mid-1;
            
            ArrayManager.ChangeColorOfNumber(mid, "red");
            
            if(!Master.Instant)
                yield return new WaitForSeconds(Master.StepLength);
        }
        // #BINARY SEARCH
        
        ArrayManager.ChangeColorOfNumber(result, "yellow");
    }
    #endregion

    #region Ternary Search
    IEnumerator TernarySearch()
    {
        var arr = ArrayManager.Array();
        int key = ArrayManager.SearchingNumber;

        int l = 0;
        int r = arr.Length;

        while (r >= l) 
        { 
            int mid1 = l + (r - l) / 3; 
            int mid2 = r - (r - l) / 3;

            if (arr[mid1] == key)
            {
                ArrayManager.ChangeColorOfNumber(mid1, "yellow");
                break;
            }
            else if (arr[mid2] == key)
            {
                ArrayManager.ChangeColorOfNumber(mid2, "yellow");
                break;
            }
            
                 if (key < arr[mid1]) r = mid1-1; 
            else if (key > arr[mid2]) l = mid2+1; 
            else {
                l = mid1+1;
                r = mid2-1;
            }
            
            if(!Master.Instant)
            {
                ArrayManager.ChangeColorOfTwoNumbers(mid1, mid2, "red");
                yield return new WaitForSeconds(Master.StepLength);
            }
        }
    }
    #endregion
}