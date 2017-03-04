
This is a (currently very barebones) port of Kate Compton's [Tracery](http://tracery.io "Tracery") text generation grammar tool to Unity (and by extension C# in general).

It's used like this:
	
    var tracerySource = "{"origin":["one#symbol#","two#symbol#","three"],"symbol":"s"}"
    var grammar = new TraceryGrammar(tracerySource);
    Debug.Log(grammar.Generate());
    Debug.Log(grammar.Generate());

which would give an output something like :

    ones
    three
    
You can also specify the random seed when calling Generate, to make output repeatable:

    Debug.Log(grammar.Generate(123456));
    Debug.Log(grammar.Generate(123456));
    Debug.Log(grammar.Generate(123456));
    Debug.Log(grammar.Generate(123456));
    
    ones
    ones
    ones
    ones

There is currently no support for modifiers, variables, or the fancy rich-object stuff that's been added recently.



