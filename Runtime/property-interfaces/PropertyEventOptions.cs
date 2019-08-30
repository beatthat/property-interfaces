using System;
using System.Collections.Generic;
using UnityEngine.Events;


namespace BeatThat.Properties{
	/// <summary>
	/// Specifies desired event behaviour when setting a property that has an onChanged event.
	/// 
	/// <c>SendOnChange</c> - sent the change event only if the new value is different from prev value
	/// <c>Disable</c> - don't sent the changed event
	/// <c>Force</c> - force send the change event
	/// </summary>
	public enum PropertyEventOptions 
	{
		SendOnChange = 0, 
		Disable = 1,
		Force = 2
	}

    public static class PropertyEventOptionsExt
    {
        public static bool Set<T>(this PropertyEventOptions opt, ref T prop, T val, Action changeEvent)
        {
            if (!SetThenShouldNotify(opt, ref prop, val))
            {
                return false;
            }
            changeEvent?.Invoke();
            return true;
        }

        public static bool Set<T>(this PropertyEventOptions opt, ref T prop, T val, Action<T> changeEvent)
        {
            if (!SetThenShouldNotify(opt, ref prop, val))
            {
                return false;
            }
            changeEvent?.Invoke(val);
            return true;
        }

        public static bool Set<T>(this PropertyEventOptions opt, ref T prop, T val, UnityEvent changeEvent)
        {
            if (!SetThenShouldNotify(opt, ref prop, val))
            {
                return false;
            }
            if (changeEvent != null)
            {
                changeEvent.Invoke();
            }
            return true;
        }

        public static bool Set<T>(this PropertyEventOptions opt, ref T prop, T val, UnityEvent<T> changeEvent)
        {
            if (!SetThenShouldNotify(opt, ref prop, val))
            {
                return false;
            }
            if (changeEvent != null)
            {
                changeEvent.Invoke(val);
            }
            return true;
        }

        private static bool SetThenShouldNotify<T>(this PropertyEventOptions opt, ref T prop, T val)
        {
            if (EqualityComparer<T>.Default.Equals(prop, val) && opt != PropertyEventOptions.Force)
            {
                return false;
            }
            prop = val;
            return opt != PropertyEventOptions.Disable;
        }
    }
}
