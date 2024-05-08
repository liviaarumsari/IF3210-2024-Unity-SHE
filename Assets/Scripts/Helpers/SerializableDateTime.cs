using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDateTime
{
    public long binaryDateTime;

    public DateTime DateTime
    {
        get { return DateTime.FromBinary(binaryDateTime); }
        set { binaryDateTime = value.ToBinary(); }
    }
}
