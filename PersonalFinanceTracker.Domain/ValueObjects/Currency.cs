using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace PersonalFinanceTracker.Domain.ValueObjects
{
	public sealed partial record Currency
	{
		public string Code { get; }

		private Currency(string code) => Code = code;

		public static Currency USD => new("USD");
		public static Currency EUR => new("EUR");
		public static Currency UAH => new("UAH");
		public static IReadOnlyCollection<Currency> All { get; } = new[]
		{
			USD, EUR, UAH
		};

		[GeneratedRegex("^[A-Z]{3}$", RegexOptions.CultureInvariant)]
		private static partial Regex Iso4217Regex();

		public static bool TryParse(string? code, [NotNullWhen(true)] out Currency? currency)
		{
			currency = null;
			if (string.IsNullOrWhiteSpace(code)) return false;

			var normalized = code.Trim().ToUpperInvariant();

			if (!Iso4217Regex().IsMatch(normalized)) return false;

			currency = normalized switch
			{
				"UAH" => UAH,
				"USD" => USD,
				"EUR" => EUR,
				_ => new Currency(normalized)
			};

			return true;
		}

		public static Currency Parse(string code)
			=> TryParse(code, out var currency)
				? currency!
				: throw new ArgumentException(
					$"Invalid currency code: '{code}'. Expected ISO 4217 alpha-3 (e.g., UAH, USD, EUR).",
					nameof(code));

		public override string ToString() => Code;
	}
}
