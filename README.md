
# About Platescape

Platescape is an application to help with creating a LEGO(R) mosaic from a given image.

## Features

- Basic image filtering.
- Three dithering algorithms.
- Specify the baseplate and element to use.
- Supports up to 20x20 baseplate mosaics.
- Select the element colours to use.
- Export to PNG.

## How to Use

- Run the program and open an image file.
- Select a baseplate size and element to use.
- Select the size of the mosaic you want.
- Choose a dithering algorithm:
	- Nearest Colour:
		- Works well for images with solid colours.
	- Bayer Matrix:
		- Works well for greyscale images, or images with solid colours that don't exactly match LDRAW colours.
	- Floyd Steinberg:
		- Works well for photographs.
- Add and remove filters and adjust the parameters.
- Select the colours to use for the final mosaic.
- Export to PNG:
	- Select the number of pixels per stud to use.
	- Turn on or off drawing a circles represening each stud.
	- One file for the entire mosaic, or a file per baseplate.

## Development Info

- Platescape is written in C# (.NET 10) and uses the Avalonia UI framework.
- Builds for Mac and Windows.
	- Distributed as a self-contained portable executable.

