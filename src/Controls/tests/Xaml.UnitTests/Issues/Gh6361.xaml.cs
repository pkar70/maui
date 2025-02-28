using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Core.UnitTests;
using NUnit.Framework;

namespace Microsoft.Maui.Controls.Xaml.UnitTests
{
	public partial class Gh6361 : ContentPage
	{
		public Gh6361() => InitializeComponent();
		public Gh6361(bool useCompiledXaml)
		{
			//this stub will be replaced at compile time
		}

		[TestFixture]
		class Tests
		{
			[SetUp]
			public void Setup()
			{
				Device.PlatformServices = new MockPlatformServices();
			}

			[TearDown] public void TearDown() => Device.PlatformServices = null;

			[Test]
			public void CSSBorderRadiusDoesNotFail([Values(false, true)] bool useCompiledXaml)
			{
				var layout = new Gh6361(useCompiledXaml);
			}
		}
	}
}
