using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BeatThat.Properties{
	public interface IHasTexture : IHasValue<Texture> {}

	public interface IEditsTexture : IHasTexture, IHasValueChangedEvent {}

	public interface IHasMaterial : IHasValue<Material> {}

	public interface IHasText : IHasValue<string> {}

	public interface IHasColor : IHasValue<Color> {}

	public interface IHasColorBlock : IHasValue<ColorBlock> {}
		
	public interface IHasTextInput : IHasText, IHasValueChangedEvent<string>
	{
		void ActivateInput();

		UnityEvent<string> onSubmit { get; }
	}

	public interface IHasValue
	{
		object valueObj { get; }
	}

	public interface IHasValue<T> : IHasValue
	{
		T value { get; set; }
	}

	public interface IHasFloat : IHasValue<float> {}

	public interface IHasInt : IHasValue<int> {}

	public interface IHasLong : IHasValue<long> {}

	public interface IHasBool : IHasValue<bool> {}

	public interface IHasDateTime : IHasValue<DateTime> {}

	public interface IHasClick 
	{
		#if HAS_CLICK_LEGACY
		bool interactable { get; set; }
		[Obsolete("use UnityEvent onValueChanged")]event Action Clicked; // TODO: replace with UnityEvent
		#endif

		UnityEvent onClicked { get; }
	}

	public interface IHasValueObjChanged
	{
		UnityEvent onValueObjChanged { get; }
	}

	public interface IHasValueChangedEvent<T>
	{
		UnityEvent<T> onValueChanged { get; }
	}

	public interface IHasValueChangedEvent 
	{
		UnityEvent onValueChanged { get; }
	}

	public interface IHasBounds
	{
		Bounds bounds { get; }
	}

	public interface IHasRect 
	{
		Rect rect { get; }
	}

	public interface IHasUVRect 
	{
		Rect uvRect { get; set; }
	}

	public interface IEditsBool : IHasBool, IHasValueChangedEvent
	{
	}


	public interface IDrive 
	{
		object GetDrivenObject();

		/// <summary>
		/// Used when loop is detected
		/// </summary>
		/// <returns><c>true</c>, if driven was cleared, <c>false</c> otherwise.</returns>
		bool ClearDriven();
	}

	/// <summary>
	/// A way for a driver to make discoverable that it drives another component, 
	/// e.g. CurvesFloat implements IDrive<IHasFloat>
	/// </summary>
	public interface IDrive<T> : IDrive where T : class
	{
		T driven { get; }
	}

}