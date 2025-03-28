TikzExport
==========
[![Nuget](https://img.shields.io/nuget/v/TikzExport?style=flat-square&logo=nuget&color=blue)](https://www.nuget.org/packages/ILNumerics.Community.TikzExport)

Export functionality for ILNumerics (http://ilnumerics.net/) scene graphs
and plot cubes to TikZ/PGFPlots (LaTeX graphics package, see [Wikipedia](https://en.wikipedia.org/wiki/PGF/TikZ)).

## How to use

Export scene to string:
```csharp
var tikzString = TikzExport.ExportString(scene);
```
or export scene to file (usually *.tikz):
```csharp
TikzExport.ExportFile(scene, filePath);
```

Both variants take an optional argument ```Size canvasSize``` (defaults to 120x100 mm^2), for example:
```csharp
TikzExport.ExportFile(scene, filePath, new Size(mmWidth, mmHeight));
```

As of today (March 2025) the following plot types are supported: _LinePlot_, _ErrorBarPlot (y-error only)_, _Surface_, _FastSurface_.

### License
TikzExport is licensed under the terms of the MIT license (<http://opensource.org/licenses/MIT>, see LICENSE.txt).
