using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClassicArrayExtension : MonoBehaviour
{

    public static T[] Add<T>(T[] arrayInput, T newElement)
    {
        T[] newArray = new T[arrayInput.Length + 1];
        newArray.CopyTo(arrayInput, arrayInput.Length - 1);
        newArray[newArray.Length - 1] = newElement;
        return newArray;
    }
    //Complexity O(n)
    public static T[] AddTo<T>(T[] arrayInput, int elementToBeRemovedIndex)
    {
        T[] newArray = new T[arrayInput.Length - 1];
        int index = 0;
        bool jumped = false;
        foreach (T element in arrayInput)
        {
            if (index == elementToBeRemovedIndex && jumped == false)
            {
                jumped = true;
            }
            else
            {
                newArray[index] = element;
            }
        }
        return newArray;
    }

    //Complexity O(n)
    public static T[] RemoveFrom<T>(T[] arrayInput, int removeFromIndex)
    {
        T[] newArray = new T[arrayInput.Length -1];
        int index = 0;
        foreach (T element in arrayInput)
        {
            if (index != removeFromIndex)
            {
                newArray[index] = element;
                index++;
            }
            
        }
        return newArray;
    }

    //Complexity O(n)
    public static T[] Resize<T>(T[] arrayInput, int newSize)
    {
        T[] newArray = new T[newSize];
        for (int i = 0; i < arrayInput.Length && i < newArray.Length; i++)
        {
            newArray[i] = arrayInput[i];
        }
        for (int i = arrayInput.Length; i < newArray.Length; i++)
        {
            if(arrayInput.Length > 0)
            {
                newArray[i] = arrayInput[arrayInput.Length - 1];
            }
            
        }
        return newArray;
    }

    //Complexity O(n^2)
    public static T[] Sort<T>(T[] arrayInput, Func<T, T, bool> function)
    {
        T[] newArray = new T[arrayInput.Length];
        int index = 0;
        T prevElement = default(T);
        foreach (T element in arrayInput)
        {
            bool added = false;
            for (int i = 0; i < index; i++)
            {
                if (added)
                {
                    prevElement = newArray[i + 1];
                    newArray[i + 1] = prevElement;
                }

                if (function(newArray[i], element))
                {
                    prevElement = newArray[i];
                    newArray[i] = element;
                    newArray[i + 1] = prevElement;
                    added = true;
                }
            }

            if (!added)
            {
                newArray[index] = element;
            }
            index++;
        }
        return newArray;
    }
}
