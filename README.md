
This is a (currently very barebones) port of Kate Compton's [Tracery|https://tracery.io] text generation grammar tool to Unity (and by extension C# in general).

It's used like this:

	var tracerySource = "{"origin":["one#symbol#","two#symbol#","three"],"symbol":"s"}"
    var grammar = new TraceryGrammar(tracerySource);
    Debug.Log(grammar.Generate());
    Debug.Log(grammar.Generate());

which would give an output something like :

	ones
	three

There is currently no support for modifiers, variables, or the fancy rich-object stuff that's been added recently.



