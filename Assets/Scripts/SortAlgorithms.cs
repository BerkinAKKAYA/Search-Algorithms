using UnityEngine;
using System.Collections;

public class SortAlgorithms : MonoBehaviour
{
    #region Sort
    public void Sort(Master.SortAlgorithm algorithm)
    {
        Master.StopAllCoroutines();
        
        if (algorithm==Master.SortAlgorithm.Selection)
            StartCoroutine("SelectionSort");
        else if (algorithm==Master.SortAlgorithm.Bubble)
            StartCoroutine("BubbleSort");
        else if (algorithm==Master.SortAlgorithm.Insertion)
            StartCoroutine("InsertionSort");
        else if (algorithm==Master.SortAlgorithm.Merge)
            StartCoroutine("MergeSort");
    }
    #endregion

    #region Selection Sort
    IEnumerator SelectionSort()
    {
        var arr = ArrayManager.Array();
        int len = arr.Length;

        for(int i=0; i<len-1; i++)
        {
            int min = i;

            for(int j=i+1; j<len; j++)
            {
                if(arr[j] < arr[min])
                    min = j;
            }

            bool swapped = arr[min] != arr[i];

            int toSwap = arr[min];
            arr[min]   = arr[i];
            arr[i]     = toSwap;

            if(swapped && !Master.Instant)
            {
                ArrayManager.ChangeColorOfNumber(i, "red");
                yield return new WaitForSeconds(Master.StepLength);
            }
        }
        
        ArrayManager.ResetUI();
    }
    #endregion
    
    #region Bubble Sort
    IEnumerator BubbleSort()
    {
        var arr = ArrayManager.Array();
        int len = arr.Length;

        for(int x=0; x<len-1; x++){

            bool swapped = false;

            for(int y=0; y<len-x-1; y++){
                if(arr[y] > arr[y+1])
                {
                    swapped = (arr[y] != arr[y+1]);

                    int toSwap = arr[y];
                    arr[y]     = arr[y+1];
                    arr[y+1]   = toSwap;
                }
            }

            if(swapped && !Master.Instant)
            {
                ArrayManager.ChangeColorOfNumber(x, "red");
                yield return new WaitForSeconds(Master.StepLength);
            }
        }

        ArrayManager.ResetUI();
    }
    #endregion

    #region Insertion Sort
    IEnumerator InsertionSort()
    {
        var array = ArrayManager.Array();
        int len = array.Length;

        for(int i=1; i<len; i++)
        {
            int key = array[i];
            int j = i-1;

            while(j>=0 && array[j]>key)
            {
                array[j+1] = array[j];
                j--;

                if(!Master.Instant)
                {
                    ArrayManager.ChangeColorOfNumber(j+1, "red");
                    yield return new WaitForSeconds(Master.StepLength);
                }
            }

            array[j+1] = key;
        }

        ArrayManager.ResetUI();
    }
    #endregion

    #region Merge Sort
    IEnumerator MergeSort()
    {
        var len = ArrayManager.Array().Length;

        Sort(0, len-1);
        ArrayManager.ResetUI();

        yield return null;
    }

    void Sort(int left, int right)
    {
        if(left < right)
        {
            int mid = (left+right) / 2;

            Sort(left,  mid);
            Sort(mid+1, right);

            Merge(left, mid, right);
        }
    }

    void Merge(int left, int mid, int right)
    {
        var array = ArrayManager.Array();

        var L  = new int[(mid - left + 1)];
        var R  = new int[(right - mid)];

        for(int x=0; x<L.Length; x++)  L[x] = array[left + x];
        for(int x=0; x<R.Length; x++)  R[x] = array[mid  + x + 1];

        int iLeft  = 0;
        int iRight = 0;
        int k = left;

        while(iLeft<L.Length && iRight<R.Length)
        {
            if(L[iLeft] <= R[iRight])
            {
                array[k] = L[iLeft];
                iLeft++;
            }
            else
            {
                array[k] = R[iRight];
                iRight++;
            }

            k++;
        }
        while(iLeft<L.Length)
        {
            array[k] = L[iLeft];
            iLeft++;
            k++;
        }
        while(iRight<R.Length)
        {
            array[k] = R[iRight];
            iRight++;
            k++;
        }
    }
    #endregion
}