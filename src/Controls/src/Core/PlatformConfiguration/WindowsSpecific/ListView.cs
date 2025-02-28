using System;

namespace Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific
{
	using FormsElement = Maui.Controls.ListView;

	/// <include file="../../../../docs/Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific/ListView.xml" path="Type[@FullName='Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific.ListView']/Docs" />
	public static class ListView
	{
		#region SelectionMode

		/// <include file="../../../../docs/Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific/ListView.xml" path="//Member[@MemberName='SelectionModeProperty']/Docs" />
		public static readonly BindableProperty SelectionModeProperty =
			BindableProperty.CreateAttached("WindowsSelectionMode", typeof(ListViewSelectionMode),
				typeof(ListView), ListViewSelectionMode.Accessible);

		/// <include file="../../../../docs/Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific/ListView.xml" path="//Member[@MemberName='GetSelectionMode'][0]/Docs" />
		public static ListViewSelectionMode GetSelectionMode(BindableObject element)
		{
			return (ListViewSelectionMode)element.GetValue(SelectionModeProperty);
		}

		/// <include file="../../../../docs/Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific/ListView.xml" path="//Member[@MemberName='SetSelectionMode'][0]/Docs" />
		public static void SetSelectionMode(BindableObject element, ListViewSelectionMode value)
		{
			element.SetValue(SelectionModeProperty, value);
		}

		/// <include file="../../../../docs/Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific/ListView.xml" path="//Member[@MemberName='GetSelectionMode']/Docs" />
		public static ListViewSelectionMode GetSelectionMode(this IPlatformElementConfiguration<Windows, FormsElement> config)
		{
			return (ListViewSelectionMode)config.Element.GetValue(SelectionModeProperty);
		}

		/// <include file="../../../../docs/Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific/ListView.xml" path="//Member[@MemberName='SetSelectionMode']/Docs" />
		public static IPlatformElementConfiguration<Windows, FormsElement> SetSelectionMode(
			this IPlatformElementConfiguration<Windows, FormsElement> config, ListViewSelectionMode value)
		{
			config.Element.SetValue(SelectionModeProperty, value);
			return config;
		}

		#endregion
	}

	/// <include file="../../../../docs/Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific/ListViewSelectionMode.xml" path="Type[@FullName='Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific.ListViewSelectionMode']/Docs" />
	public enum ListViewSelectionMode
	{
		/// <summary>
		/// Allows ListItems to have TapGestures. The Enter key and Narrator will not fire the ItemTapped event.
		/// </summary>
		Inaccessible,
		/// <summary>
		/// Allows the Enter key and Narrator to fire the ItemTapped event. ListItems cannot have TapGestures.
		/// </summary>
		Accessible
	}
}