# Theseus
_Graphical maze solver_

## Available Commands
```
help        Shows help information.     Usage: help [subcommand]
solve       Solves a maze image.        Usage: solve <sourceImageFile.(bmp|png|jpg)> <outputImageFile.(bmp|png|jpg)>
clean       Cleans a maze image.        Usage: clean <sourceImageFile.(bmp|png|jpg)> <outputImageFile.(bmp|png|jpg)>
```

## Notes
- Default maze solver is "shortest path".
- Default maze color palette are black walls, red start, blue finish, and green solution, with white as empty space
when cleaning.
- Maze cleaning sets each pixel to the closest palette value.
