-> main

=== main ===
Which pokemon do you choose?
    + [Charmander]
        -> chosen("Charmander")
    + [Bulbasaur]
        -> chosen("Bulbasaur")
    + [Squirtle]
        -> chosen("Squirtle")
        
        
        

=== chosen(pokemon) ===
Are you sure?
    + [Yes]
        You chose {pokemon}!
        ->DONE
    + [No, i want to choose a different one]
        -> main
        
-> END