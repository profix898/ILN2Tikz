using System.Text.RegularExpressions;
using AnyAscii;

namespace TikzExport.Generator;

public static class TikzTextUtility
{
    public static string EscapeText(string input)
    {
        // Replace greek letters
        input = input.Replace("α", @"${\alpha}$");
        input = input.Replace("β", @"${\beta}$");
        input = input.Replace("γ", @"${\gamma}$");
        input = input.Replace("Γ", @"${\Gamma}$");
        input = input.Replace("δ", @"${\delta}$");
        input = input.Replace("Δ", @"${\Delta}$");
        input = input.Replace("ϵ", @"${\epsilon}$");
        input = input.Replace("ε", @"${\varepsilon}$");
        input = input.Replace("ζ", @"${\zeta}$");
        input = input.Replace("η", @"${\eta}$");
        input = input.Replace("θ", @"${\theta}$");
        input = input.Replace("ϑ", @"${\vartheta}$");
        input = input.Replace("Θ", @"${\Theta}$");
        input = input.Replace("ι", @"${\iota}$");
        input = input.Replace("κ", @"${\kappa}$");
        input = input.Replace("ϰ", @"${\kappa}$");
        input = input.Replace("λ", @"${\lambda}$");
        input = input.Replace("Λ", @"${\Lambda}$");
        input = input.Replace("μ", @"${\mu}$");
        input = input.Replace("µ", @"${\mu}$"); // micro sign
        input = input.Replace("ν", @"${\nu}$");
        input = input.Replace("ξ", @"${\xi}$");
        input = input.Replace("π", @"${\pi}$");
        input = input.Replace("Π", @"${\Pi}$");
        input = input.Replace("ρ", @"${\rho}$");
        input = input.Replace("ϱ", @"${\varrho}$");
        input = input.Replace("σ", @"${\sigma}$");
        input = input.Replace("ς", @"${\sigma}$");
        input = input.Replace("Σ", @"${\Sigma}$");
        input = input.Replace("τ", @"${\tau}$");
        input = input.Replace("υ", @"${\upsilon}$");
        input = input.Replace("Υ", @"${\Upsilon}$");
        input = input.Replace("ϕ", @"${\phi}$");
        input = input.Replace("φ", @"${\varphi}$");
        input = input.Replace("Φ", @"${\Phi}$");
        input = input.Replace("χ", @"${\chi}$");
        input = input.Replace("ψ", @"${\psi}$");
        input = input.Replace("Ψ", @"${\Psi}$");
        input = input.Replace("ω", @"${\omega}$");
        input = input.Replace("Ω", @"${\Omega}$");

        // Operations / Relations
        input = input.Replace("±", @"${\pm}$");
        input = input.Replace("∓", @"${\mp}$");
        input = input.Replace("≈", @"${\approx}$");
        input = input.Replace("∼", @"${\sim}$");
        input = input.Replace("≅", @"${\cong}$");
        input = input.Replace("≠", @"${\neq}$");
        input = input.Replace("⊕", @"${\oplus}$");
        input = input.Replace("×", @"${\times}$");
        input = input.Replace("∇", @"${\nabla}$");

        // Arrows
        input = input.Replace("→", @"${\rightarrow}$");
        input = input.Replace("←", @"${\leftarrow}$");
        input = input.Replace("⇒", @"${\Rightarrow}$");
        input = input.Replace("⇐", @"${\Leftarrow}$");
        input = input.Replace("↔", @"${\leftrightarrow}$");
        input = input.Replace("⇔", @"${\Leftrightarrow}$");
        input = input.Replace("↦", @"${\mapsto}$");

        // Scientific notation
        input = Regex.Replace(input, @"([\d]+)\^([\{\}\d\+\-]+)", @"$${$1^{$2}}$$");

        // Detect basic math sequences "a_{a}" and "a^{b}"
        input = Regex.Replace(input, @"(.*)\b([\w\d]+_[\w\d]{1})\b(.*)", @"$1$$$2$$$3"); // "a_a"
        input = Regex.Replace(input, @"(.*)\b([\w\d]+\^[\w\d]{1})\b(.*)", @"$1$$$2$$$3"); // "a^a"
        input = Regex.Replace(input, @"([\w\d]+_\{[\w\d]+\})", @"$$$1$$"); // "a_{abc}"
        input = Regex.Replace(input, @"([\w\d]+\^\{[\w\d]+\})", @"$$$1$$"); // "a^{abc}"

        // Replace '{', '}', '_', '^' (if not inside math environment)
        input = Regex.Replace(input, @"(?!\B\$[^\$]*)([\{\}_\^])(?![^\$]*\$\B)", @"\$1"); // '{}_^'
        // Replace '\' (if not used for escaping above)
        input = Regex.Replace(input, @"\\(?=[^\{\}_\^])", @"\\"); // '\'
        // Revert double escaping of greek letters and math characters
        input = input.Replace(@"{\\", @"{\");
        // Join consecutive math environments
        input = Regex.Replace(input, @"\$([\^_\ \+\-\*\/]*)\$", @"$1");
        // Un-escape '\{' in math environment
        input = Regex.Replace(input, @"(?!\b\$[^\$]*)\\([\{\}]+)(?![^\$]*\$\b)", @"$1"); // '{}'

        // ASCII transliteration (replace all remaining non-ascii characters)
        input = input.Transliterate();

        return input;
    }
}