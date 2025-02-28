﻿#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WRect = Windows.Foundation.Rect;
using WSize = Windows.Foundation.Size;

namespace Microsoft.Maui.Controls.Handlers.Compatibility
{
	public abstract partial class VisualElementRenderer<TElement, TNativeElement>
		: Panel, IDisposable
		where TElement : VisualElement
		where TNativeElement : FrameworkElement
	{
		TNativeElement? _nativeView;
		public FrameworkElement ContainerElement => this;

		public TNativeElement? Control => ((IElementHandler)this).NativeView as TNativeElement ?? _nativeView;
		object? IElementHandler.NativeView => _nativeView;

		public UIElement? GetNativeElement() => Control;

		protected virtual void UpdateNativeControl() { }

		protected void SetNativeControl(TNativeElement control)
		{
			TNativeElement? oldControl = Control;
			_nativeView = control;

			if (oldControl != null)
			{
				Children.Remove(oldControl);
			}

			if (Control == null)
			{
				return;
			}

			Control.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
			Control.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Stretch;

			Children.Add(control);
			UpdateNativeControl();
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		protected override WSize MeasureOverride(global::Windows.Foundation.Size availableSize)
		{
			if (Element == null || availableSize.Width * availableSize.Height == 0)
				return new WSize(0, 0);

			if (Control != null)
			{
				Control.Measure(availableSize);
			}

			var mauiContext = Element?.Handler?.MauiContext;
			var minimumSize = MinimumSize();
			var mauiRect = Control?.DesiredSize ?? minimumSize.ToNative();
			
			if (Element is not IVisualTreeElement vte || mauiContext == null)
				return mauiRect;

			var width = Math.Max(mauiRect.Width, minimumSize.Width);
			var height = Math.Max(mauiRect.Height, minimumSize.Height);

			foreach (var child in vte.GetVisualChildren())
			{
				if (child is Maui.IElement childElement && childElement.Handler is INativeViewHandler nvh)
				{
					var size = nvh.GetDesiredSizeFromHandler(availableSize.Width, availableSize.Height);
					height = Math.Max(height, size.Height);
					width = Math.Max(width, size.Width);
				}
			}

			return new WSize(width, height);
		}

		protected override WSize ArrangeOverride(global::Windows.Foundation.Size finalSize)
		{
			var myRect = new WRect(0, 0, finalSize.Width, finalSize.Height);
			if (Control != null)
			{
				Control.Arrange(myRect);
			}

			var mauiContext = Element?.Handler?.MauiContext;
			if (Element is not IVisualTreeElement vte || mauiContext == null)
				return finalSize;

			var mauiRect = new Graphics.Rectangle(0, 0, finalSize.Width, finalSize.Height);
			foreach (var child in vte.GetVisualChildren())
			{
				if (child is Maui.IElement childElement && childElement.Handler is INativeViewHandler nvh)
					nvh.NativeArrangeHandler(mauiRect);
			}

			return finalSize;
		}

		void IDisposable.Dispose()
		{
			(this as IElementHandler).DisconnectHandler();
			Dispose(true);
		}

		protected virtual void SetAutomationPropertiesLabeledBy()
		{
			if (Element != null)
				VisualElement.MapAutomationPropertiesLabeledBy(this, Element);
		}

		protected virtual void SetAutomationPropertiesHelpText()
		{
			if (Element != null)
				VisualElement.MapAutomationPropertiesHelpText(this, Element);
		}


		protected virtual void SetAutomationPropertiesName()
		{
			if (Element != null)
				VisualElement.MapAutomationPropertiesName(this, Element);
		}

		static partial void ProcessAutoPackage(Maui.IElement element)
		{
			if (element.Handler is not INativeViewHandler nvh ||
				nvh.ContainerView is not Panel panel)
			{
				return;
			}

			panel.Children.Clear();

			if (element is not IVisualTreeElement vte)
				return;

			var mauiContext = element?.Handler?.MauiContext;
			if (mauiContext == null)
				return;

			foreach (var child in vte.GetVisualChildren())
			{
				if (child is Maui.IElement childElement)
					panel.Children.Add(childElement.ToPlatform(mauiContext));
			}
		}

		public static void MapAutomationPropertiesLabeledBy(INativeViewHandler handler, TElement view)
		{
			if (handler is VisualElementRenderer<TElement, TNativeElement> ver)
				ver.SetAutomationPropertiesLabeledBy();
		}

		public static void MapAutomationPropertiesHelpText(INativeViewHandler handler, TElement view)
		{
			if (handler is VisualElementRenderer<TElement, TNativeElement> ver)
				ver.SetAutomationPropertiesHelpText();
		}

		public static void MapAutomationPropertiesName(INativeViewHandler handler, TElement view)
		{
			if (handler is VisualElementRenderer<TElement, TNativeElement> ver)
				ver.SetAutomationPropertiesName();
		}
	}
}