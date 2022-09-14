
# About Chantry

Chantry is an application to help with creating a LEGO(R) mosaic from a given image.

## Features

### Current Features

- Load and save the current mosaic configuration as a "project"
- Basic filtering:
	- Brightness / Contrast
	- Multiply
	- Saturation
- Add, remove, enable and disable filters
- Dithering selection:
	- Floyd-Steinberg
	- Bayer Matrix
	- Nearest Colour
- Specify the baseplate and element to use
- Supports up to 20x20 baseplate mosaics
- Select the element colours to use
- Very fast mosaic generation and rendering
- Export to LDRAW format

### Planned Features

- Re-order filters
- Filter grouping so they can be enabled and disabled in bulk
- Save filter settings so you can toggle between different configurations to see which ones generate the best mosaic
- Manually modify the generated mosaic
- Custom palettes of colours to quickly limit to what you have
- Configure the available colours
- Export to other formats

## Running the project

Chantry is built as a single self-contained executable for Windows and MacOS - no installation required.

## Technology

Chantry is written in C# (.NET6) and uses AvaloniaUI as the GUI framework.
