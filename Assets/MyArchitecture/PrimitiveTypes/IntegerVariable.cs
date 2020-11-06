using System;
using UnityEngine;

namespace MyArchitecture
{
    [CreateAssetMenu(fileName = "new IntegerVariable", menuName = "SO/Types/Integer", order = 1)]
    public class IntegerVariable : BaseVariable<int>
    {
        public override Type GetTypeOfField { get; }
        public override object BaseValue { get; set; }

        public override bool IsClampable => true;
    }
}