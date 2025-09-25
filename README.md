# 3D-Graphics---Unity

Worked on June - July 2024

## Author

Blair C. Tate aka cubeo

## Overview & Background

- **Overview**
This was one of my first larger projects before studying computer programming, so their is a heavy need for refinement. I intend to clean this code one day, or perhaps start from scratch in order to develope with the techniques I have learned from my more recent experiences in OOP, firmware/embedded systems, and web application development.

For this project I set out to explore 3D rendering algorithms inspired by old architectural drawing techniques. The algorithms used were created from scratch using linear algebra. See Algorithm section for more details. 

This project currently is capable of projecting a triangular plane (not a prism) on a 3D plane into perspective on a 2D surface. You are able to change your position relative to the position of the triangle and see how the shape appears from different angles and elevation. One issue of note is that the window currently wraps around, so more work is to be done in order to make it possible for a user to walk around the space and get out of view of the object.

The project is in an early development stage, and requires more work when time is available.

- **Future Work**
    - Add Comments to Code
    - Add prismatic shapes (Platonic Solids)
    - Fix window wrapping
    - Create surface fill algorithm
    - Implement Shadow algorithm

<details close>
<summary><h3>Background</h3></summary>
- **Background**
Before starting my journey into the world of programming, I studied the visual arts at Concordia University. Ironically, it was the study of the arts that made me interedted in the applied sciences (such as programming). But that is another story. 

During my studies, I became fassinated with geometry and how it could be used to tell complicated narratives, so naturally, I did a lot of research into perspective. I ended up comming accross some books on archatectural techniques for putting projecting orthographic drawings (something I learned in trade school) into 3D perspective drawings. I had tried playing around with the more basic techniques taught in art school, but found the distortion of the techniques jarring and hard to turn into proper equations. But this technique of using orthographic schematics allowed for precision, and could be turned into an equation.

Before attempting to create this equation, there had to be a reason to do so. During my early days (curiosity days), I was talking with a friend who works in the video game industry as a programmer. I had mentioned that I not only thought I could create an equation for projecting 3D shapes onto a 2D surface, but that I could also create equations to calculate shadows, and project them as well... He doubted my claim, and so I set out to prove that it was possible.

And of course it was, because it has already been done. 

For my equations, though they likely have already been derived (look into it), they are not exactly efficient, or quick as of yet. At the time that I made this project my skills as a programmer, and documenter, were fairly limited. So the code still requires a lot of cleaning, and maybe needs to be started from scratch in order to create cleaner and faster runtime code. But currently it does work. 
</details>

---

## Project Images Videos and Notes



---

## Algorithms



---

```
ISC License

Copyright (c) 2024 Blair C. Tate / aka Cubeo

Permission to use, copy, modify, and/or distribute this software for any
purpose with or without fee is hereby granted, provided that the above
copyright notice and this permission notice appear in all copies.

THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
```
