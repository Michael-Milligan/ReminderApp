Hello, the instructions for adding, modifying, or deleting ingredients for the Explosive Torpedo are described below.
For the name of the ingredients, look in the file Ingredients.txt
!!! Each TORPEDO has its own file, for example: Recipe_StandardTorpedo.txt
!!! Do not rename files...

------------------------
To change the number of torpedoes created, change the number in FIRST LINE, for example 5:
------------------------
CraftAmount:5



------------------------
Adding:
Add new ingredients a new line, below the line -> "-----"
------------------------
	To add some ingredients to the recipe,
	add the line below to the Recipe.txt file.
	Where before sign ":" name of ingredient, and after quantity, without spaces:

Titanium:1



------------------------
Modifying:
------------------------
	To change the recipe, change the name or quantity of the ingredient,
	for example, change to gold with a quantity of 2.
	Where before sign ":" name of ingredient, and after quantity, without spaces:

Gold:2



------------------------
Deleting:
Do not delete the line -> "CraftAmount:1"
Do not delete the line -> "-----"
------------------------
	To delete an ingredient, delete the line of this ingredient from the torpedo file
