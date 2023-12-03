using _01._Prototype;

SandwichMenu sandwichMenu = new SandwichMenu();

sandwichMenu["BLT"] = new Sandwich("Wheat", "Bacon", "", "Lettuce, Tomato");
sandwichMenu["PB&J"] = new Sandwich("White", "", "", "Peanut, Butter, Jelly");
sandwichMenu["Turkey"] = new Sandwich("Rye", "Turkey", "Swiss", "Lettuce, Onion, Tomato");

sandwichMenu["LoadedBLT"] = new Sandwich("Wheat", "Turkey, Bacon", "American", "Lettuce, Tomato, Onion, Olives");
sandwichMenu["ThreeMeatCombo"] = new Sandwich("Rye", "Turkey, Ham, Salami", "Provolone", "Lettuce, Onion");
sandwichMenu["Vegeterian"] = new Sandwich("Wheat", "", "", "Lettuce, Onion, Tomato, Olives, Spinach");

Sandwich sandich1 = sandwichMenu["BLT"].Clone() as Sandwich;
Sandwich sandich2 = sandwichMenu["ThreeMeatCombo"].Clone() as Sandwich;
Sandwich sandich3 = sandwichMenu["Vegeterian"].Clone() as Sandwich;