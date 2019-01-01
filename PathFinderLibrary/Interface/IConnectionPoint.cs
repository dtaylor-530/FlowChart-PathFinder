using GeometryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace PathFinderLibrary
{

    public interface IConnectionPoint
    {
        Point Position { get; set; }

        Side Side { get; set; }
    }

}

