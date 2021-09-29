using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command // base untuk class command
{
    public abstract void Execute();
    public abstract void UnExecute();
}
