using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructable
{
    void OnDestruction(GameObject destroyer);
}
