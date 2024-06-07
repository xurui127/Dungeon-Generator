



# Dungeon Generator Documents 

## Introduction
### Dungeon Generator 1 

Dungeon generator 1 is using basic random walk algorithm to generator the base room and check the neighbors of the rooms. Then check whether adjacent rooms overlaped, and check which door should be opened for each adjacent rooms.

## Dungeon Generator 2 

Dungeon generator 2 is based on the random walk algorithm used in Dungeon generator 1. However, it uses a backtracking algorithm to determine which doors and corridors should be opened at the sametime.
## Dungeon Generator 3

Dungeon generator 3 is one of the most complicated methods. This method involves creating editor tools to generate Dungeon Levels, Dungeon graphs, and Dungeon rooms (saving details such as the bounds of the rooms and the doorway positions,etc..). It also uses generation code to read the stored coordinates to generate the entire dungeon.

## Dungeon Generator 4 

Dungeon generator 4 uses the Binary Space Partitioning algorithm to split the entire map (total dungeon size) into different areas and generate rooms tile by tile. It then uses the centers of the rooms to calculate the distance and generate the corridors.It also using binary code to represent the wall positions to generate the walls.(The binary code like (0 0 0 1), 1st digits represant Up,2nd Right, 3rd Down, 4th Left).

## Dungeon Generator 5 

Dungeon generator 5 is a plug-in. It is samliar like Dungeon generator 3. It is allow using Editor tools to create the Dungeon level graphs to visuallize the whole dungeon. And the different between generator 3 and generator 5 is that generator 5 is more easy to use, because the system will calculate the doorway instead  memually input the coordintes.

# How to use 

## For the Dungeon 1,2 

1. Find Project view.
2. Go to Assets -> Scences -> Generator_1 or Generatoir_2
3. Press play button. 
4. Press KeyBoard "R" to regenerate the dungeon


## For the Dungeon 3

1. In Project view,Go to Assets -> Scences -> Generator_3 
2. Navigate to Generator_3/ScriptableObjectAsset/Dungeon/DungeonRoomNodeGraphs. Right-click and select Create -> ScriptableObject/Dungeon/Room Node Graph to create a room map.  
3. Double-click the Room Node Graph you just created. Right-click to open a menu and create room nodes. Note: The Room Node Graph requires validation, ensuring each graph has one boss room and one chest room. Each room must be connected by at least one corridor node.
4. Navigate to Generator_3/ScriptableObjectAsset/Dungeon/Levels. Right-click and select Create -> ScriptableObject/Dungeon/Dungeon Level to create a level for the generator.
5. Inside the Dungeon Level, you'll need to add a room template list located at Generator_3/Prefabs/Dungeon/Rooms. The Catacombs and Sorcery folders contain room prefabs. Drag these to the room template list in the Dungeon Level ScriptableObject. Drag the Room Node Graph you created to the room node graph list, and enter the level name in the text box.
6. Press Play button to see the result.
7. Press Keyboard "R" to regenerate the dungeon.

## For the Dungeon 4

1. In Project view,Go to Assets -> Scences -> Generator_3
2. In the Hierarchy window, select RoomFirstDungeonGenerator.
3. Adjust the Min Room Width and Min Room Height to change the room sizes.
4. Adjust the Dungeon Width and Dungeon Height to change the overall dungeon size (these values affect the number of rooms generated).
5. Adjust the Offset to change the distance between rooms.
6. Press "Create Dungeon" to generate a dungeon (each time you press this button, a new dungeon is generated).

## For the Dungeon 5 

Tutorial:  https://www.youtube.com/watch?v=JpWWwneEaPo&t=329s


# Conclution

Generators 1 and 2 are designed to generate simple rooms without complex components, similar to the map style of "The Binding of Isaac." They include the basic procedural elements for building a random dungeon map. Generator 3 creates maps similar to "Enter the Gungeon" but requires a lot of coordinate adjustments and is prone to errors. It has numerous validations to check for mistakes, but these can still be hard to spot. Generator 4 offers freedom in room positions but makes it difficult to control the size of the rooms placed on the map. I am not go to deep to check the Dungeon 5, because the deadline is coming. 

And I will leave the useful article for explain how "Enter to Gungeon" generate these map.This method using other algorithm call "Force-directed graph drawing algorithms" which I am not start to research. If I am not in a rushtime,I will keep researching.  



Article: https://www.boristhebrave.com/2019/07/28/dungeon-generation-in-enter-the-gungeon/

Force-directed graph drawing:
1. Wiki: https://en.wikipedia.org/wiki/Force-directed_graph_drawing
2. Paper: https://cs.brown.edu/people/rtamassi/gdhandbook/chapters/force-directed.pdf
3. Vedio: https://www.youtube.com/watch?v=WWm-g2nLHds&list=PLubYOWSl9mIvtnRjCCHP3wqNETTHYjQex 







