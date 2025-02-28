#nullable enable
using System;

namespace Microsoft.Maui
{
	/// <include file="../../docs/Microsoft.Maui/Font.xml" path="Type[@FullName='Microsoft.Maui.Font']/Docs" />
	public readonly struct Font : IEquatable<Font>
	{
		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='Family']/Docs" />
		public string? Family { get; }

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='Size']/Docs" />
		public double Size { get; }

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='Slant']/Docs" />
		public FontSlant Slant { get; }

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='IsDefault']/Docs" />
		public bool IsDefault => Family == null && (Size == 0 || Size == double.NaN) && Slant == FontSlant.Default && Weight == FontWeight.Regular;

		static Font _default = default(Font).WithWeight(FontWeight.Regular);
		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='Default']/Docs" />
		public static Font Default => _default;

		readonly FontWeight _weight;

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='Weight']/Docs" />
		public FontWeight Weight
		{
			get => _weight <= 0 ? FontWeight.Regular : _weight;
		}

		private Font(string? family, double size, FontSlant slant, FontWeight weight, bool enableScaling) : this()
		{
			Family = family;
			Size = size;
			Slant = slant;
			_weight = weight;
			_disableScaling = !enableScaling;
		}


		readonly bool _disableScaling;
		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='AutoScalingEnabled']/Docs" />
		public bool AutoScalingEnabled
		{
			get => !_disableScaling;
		}

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='WithAutoScaling']/Docs" />
		public Font WithAutoScaling(bool enabled)
		{
			return new Font(Family, Size, Slant, Weight, enabled);
		}

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='WithSize'][0]/Docs" />
		public Font WithSize(double size)
		{
			return new Font(Family, size, Slant, Weight, AutoScalingEnabled);
		}

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='WithSlant']/Docs" />
		public Font WithSlant(FontSlant fontSlant)
		{
			return new Font(Family, Size, fontSlant, Weight, AutoScalingEnabled);
		}

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='WithWeight']/Docs" />
		public Font WithWeight(FontWeight weight)
		{
			return new Font(Family, Size, Slant, weight, AutoScalingEnabled);
		}

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='WithWeight']/Docs" />
		public Font WithWeight(FontWeight weight, FontSlant fontSlant)
		{
			return new Font(Family, Size, fontSlant, weight, AutoScalingEnabled);
		}

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='OfSize']/Docs" />
		public static Font OfSize(string? name, double size, FontWeight weight = FontWeight.Regular, FontSlant fontSlant = FontSlant.Default, bool enableScaling = true) =>
			new(name, size, fontSlant, weight, enableScaling);

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='SystemFontOfSize']/Docs" />
		public static Font SystemFontOfSize(double size, FontWeight weight = FontWeight.Regular, FontSlant fontSlant = FontSlant.Default, bool enableScaling = true) =>
			new(null, size, fontSlant, weight, enableScaling);

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='SystemFontOfWeight']/Docs" />
		public static Font SystemFontOfWeight(FontWeight weight, FontSlant fontSlant = FontSlant.Default, bool enableScaling = true) =>
			new(null, default(double), fontSlant, weight, enableScaling);

		bool Equals(Font other)
		{
			return string.Equals(Family, other.Family)
				&& Size.Equals(other.Size)
				&& Weight == other.Weight
				&& Slant == other.Slant
				&& AutoScalingEnabled == other.AutoScalingEnabled;
		}

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='Equals']/Docs" />
		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (obj.GetType() != GetType())
			{
				return false;
			}
			return Equals((Font)obj);
		}

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='GetHashCode']/Docs" />
		public override int GetHashCode() => (Family, Size, Weight, Slant, AutoScalingEnabled).GetHashCode();

		public static bool operator ==(Font left, Font right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Font left, Font right)
		{
			return !left.Equals(right);
		}

		/// <include file="../../docs/Microsoft.Maui/Font.xml" path="//Member[@MemberName='ToString']/Docs" />
		public override string ToString()
			=> $"Family: {Family}, Size: {Size}, Weight: {Weight}, Slant: {Slant}, AutoScalingEnabled: {AutoScalingEnabled}";

		bool IEquatable<Font>.Equals(Font other)
		{
			return Equals(other);
		}
	}
}