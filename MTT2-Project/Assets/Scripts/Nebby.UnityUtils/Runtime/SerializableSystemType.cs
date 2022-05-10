using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Nebby.UnityUtils
{
    [Serializable]
    public struct SerializableSystemType
    {
        public SerializableSystemType(string typeName)
        {
            this._typeName = "";
            this._typeName = typeName;
        }
        public SerializableSystemType(Type type)
        {
            this._typeName = "";
            this.Type = type;
        }

        public string TypeName { get => _typeName; set => Type = Type.GetType(value); }

        public Type Type
        {
            get
            {
                if (_typeName == null)
                    return null;

                Type type = Type.GetType(_typeName);
                if (!(type != null))
                    return null;
                return type;
            }
            set
            {
                _typeName = ((value != null)) ? value.AssemblyQualifiedName : ""; 
            }
        }

        [SerializeField]
        private string _typeName;
    }
}