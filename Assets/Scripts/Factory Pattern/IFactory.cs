using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory // interface untuk mengimplenetasikan factory
{
    GameObject FactoryMethod(int tag);
}

