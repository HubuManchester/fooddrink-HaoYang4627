using System.Runtime.CompilerServices;

namespace MyFoodTracker.Services;

public static class AccessibilityService
{
    private const double Scale = 1.25;
    private static readonly ConditionalWeakTable<BindableObject, FontSizeHolder> _originalSizes = new();
    public static bool LargeTextEnabled { get; set; }

    public static void ApplyFontScale(Element root)
    {
        ApplyToElement(root);
        if (root is IVisualTreeElement visualTree)
            foreach (var child in visualTree.GetVisualChildren().OfType<Element>())
                ApplyFontScale(child);
    }

    private static void ApplyToElement(Element element)
    {
        double factor = LargeTextEnabled ? Scale : 1.0;
        switch (element)
        {
            case Label l:
                l.FontSize = GetOriginal(l, l.FontSize) * factor;
                break;
            case Button b:
                b.FontSize = GetOriginal(b, b.FontSize) * factor;
                break;
            case Entry e:
                e.FontSize = GetOriginal(e, e.FontSize) * factor;
                break;
            case Editor ed:
                ed.FontSize = GetOriginal(ed, ed.FontSize) * factor;
                break;
            case Picker p:
                p.FontSize = GetOriginal(p, p.FontSize) * factor;
                break;
            case SearchBar sb:
                sb.FontSize = GetOriginal(sb, sb.FontSize) * factor;
                break;
        }
    }

    private static double GetOriginal(BindableObject control, double current)
    {
        var holder = _originalSizes.GetOrCreateValue(control);
        if (holder.Value == 0)
        {
            holder.Value = current > 0 ? current : 14;
        }
        return holder.Value;
    }

    private class FontSizeHolder
    {
        public double Value { get; set; }
    }
}