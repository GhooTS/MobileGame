﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;

public class DynamicCollection<T> : DynamicCollectionBase
    where T : MonoBehaviour
{
    protected readonly List<T> elements = new List<T>();
    public ReadOnlyCollection<T> Elements { get { return elements.AsReadOnly(); } }

    public int Count { get { return elements.Count; } }

    public T this[int index]
    {
        get { return elements[index]; }
    }

    private void OnDisable()
    {
        Assert.IsTrue(Count == 0);
        elements.Clear();
    }

    public override bool Subscribe(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out T subscriber) && !Contain(subscriber))
        {
            elements.Add(subscriber);
            return true;
        }

        return false;
    }

    public bool Subscribe(T subscriber)
    {
        if (!Contain(subscriber))
        {
            elements.Add(subscriber);
            return true;
        }

        return false;
    }

    public override bool Unsubscribe(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out T subscriber))
        {
            return elements.Remove(subscriber);
        }

        return false;
    }

    public bool Unsubscribe(T subscriber)
    {
        return elements.Remove(subscriber);
    }

    public bool Contain(T element)
    {
        return elements.Contains(element);
    }

}
