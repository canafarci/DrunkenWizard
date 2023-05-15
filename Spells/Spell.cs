using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public static event Action SpellCastedHandler;
    public virtual void Cast()
    {
        SpellCastedHandler.Invoke();
    }
}
