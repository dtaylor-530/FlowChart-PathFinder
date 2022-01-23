using System.Collections.Generic;
using System.Windows.Media;

namespace AnimationPathWpf
{
    public static class RandomBrush
    {
        static RandomBrush()
        {
            enumerator = BrushHelper.GetBrushes().GetEnumerator();
        }

        public static Brush Brush
        {
            get { enumerator.MoveNext(); return enumerator.Current; }
        }

        private static IEnumerator<Brush> enumerator;
    }

    public static class BrushHelper
    {
        public static IEnumerable<Brush> GetBrushes()
        {
            var props = typeof(Brushes).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            foreach (var propInfo in props)
            {
                yield return (Brush)propInfo.GetValue(null, null);
            }
        }
    }
}