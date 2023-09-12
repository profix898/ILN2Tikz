ILN2Tikz
==========
[![Nuget](https://img.shields.io/nuget/v/ILN2Tikz?style=flat-square&logo=nuget&color=blue)](https://www.nuget.org/packages/ILN2Tikz)

Export functionality for ILNumerics (http://ilnumerics.net/) scene graphs
and plot cubes to TikZ/PGFPlots (LaTeX graphics package, see [Wikipedia](https://en.wikipedia.org/wiki/PGF/TikZ)).

## How to use

Export scene to string:
```csharp
var tikzString = ILN2TikzExport.ExportString(scene);
```
or export scene to file (usually *.tikz):
```csharp
ILN2TikzExport.ExportFile(scene, filePath);
```

Both variants take an optional argument ```Size canvasSize``` (defaults to 120x100 mm^2), for example:
```csharp
ILN2TikzExport.ExportFile(scene, filePath, new Size(mmWidth, mmHeight));
```

As of today (Jan 2023) the following plot types are supported: _LinePlot_, _ErrorBarPlot (y-error only)_, _Surface_, _FastSurface_.

### License
ILN2Tikz is licensed under the terms of the MIT license (<http://opensource.org/licenses/MIT>, see LICENSE.txt).
