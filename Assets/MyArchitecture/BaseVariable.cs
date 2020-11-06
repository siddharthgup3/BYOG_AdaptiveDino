using System;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MyArchitecture
{
    public abstract class BaseVariable : BaseDataScriptableObject
    {
        public abstract bool IsClampable { get; }
        public abstract Type GetTypeOfField { get; }
        public abstract object BaseValue { get; set; }
    }

    public abstract class BaseVariable<T> : BaseVariable
    {
        [CanBeNull]
        public virtual T Value
        {
            get => _value;
            set => _value = SetValue(value);
        }
        
        [SerializeField] private bool isReadOnly;
        [SerializeField] protected T _value = default(T);
        [SerializeField] private bool raiseError;

        [SerializeField] protected bool isClamped = false;
        [SerializeField] [ShowIf("isClamped")] protected T minClampedValue = default(T);
        [SerializeField] [ShowIf("isClamped")] protected T maxClampedValue = default(T);
        
        public override bool IsClampable => false;
        public override Type GetTypeOfField => typeof(T);    

        protected virtual T SetValue(T value)
        {
            return value;
        }
        
        public override object BaseValue
        {
            get => _value;
            set => _value = SetValue((T)value);
        }

    }
}