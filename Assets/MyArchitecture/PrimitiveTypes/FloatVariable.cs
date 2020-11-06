using System;
using UnityEngine;

namespace MyArchitecture
{
    [CreateAssetMenu(fileName = "new FloatVariable", menuName = "SO/Types/Float", order = 2)]
    public class FloatVariable : BaseVariable<float>
    {
        public override object BaseValue { get; set; }
        public override bool IsClampable => true;
    }
}