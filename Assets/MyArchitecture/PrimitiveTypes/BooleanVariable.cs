using System;
using UnityEngine;

namespace MyArchitecture
{
    [CreateAssetMenu(fileName = "new BooleanVariable", menuName = "SO/Types/Bool", order = 0)]
    public class BooleanVariable : BaseVariable<bool>
    {
    }
}