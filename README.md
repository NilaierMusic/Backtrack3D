# VisualBacktrack3D for R.E.P.O

A purely client-sided BepInEx mod for R.E.P.O that adds 3D visual representations (spheres) in the game world corresponding to the 2D backtrack points shown on the in-game map.

This helps players visualize their tracked path and navigate previously visited areas more easily by providing landmarks directly within the game environment.

## Showcase

![VisualBacktrack Demo](link_to_your_gif_or_a_screenshot.png)

## Features

*   **3D Backtrack Points:** Renders visible spheres in the 3D game world at the locations corresponding to the 2D map backtrack points.
*   **Visual Navigation Aid:** Helps orient yourself and follow your tracked path directly within the game environment.
*   **Synced Animation:** Sphere animations (scaling effect) can mirror the 2D map points' appearance animation.
*   **Customizable:** Configure appearance (color, opacity, shadows) and animation behavior via a configuration file.

## Requirements

*   [R.E.P.O](https://store.steampowered.com/app/3241660/REPO/) (The game itself)
*   [BepInEx pack](https://thunderstore.io/c/repo/p/BepInEx/BepInExPack/) for Unity games (ensure it's correctly installed for R.E.P.O)

## Installation

1.  Ensure you have BepInEx installed correctly for R.E.P.O. You typically need to run the game once with BepInEx installed to generate its folders.
2.  Download the latest zip containing the `Backtrack3D.dll` file.
3.  Place the `.dll` file inside your `R.E.P.O/BepInEx/plugins/` folder. If the `plugins` folder doesn't exist inside `BepInEx`, create it.
4.  Launch the game. The mod should now be active.

## Configuration

After running the game once with the mod installed, a configuration file will be created at:
`R.E.P.O/BepInEx/config/com.nilaier.visualbacktrack.cfg`

You can edit this file with a text editor to change the mod's settings:

*   **`[3D Visuals]` Section:**
    *   **`CastShadows`** (Type: `Boolean`, Default: `true`)
        *   Determines if the 3D spheres should cast shadows. Set to `true` to cast shadows, `false` to disable.
    *   **`BacktrackPointColor`** (Type: `Color`, Default: `White`)
        *   Sets the color of the 3D spheres. You can use standard Unity color names (like `White`, `Red`, `Blue`) or RGBA values as comma-separated strings (e.g., `1,0,0,1` for opaque red, `0,1,0,0.5` for semi-transparent green). The alpha component here is mostly superseded by the `Opacity` setting below for rendering purposes, but it's part of the Color structure.
    *   **`SyncAnimation`** (Type: `Boolean`, Default: `true`)
        *   If `true`, the 3D sphere's appearance animation (scaling up) will use the same timing (speed and curve) as the 2D map point's animation.
        *   If `false`, the animation will use a default ease-in-out curve and the `AnimationSpeed` setting below.
    *   **`AnimationSpeed`** (Type: `Single`, Default: `1.0`)
        *   Controls the speed of the sphere's appearance animation *only if* `SyncAnimation` is set to `false`. Higher values result in a faster animation.
    *   **`Opacity`** (Type: `Single`, Default: `0.75`)
        *   Sets the transparency of the 3D spheres. `1.0` is fully opaque, `0.0` is fully transparent. Values below `1.0` automatically configure the sphere's material for transparency.

## License

This mod is distributed under the terms of the **GNU General Public License v3.0**.

You can find a copy of the license in the LICENSE file included with the source code, or read it online here:
[https://www.gnu.org/licenses/gpl-3.0.html](https://www.gnu.org/licenses/gpl-3.0.html)
