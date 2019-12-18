```diff
+ This package is based on original leomani3/Unity-Menu-Generator.
+ Source : https://github.com/leomani3/Unity-Menu-Generator
```

- First : Drop the prefab **CanvasMenuGenerator** into your scene
- Second : Create a ButtonAspect - Right click in your project folder - Create - MenuGenerator - ButtonAspect
- Third : Drop your new ButtonAspect into the CanvasMenuGenerator's **Button Aspect**
- Optional : Create a BackgroundAspect - Right click in your project folder - Create - MenuGenerator - BackgroundAspect
- Optional + : Check the **Use Bg Image** box
- Optional + : Select **Background Padding** with the slider (it's easier to use this option when the menu is created)
- Fourth : Add buttons 
  - Select **CanvasMenuGenerator** 
  - Go to script **Menu Generator** 
  - Expand **Button Names** 
  - Enter the number of buttons you want into the **Size** field 
  - Enter names (text) of your buttons for each **Element**
- Fifth : Enter **Button Font Size**
- Sixth : Select **Space Between Buttons** with the slider (it's easier to use this option when the menu is created)
- Seventh : Add the sound you want to hear when a button is selected into **Select Sound** field in **Menu Manger** script (composent of **CanvasMenuGenerator**)  
- Heighth : Click on **Generate Menu**
- Optional : Import TextMesh Pro (TMP) if needed

---

- You can *update* your menu by clickinxg on *Generate Menu*
- You can *remove* all GameObject by clicking on **Clean Menu**

---

- For each **Button** create with the GameObject **CanvasMenuGenerator**, there is 3 options you can select in the script **_Button Manager_**.
  - **Load Scene** : by clicking on this button, the scene passed in parameters will be load.
  - **Toggle Object** : by clicking on this button, the object passed in parameters will be activated/deactivated.
  - **Quit** : by clicking on this button, the game will quit.

---

- There is 4 video to watch how it works :
  - [First part](https://i.imgur.com/Hqt7mtQ.mp4)
  - [Second part](https://i.imgur.com/p3UfM3R.mp4)
  - [Third part](https://i.imgur.com/a6P9XIC.mp4)
  - [Fourth part](https://i.imgur.com/hI3CplO.mp4)
  
<a id="raw-url" href="https://raw.githubusercontent.com/Marsgames/Unity-Menu-Generator/master/MenuGenerator.unitypackage">Download package</a>
