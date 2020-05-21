# A simple “library” window for Unity

I have a few folders which contain assets I like to be able to quickly import: mainly a library of 3D primitives that’s a little bit more comprehensive than the native Unity one.

After all, when just noodling around and seeing how something might work, I just want quick one click access to a selection of simple shapes, a selection that I can easily update from other 3rd party applications as well, a selection that I can also use in other applications. (So I don’t want everything hidden away in a Unity package or something similar.)

It’s so basic that all configuration is done within the script itself:

Define paths to be scanned under the OnGUI method:

`this.sources.Add("/Volumes/external/work/_unity object lib");
this.sources.Add("/Users/charlie/Documents/_primitves");`

Define what file extensions will be included:

`this.acceptable.Add(".obj");
this.acceptable.Add(".fbx");`

Very very basic stuff, but I find it useful...
