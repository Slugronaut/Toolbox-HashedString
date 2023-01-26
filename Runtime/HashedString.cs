using UnityEngine;

namespace Toolbox
{
    /// <summary>
    /// A datatype that represents a string and its associated hash value.
    /// The hash value is generated using the Unity.Animator component.
    /// </summary>
    [System.Serializable]
    public sealed class HashedString
    {
        [HideInInspector]
        [SerializeField]
        string _Value;
        [Sirenix.OdinInspector.ShowInInspector]
        public string Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                StaleHash = true;
            }
        }

        bool StaleHash = true;
        int _Hash;
        [Sirenix.OdinInspector.ShowInInspector]
        [Sirenix.OdinInspector.ReadOnly]
        public bool NoValue
        {
            get { return string.IsNullOrEmpty(Value); }
        }

        public int Hash
        {
            get
            {
                #if UNITY_EDITOR
                if(!Application.isPlaying)
                    _Hash = Animator.StringToHash(_Value);
                else
                {
                    if (StaleHash)
                    {
                        _Hash = Animator.StringToHash(_Value);
                        StaleHash = false;
                    }
                }
                #else
                if (StaleHash)
                {
                    _Hash = Animator.StringToHash(_Value);
                    StaleHash = false;
                }
                #endif
                return _Hash;
            }
        }

        public HashedString()
        {

        }

        public HashedString(string str)
        {
            Value = str;
        }

        public static implicit operator string(HashedString hs)
        {
            return hs._Value;
        }

        public static int StringToHash(string name)
        {
            return Animator.StringToHash(name);
        }

        /// <summary>
        /// Returns true if the given hash value exists within the list of HashedStrings.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Contains(HashedString[] list, int value)
        {
            for(int i = 0; i < list.Length; i++)
            {
                if (list[i].Hash == value) return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the given hash value doesn't exist within the list of HashedStrings.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool DoNotContain(HashedString[] list, int value)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Hash == value) return false;
            }
            return true;
        }
    }
}
