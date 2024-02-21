using System.IO.Hashing;
using System.Runtime.InteropServices;
using System.Text.Json;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using Avalonia.Platform;

namespace InkForge.Desktop.Controls;

public class FluentSymbolIcon : IconElement
{
	public static readonly StyledProperty<int> IconSizeProperty
		= AvaloniaProperty.Register<FluentSymbolIcon, int>(nameof(IconSize), defaultValue: 20);
	public static readonly StyledProperty<FontIconStyle> IconStyleProperty
		= AvaloniaProperty.Register<FluentSymbolIcon, FontIconStyle>(nameof(IconStyle));
	public static readonly StyledProperty<string> SymbolProperty
		= AvaloniaProperty.Register<FluentSymbolIcon, string>(nameof(Symbol));
	private static readonly Dictionary<(FontIconStyle, uint Key), string> _glyphCache = [];
	private static readonly Dictionary<FontIconStyle, FontFamily> _iconFonts = [];
	private TextLayout? _textLayout;

	public int IconSize
	{
		get => GetValue(IconSizeProperty);
		set => SetValue(IconSizeProperty, value);
	}

	public FontIconStyle IconStyle
	{
		get => GetValue(IconStyleProperty);
		set => SetValue(IconStyleProperty, value);
	}

	public string Symbol
	{
		get => GetValue(SymbolProperty);
		set => SetValue(SymbolProperty, value);
	}

	static FluentSymbolIcon()
	{
		AffectsMeasure<FluentSymbolIcon>([
			IconStyleProperty,
			SymbolProperty,
		]);
	}

	public override void Render(DrawingContext context)
	{
		_textLayout ??= GenerateText();

		var dstRect = new Rect(Bounds.Size);
		using (context.PushClip(dstRect))
		{
			var pt = new Point(dstRect.Center.X - _textLayout.Width / 2,
				dstRect.Center.Y - _textLayout.Height / 2);
			_textLayout.Draw(context, pt);
		}
	}

	protected override Size MeasureOverride(Size availableSize)
	{
		_textLayout ??= GenerateText();

		return new Size(_textLayout.Width, _textLayout.Height);
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
	{
		base.OnPropertyChanged(change);

		switch (change.Property.Name)
		{
			case nameof(IconSize):
			case nameof(IconStyle):
			case nameof(Symbol):
				InvalidateSymbolLayout();
				break;
		}
	}

	protected override void OnMeasureInvalidated()
	{
		_textLayout?.Dispose();
		_textLayout = null;

		base.OnMeasureInvalidated();
	}

	private TextLayout GenerateText()
	{
		var glyph = GetIconGlyph(this);

		if (!_iconFonts.TryGetValue(IconStyle, out var fontFamily))
		{
			_iconFonts[IconStyle] = fontFamily = new FontFamily($"avares://InkForge/Assets/Fonts#FluentSystemIcons-{IconStyle}");
		}

		return new TextLayout(glyph, new Typeface(fontFamily), FontSize, Foreground, TextAlignment.Left);
	}

	private void InvalidateSymbolLayout()
	{
		InvalidateMeasure();
	}

	private static string GetIconGlyph(FluentSymbolIcon icon)
	{
		ReadOnlySpan<char> glyphKey = $"ic_fluent_{icon.Symbol}_{icon.IconSize:0}";
		var hash = Hash(glyphKey, icon.IconStyle);
		if (!_glyphCache.TryGetValue((icon.IconStyle, hash), out var glyph))
		{
			glyph = LoadIcons(icon.IconStyle, hash);
		}

		if (string.IsNullOrWhiteSpace(glyph))
		{
			return string.Empty;
		}

		return glyph;
	}

	private static uint Hash(ReadOnlySpan<char> key, FontIconStyle iconStyle)
	{
		return XxHash32.HashToUInt32(MemoryMarshal.AsBytes(key), (int)iconStyle);
	}

	private static string LoadIcons(FontIconStyle iconStyle, uint key)
	{
		Optional<string>? glyph = Optional<string>.Empty;
		using (var stream = AssetLoader.Open(new($"avares://InkForge/Assets/Fonts/FluentSystemIcons-{iconStyle}.json")))
		using (var document = JsonDocument.Parse(stream))
		{
			foreach (var element in document.RootElement.EnumerateObject())
			{
				if (element.Value.ValueKind is not JsonValueKind.Number)
				{
					continue;
				}

				var typeSeparator = element.Name.LastIndexOf('_');
				if (typeSeparator == -1)
				{
					continue;
				}

				ReadOnlySpan<char> elementKey = element.Name.AsSpan(0, typeSeparator);
				var hash = Hash(elementKey, iconStyle);
				var elementGlyph = char.ConvertFromUtf32(element.Value.GetInt32())!;
				if (hash == key)
				{
					glyph = glyph switch
					{
						{ HasValue: false } => elementGlyph,
						_ => default(Optional<string>?)
					};
				}

				_glyphCache[(iconStyle, hash)] = elementGlyph;
			}
		}

		return glyph?.GetValueOrDefault() ?? string.Empty;
	}

	public enum FontIconStyle
	{
		Regular, Filled
	}
}
