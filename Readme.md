# Educational work "Game of clans" for the study of patterns

Each clan includes squads of warriors, dragons and robots. 
The number of players in each squad and the location of the 
squads on the clan front are determined randomly. 
Each of the players can have additional features: color, 
height, type of clothing.
The head of the clan can give commands "forward", "back", "fight" 
to each detachment separately. 
In response to the received command, each player from the squad 
performs the necessary action. (Buttons “Forward”, “Back”, “Attack” 
opposite each squad). If the player is given the "fight" command, 
they may be injured (random), hitting the player again puts them 
in the "out of combat" state.
Initially, the clans are located in the middle at a distance of 2 cells.
By command:
"forward" - the player advances 1 cell
"back" - the player goes back 1 cell and, if he was injured, restores his state;
“fight” - the player shoots and can be wounded (random choice), hitting 
a wounded player again puts him in the “out of battle” state.
Clan chiefs give commands in turn



****Patterns used:****
"Factory Method" - for generating players
"Decorator" - to specify additional properties of players
"Bridge" - to display information about clan players
"Command" - to generate commands
"State" - implements the state of the players ("healthy", "wounded", "left the battle")
"Memento" - implements the ability to create a checkpoint


Works in VisualStudio 2019, .Net Framework 4.7.2