using GeometryLibrary;
using System.Windows;

namespace PathFinderLibrary
{
    public interface IConnectionPoint
    {
        Point Position { get; set; }

        Side Side { get; set; }
    }
}