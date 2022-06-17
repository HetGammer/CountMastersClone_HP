using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{
    public interface ICollectable 
    {
        void Collect(JetCharacter characterWhoCollected);
    }
}