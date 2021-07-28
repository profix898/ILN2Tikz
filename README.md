ILN2Tikz Export
==========

Export functionality for ILNumerics (http://ilnumerics.net/) scene graphs
and plot cubes to TikZ (LaTeX graphics package, see [Wikipedia](https://en.wikipedia.org/wiki/PGF/TikZ)).

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

As of today (July 2021) only _LinePlot_ and _Surface_ are supported.

### License
ILN2Tikz is licensed under the terms of the MIT license (<http://opensource.org/licenses/MIT>, see LICENSE.txt).
