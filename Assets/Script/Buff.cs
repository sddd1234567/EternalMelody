using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff{
        public int species;
        public float numerical;
        public int duration;
    public Buff(int kind, float value, int buffDuration)
    {
        species = kind;
        numerical = value;
        duration = buffDuration;
    }
}
